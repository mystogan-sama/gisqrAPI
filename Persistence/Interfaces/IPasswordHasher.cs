namespace gisAPI.Interfaces
{
  public interface IPasswordHasher
  {
    void Generate(string pass, out byte[] hash, out byte[] salt);
    bool Verify(string pass, byte[] hash, byte[] salt);
  }
}