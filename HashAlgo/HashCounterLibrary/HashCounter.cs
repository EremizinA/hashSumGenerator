using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using SsdeepNET;

namespace HashCounterLibrary
{
    public static class HashCounter
    {
        public delegate string GetHash(string input);
        
        public static string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x"));


            return sBuilder.ToString();
        }

        //********************************************************************

       
        public static string GetSHA256Hash(string input)
        {
            SHA256 sha256hash = SHA256.Create();
            
            byte[] data = sha256hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x"));

            return sBuilder.ToString();
        }

        //*********************************************************


        public static string GetFNV1aHash(string input)
        {
            const uint FNV_32_PRIME = 0x01000193;
            uint hval = 0x811c9dc5;


            for (int i = 0; i < input.Length; i++)
            {
                hval ^= (uint)input[i++];
                hval *= FNV_32_PRIME;
            }

            return hval.ToString("X");
        }


        //***********************************************

        public static string GetSsdeepHash(string input)
        {
            return Hasher.HashBuffer(input, input.Length);
        }


        //***********************************************

        public static void InitHashFunction<T>(T hashType, out GetHash hashFunction) where T : HashAlgorithm
        {

            if (hashType.GetType() == MD5.Create().GetType())
                hashFunction = HashCounter.GetMd5Hash;

            else if (hashType.GetType() == SHA256.Create().GetType())
                hashFunction = HashCounter.GetSHA256Hash;


            else if (hashType.GetType() != MD5.Create().GetType()
                & hashType.GetType() != SHA256.Create().GetType() 
                & hashType.GetType() != HashAlgorithm.Create().GetType())
                hashFunction = HashCounter.GetFNV1aHash;

               else hashFunction = HashCounter.GetSsdeepHash;
        }


    }
}
