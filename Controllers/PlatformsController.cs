using AutoMapper;
using CommandService.Data;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{

  [Route("api/c/[controller]")]
  [ApiController]
  public class PlatformsController : ControllerBase
  {
    private readonly ICommandRepo _repository;
    private readonly IMapper _mapper;

    public PlatformsController(ICommandRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpPost]
    public ActionResult TestInBoundConnection()
    {
      Console.WriteLine("--> Inbound POST # Command Service");

      return Ok("Inbound test of from Platforms Controller");
    }
  }
}