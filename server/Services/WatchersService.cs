namespace post_it_dotnet.Services;

public class WatchersService
{
  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }
  private readonly WatchersRepository _repository;

}