
namespace post_it_dotnet.Repositories;

public class WatchersRepository
{
  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO 
    watchers(album_id, account_id) 
    VALUES (@AlbumId, @AccountId);
    
    SELECT * FROM watchers WHERE id = LAST_INSERT_ID();";

    Watcher watcher = _db.Query<Watcher>(sql, watcherData).SingleOrDefault();
    return watcher;
  }
}