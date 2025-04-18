



namespace post_it_dotnet.Services;

public class WatchersService
{
  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }
  private readonly WatchersRepository _repository;

  internal WatcherProfile CreateWatcher(Watcher watcherData)
  {
    WatcherProfile watcherProfile = _repository.CreateWatcher(watcherData);
    return watcherProfile;
  }

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    List<WatcherProfile> watcherProfiles = _repository.GetWatcherProfilesByAlbumId(albumId);
    return watcherProfiles;
  }

  internal List<WatcherAlbum> GetWatcherAlbumsByAccountId(string accountId)
  {
    List<WatcherAlbum> watcherAlbums = _repository.GetWatcherAlbumsByAccountId(accountId);
    return watcherAlbums;
  }

  private Watcher GetWatcherById(int watcherId)
  {
    Watcher watcher = _repository.GetWatcherById(watcherId);

    if (watcher == null)
    {
      throw new Exception("Invalid watcher id: " + watcherId);
    }

    return watcher;
  }

  internal void DeleteWatcher(int watcherId, Account userInfo)
  {
    Watcher watcher = GetWatcherById(watcherId);

    if (watcher.AccountId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT DELETE ANOTHER USER'S WATCHER, {userInfo.Name.ToUpper()}!!!");
    }

    _repository.DeleteWatcher(watcherId);
  }
}