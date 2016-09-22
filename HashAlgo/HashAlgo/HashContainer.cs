using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashAlgo;
using HashCounterLibrary;
using System.Security.Cryptography;
using SsdeepNET;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace HashAlgo
{
    [Serializable]
    public class HashContainer
    {
        public ICollection<KeyValuePair<string, string>> FilesWithHash { get; set; }

        public ICollection<KeyValuePair<string, string>> oldFilesWithMD5Hash { get; set; }
        public ICollection<KeyValuePair<string, string>> oldFilesWithSHA256Hash { get; set; }
        public ICollection<KeyValuePair<string, string>> newFilesWithMD5Hash { get; set; }
        public ICollection<KeyValuePair<string, string>> newFilesWithSHA256Hash { get; set; }
        public ICollection<KeyValuePair<string, string>> oldFilesWithSsdeep { get; set; }
        public ICollection<KeyValuePair<string, string>> newFilesWithSsdeep { get; set; }

        public ICollection<KeyValuePair<string, string>> oldFilesWithHash { get; set; }
        public ICollection<KeyValuePair<string, string>> newFilesWithHash { get; set; }

        public HashContainer()
        {
         
        }
    }
}


