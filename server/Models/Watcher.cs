namespace post_it_dotnet.Models;

public class Watcher : RepoItem<int>
{
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
}