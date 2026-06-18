using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;

namespace Services.utilities
{
    public static class HashService
    {
        public static string HashString(string str)
        {
            byte[] salt = new byte[16];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var config = new Argon2Config
            {
                Type = Argon2Type.HybridAddressing, 
                Version = Argon2Version.Nineteen,   
                TimeCost = 3,                       
                MemoryCost = 19456,
                Lanes = 1,                          
                Threads = 1,
                Password = System.Text.Encoding.UTF8.GetBytes(str),
                Salt = salt,
                HashLength = 32                     
            };

            using var argon2 = new Argon2(config);
            using SecureArray<byte> hash = argon2.Hash();
            return config.EncodeString(hash.Buffer);
        }

        public static bool Compare(string hashedString, string str)
        {
            return Argon2.Verify(hashedString, str);
        }
    }
}
