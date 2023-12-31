using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.EventProcessing
{
  public class EventProcessor : IEventProcessor
  {
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
    {
      _scopeFactory = scopeFactory;
      _mapper = mapper;
    }
    public void ProcessEvent(string message)
    {
      var eventType = DetermineEvent(message);

      switch (eventType)
      {
        case EventType.PlatformPublished:
          // todo
          addPlatform(message);
          break;
        default:
          break;
      }
    }

    private void addPlatform(string platformPublishedMessage)
    {
      using (var scope = _scopeFactory.CreateScope())
      {
        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);


        try
        {
          var plat = _mapper.Map<Platform>(platformPublishedDto);
          if (!repo.ExternalPlatformExists(plat.ExternalID))
          {
            repo.CreatePlatform(plat);
            repo.SaveChanges();
            Console.WriteLine("--> Platform added!");
          }
          else
          {
            Console.WriteLine("--> Platform already exists...");
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine($"--> Could not add platform to the db {ex.Message}");
        }
      }
    }
    private EventType DetermineEvent(string notificationMessage)
    {
      Console.WriteLine("--> Determining the Event");

      var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

      switch (eventType.Event)
      {
        case "Platform.Published":
          Console.WriteLine("Platform Publushed Event Detected");
          return EventType.PlatformPublished;
        default:
          Console.WriteLine("Cannot determin the event type");
          return EventType.Undetermined;
      }
    }
  }

  enum EventType
  {
    PlatformPublished,
    Undetermined
  }
}