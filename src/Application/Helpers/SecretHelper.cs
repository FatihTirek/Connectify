using System.Security.Cryptography;

namespace Connectify.src.Application.Helpers
{
    public static class SecretHelper
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

        public static string GenerateSixDigitCode()
        {
            using var generator = RandomNumberGenerator.Create();
            var bytes = new byte[4]; generator.GetBytes(bytes);
            var number = BitConverter.ToInt32(bytes, 0);
            number = Math.Abs(number) % 1000000;

            return number.ToString("D6");
        }

        public static string Hash(string secret)
        {

            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var key = Rfc2898DeriveBytes.Pbkdf2(secret, salt, Iterations, Algorithm, KeySize);

            return string.Join(':', Convert.ToBase64String(key), Convert.ToBase64String(salt), Iterations, Algorithm);
        }

        public static bool Verify(string secret, string hash)
        {
            var segments = hash.Split(':');
            var iterations = int.Parse(segments[2]);
            var key = Convert.FromBase64String(segments[0]);
            var salt = Convert.FromBase64String(segments[1]);
            var algorithm = new HashAlgorithmName(segments[3]);
            var output = Rfc2898DeriveBytes.Pbkdf2(secret, salt, iterations, algorithm, key.Length);

            return CryptographicOperations.FixedTimeEquals(output, key);
        }
    }
}