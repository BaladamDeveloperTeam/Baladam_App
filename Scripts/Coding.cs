using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace security
{
    public class Coding
    {
        public string Md5Sum(string strToEncrypt)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        public string Sha1Sum(string strToEncrypt)
        {
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] hashBytes = sha.ComputeHash(bytes);

            
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        public int[] MySecurityEncrypt(string strToEncrypt)
        {
            char[] charArray = strToEncrypt.ToCharArray();
            int[] intArray = new int[charArray.Length];
            for(int i = 0; i < charArray.Length; i++)
            {
                intArray[i] = charArray[i];
            }
            for(int i = 0; i < intArray.Length; i++)
            {
                intArray[i] = ((((intArray[i] * 3) + 5) * 25) - 67);
            }
            return intArray;
        }

        public string MySecurityDecrypt(int[] intToDecrypt)
        {
            char[] charArray = new char[intToDecrypt.Length];
            for(int i = 0; i < intToDecrypt.Length; i++)
            {
                intToDecrypt[i] = ((((intToDecrypt[i] - 67) / 25) - 5) / 3) + 2;
            }
            for(int i = 0; i < intToDecrypt.Length; i++)
            {
                charArray[i] = Convert.ToChar(intToDecrypt[i]);
            }
            return charArray.ToString();
        }

    }
}
