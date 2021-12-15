using System.Security.Cryptography;
using System.Text;

namespace gen.common
{
    public static class HashUtil
    {
        public static string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var b in hash)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}