using System;
using System.Security.Cryptography;
using System.Text;

namespace Infraestructure.Helpers
{
    public class Security
    {
        protected Security()
        {
        }

        public static string sha256_hash(string value)
        {
            string finalKey = string.Empty;
            byte[] encode = new byte[value.Length];
            encode = Encoding.UTF8.GetBytes(value);
            finalKey = Convert.ToBase64String(encode);
            return finalKey;
        }

        public static bool CompareHash(string firstText, string secondText)
        {
            bool result = false;
            string firstValue = sha256_hash(firstText);
            string secondValue = sha256_hash(secondText);

            if (firstValue == secondValue)
            {
                result = true;
            }

            return result;
        }
    }
}
