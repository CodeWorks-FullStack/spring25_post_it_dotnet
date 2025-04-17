



namespace post_it_dotnet.Repositories;

public class AlbumsRepository
{
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  private readonly IDbConnection _db;

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(title, description, cover_img, creator_id, category)
    VALUES(@Title, @Description, @CoverImg, @CreatorId, @Category);
    
    SELECT
    albums.*,
    accounts.*
    FROM albums
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.id = LAST_INSERT_ID();";

    Album createdAlbum = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).SingleOrDefault();
    return createdAlbum;
  }

  internal List<Album> GetAlbums()
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    INNER JOIN accounts ON accounts.id = albums.creator_id;";

    List<Album> albums = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }).ToList();

    return albums;
  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.id = @albumId;";

    Album foundAlbum = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;

    }, new { albumId }).SingleOrDefault();

    return foundAlbum;
  }

  internal void ArchiveAlbum(Album album)
  {
    string sql = "UPDATE albums SET archived = @Archived WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, album);

    // NOTE super elite code that your boss doesn't want you to know about
    switch (rowsAffected)
    {
      case 1:
        return;
      case 0:
        throw new Exception("UPDATE WAS NOT SUCCESSFUL");
      default:
        throw new Exception("UPDATE WAS TOO SUCCESSFUL");
    }
  }

  internal List<Album> GetAlbumsByCategory(string category)
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.category LIKE @category;";

    List<Album> albums = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, new { category = $"%{category}%" }).ToList();
    return albums;
  }
}