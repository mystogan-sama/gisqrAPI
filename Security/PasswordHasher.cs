using System.Security.Cryptography;
using System.Text;
using gisAPI.Interfaces;

namespace gisAPI.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public void Generate(string pass, out byte[] hash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));
            }
        }

        public bool Verify(string pass, byte[] hash, byte[] salt)
        {
            using (var hmac = new HMACSHA512(salt))
            {
                var computed = hmac.ComputeHash(Encoding.UTF8.GetBytes(pass));

                for (var i = 0; i < computed.Length; i++)
                {
                    if (computed[i] != hash[i]) return false;
                }
            }
            return true;
        }
    }
}