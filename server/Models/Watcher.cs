namespace post_it_dotnet.Models;

// NOTE backing class for the many-to-many
public class Watcher : RepoItem<int>
{
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
}

// NOTE DTO (data transfer object)
// NOTE supports sending data from the watchers table and accounts table combined into one flattened out object
public class WatcherProfile : Profile
{
  public int WatcherId { get; set; }
  public int AlbumId { get; set; }
}

public class WatcherAlbum : Album
{
  public int WatcherId { get; set; }
  public string AccountId { get; set; }
}