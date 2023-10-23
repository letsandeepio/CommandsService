using CommandService.Models;

namespace CommandService.Data
{
  public interface ICommandRepo
  {
    bool SaveChanges();
    //methods for platforms 
    IEnumerable<Platform> GetAllPlatforms();
    void CreatePlatform(Platform plat);
    bool PlatformExists(int platformId);

    //methos for commands
    IEnumerable<Command> GetCommandsForPlatform(int platformId);
    Command GetCommand(int platformId, int commandId);
    void CreateCommand(int platformId, Command command);

    bool ExternalPlatformExists(int externalPlatformId);
  }
}