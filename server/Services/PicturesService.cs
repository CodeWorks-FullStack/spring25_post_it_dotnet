


namespace post_it_dotnet.Services;

public class PicturesService
{
  public PicturesService(PicturesRepository repository, AlbumsService albumsService)
  {
    _repository = repository;
    _albumsService = albumsService;
  }
  private readonly PicturesRepository _repository;
  private readonly AlbumsService _albumsService;

  internal Picture CreatePicture(Picture pictureData)
  {
    Album album = _albumsService.GetAlbumById(pictureData.AlbumId);

    if (album.Archived)
    {
      throw new Exception(album.Title + " is archived and no longer accepting pictures!");
    }

    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }

  private Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);
    if (picture == null)
    {
      throw new Exception("Invalid picture id: " + pictureId);
    }
    return picture;
  }

  internal void DeletePicture(int pictureId, Account userInfo)
  {
    Picture picture = GetPictureById(pictureId);
    if (picture.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CAN NOT DELETE ANOTHER USER'S PICTURE, {userInfo.Name.ToUpper()}");
    }
    _repository.DeletePicture(pictureId);
  }
}