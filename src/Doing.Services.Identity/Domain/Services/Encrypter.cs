using System.Security.Cryptography;
using System;
using System.Text;  


namespace Doing.Services.Identity.Domain.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int _saltSize = 40;
        private static readonly int _deriveBytesIterationsCount = 10000;
        
        public string GetHash(string password, string saltedValue)
        {
             var generatedSalt = 
                    new Rfc2898DeriveBytes(password, GetBytes(saltedValue), _deriveBytesIterationsCount);
            
            return Convert.ToBase64String(generatedSalt.GetBytes(_saltSize));
        }

        public string GetSalt()
        {
           var random = new Random();

           var salt = new byte[_saltSize];

           var rng = RandomNumberGenerator.Create();

           rng.GetBytes(salt);

           return Convert.ToBase64String(salt);
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length*sizeof(char)];

            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            
            return bytes;
        }
    }
}