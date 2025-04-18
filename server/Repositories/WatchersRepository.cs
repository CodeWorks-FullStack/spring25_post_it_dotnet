

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

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    watchers.*,
    accounts.*
    FROM watchers
    INNER JOIN accounts ON accounts.id = watchers.account_id
    WHERE watchers.album_id = @albumId;";

    List<WatcherProfile> watcherProfiles = _db.Query(sql, (Watcher watcher, WatcherProfile account) =>
    {
      account.AlbumId = watcher.AlbumId;
      account.WatcherId = watcher.Id;
      return account;
    },
     new { albumId }).ToList();
    return watcherProfiles;
  }
}