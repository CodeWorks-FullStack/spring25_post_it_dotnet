

namespace post_it_dotnet.Services;

public class AlbumsService
{
  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }
  private readonly AlbumsRepository _repository;

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal List<Album> GetAlbums()
  {
    List<Album> albums = _repository.GetAlbums();
    return albums;
  }

}