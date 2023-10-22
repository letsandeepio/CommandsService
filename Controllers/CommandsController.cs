using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
  [Route("api/c/platforms/{platformId}/[controller]")]
  [ApiController]
  public class CommandsController : ControllerBase
  {
    private readonly ICommandRepo _repository;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
      Console.WriteLine($"--> Hit GetCommandsForPlatform {platformId}");

      if (!_repository.PlatformExists(platformId))
      {
        return NotFound();
      }

      var commands = _repository.GetCommandsForPlatform(platformId);

      return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));

    }

    [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
    {
      Console.WriteLine($"--> Hit GetCommandsForPlatform {platformId} {commandId}");

      if (!_repository.PlatformExists(platformId))
      {
        return NotFound();
      }

      var command = _repository.GetCommand(platformId, commandId);

      if (command == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<CommandReadDto>(command));
    }



  }
}