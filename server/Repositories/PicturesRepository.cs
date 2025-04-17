



namespace post_it_dotnet.Repositories;

public class PicturesRepository
{
  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO
    pictures(img_url, creator_id, album_id)
    VALUES(@ImgUrl, @CreatorId, @AlbumId);
    
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    INNER JOIN accounts ON accounts.id = pictures.creator_id
    WHERE pictures.id = LAST_INSERT_ID();";

    Picture createdPicture = _db.Query(sql, (Picture picture, Profile account) =>
    {
      picture.Creator = account;
      return picture;
    }, pictureData).SingleOrDefault();

    return createdPicture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    INNER JOIN accounts ON accounts.id = pictures.creator_id
    WHERE pictures.album_id = @albumId;";

    List<Picture> pictures = _db.Query(sql, (Picture picture, Profile account) =>
    {
      picture.Creator = account;
      return picture;
    },
    new { albumId }).ToList();
    return pictures;
  }

  internal Picture GetPictureById(int pictureId)
  {
    string sql = "SELECT * FROM pictures WHERE id = @pictureId;";

    Picture foundPicture = _db.Query<Picture>(sql, new { pictureId }).SingleOrDefault();

    return foundPicture;
  }

  internal void DeletePicture(int pictureId)
  {
    string sql = "DELETE FROM pictures WHERE id = @pictureId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { pictureId });

    if (rowsAffected != 1)
    {
      throw new Exception(rowsAffected + " ROWS WERE DELETED AND THAT IS NOT GOOD, BUD");
    }
  }
}