namespace gisAPI.Interfaces
{
  public interface IUserAccessor
  {
    string GetCurrentUsername();
    string GetCurrentRole();
  }
}