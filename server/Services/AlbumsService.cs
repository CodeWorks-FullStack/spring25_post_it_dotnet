


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

  // NOTE overload
  // you can reuse the same name for a method, but depending on how many arguments/types of arguments are passed determines which method gets called (method signature)
  internal List<Album> GetAlbums(string category)
  {
    List<Album> albums = _repository.GetAlbumsByCategory(category);
    return albums;
  }
  internal List<Album> GetAlbums(bool archived)
  {
    throw new NotImplementedException();
  }
  internal List<Album> GetAlbums(string category, string title)
  {
    throw new NotImplementedException();
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);

    if (album == null)
    {
      throw new Exception("No album found with the id of " + albumId);
    }

    return album;
  }

  internal Album ArchiveAlbum(int albumId, Account userInfo)
  {
    Album album = GetAlbumById(albumId);

    if (album.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT ARCHIVE ANOTHER USER'S ALBUM, {userInfo.Name.ToUpper()}!");
    }

    album.Archived = !album.Archived;

    _repository.ArchiveAlbum(album);

    return album;
  }
}