namespace post_it_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider, PicturesService picturesService, WatchersService watchersService)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
    _picturesService = picturesService;
    _watchersService = watchersService;
  }

  private readonly Auth0Provider _auth0Provider;
  private readonly AlbumsService _albumsService;
  private readonly PicturesService _picturesService;
  private readonly WatchersService _watchersService;


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAlbums([FromQuery] string category)
  {
    try
    {
      List<Album> albums;

      if (category == null)
      {
        albums = _albumsService.GetAlbums();
      }
      else
      {
        albums = _albumsService.GetAlbums(category);
      }

      return Ok(albums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE soft delete! Might not be a good reference for your checkpoints!
  [Authorize]
  [HttpDelete("{albumId}")]
  public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbum(albumId, userInfo);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
      return Ok(pictures);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}