using System;
using System.Collections.Generic;
using System.Text;

namespace OutOfTheBox.Common
{
    public class DRX //Double Rolling XOR
    {
        public static string Crypt(string input)
        {
            return Crypt(input, Internals.CRYPT_KEY_STATIC, Internals.CRYPT_KEY_DYNAMIC);
        }

        public static string Crypt(string input, string key1, string key2)
        {
            if (key1.Length == key2.Length) throw new Exception("Key 1 and key 2 must not be the same length.");

            StringBuilder sb = new StringBuilder(input.Length);

            for (int i = 0; i < input.Length; i++)
                sb.Append((char)(input[i] ^ key1[i % key1.Length] ^ key2[i % key2.Length]));

            return sb.ToString();
        }
    }
}
