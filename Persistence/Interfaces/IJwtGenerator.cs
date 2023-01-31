using gisAPI.Persistence.Repositories;

namespace gisAPI.Interfaces
{
  public interface IJwtGenerator
  {
    string CreateToken(User user);
  }
}