namespace post_it_dotnet.Models;

// NOTE abstract denotes that this class can only be inherited, and never newed up
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}