namespace post_it_dotnet.Models;

// NOTE abstract denotes that this class can only be inherited, and never newed up
// NOTE this class accepts a type argument when inheriting from it, and will use that type for the id of the class
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}