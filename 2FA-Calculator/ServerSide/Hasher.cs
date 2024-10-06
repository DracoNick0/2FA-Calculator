using System.Text;
using System.Security.Cryptography;

namespace _2FA_Calculator.Server
{
    class Hasher
    {
        public Hasher() { }

        public string computeSha256Hash(string rawData)
        {
            // Create a SHA256 object
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Compute the hash - returns a byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));  // Convert byte to hex string
                }
                return builder.ToString();
            }
        }
    }
}