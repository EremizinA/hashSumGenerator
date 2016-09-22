using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HashCounterLibrary;
using System.Security.Cryptography;
using SsdeepNET;

namespace HashAlgo
{  

    public static class CheckSumCreator
    {

        public static List<string> files { get; set; }
        public static List<string> dirs { get; set; }
        public static ICollection<KeyValuePair<string, string>> filePlusHash;

     
           
         
        public static void GetDirectoriesRec(string path)
        {
            dirs = new List<string>();
            files = new List<string>();



            int iter;
  
           DirectoryInfo[] dirInfo = new DirectoryInfo(path).GetDirectories();

             StringBuilder sBuilder = new StringBuilder(path);
             try
             {
                 if (dirInfo.Length == 0)
                 {
                     iter = 1;
                    // Console.WriteLine("Директория не содержит других директорий!");
                 }

                 else iter = dirInfo.Length;
                 

                 for (int i = 0; i < iter; i++)
                 {
                     //  sBuilder.Append(@"\");
                     FileInfo[] fileInfo = new DirectoryInfo(path).GetFiles();

                     for (int j = 0; j < fileInfo.Length; j++)
                     {
                          files.Add(fileInfo[j].FullName);
                          Console.WriteLine(fileInfo[j].FullName);
                     }
                     GetDirectoriesRec(dirInfo[i].FullName);
                     Console.WriteLine(dirInfo[i].FullName);
                 }
             }

             catch (Exception e) {  }
             
         }

        public static void ShowFileList()
        {
            files = files.Distinct().ToList();
  
            for (int i = 0; i < files.Count; i++)
            {
             Console.WriteLine(files[i]);
            }
        }


        public static void WriteFileWithCheckSum<T>(T hashType, string path) where T : HashAlgorithm
                                                                               
        {
            if (!File.Exists(path)) File.Create(path);
           

            HashCounter.GetHash hashFunction;
            HashCounter.InitHashFunction<T>(hashType, out hashFunction);

            try
            {
                
                StringBuilder[] sBuilder = new StringBuilder[files.Count];
                string[] buff = new string[files.Count];
                for (int i = 0; i < files.Count; i++)
                {
                    sBuilder[i] = new StringBuilder();
                    sBuilder[i].Append(files[i] + "|" + hashFunction(File.ReadAllText(@files[i])));
                }

                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    for (int i = 0; i < sBuilder.Length; i++)
                        sw.WriteLine(sBuilder[i].ToString());
                }
          }
         catch (Exception e) { Console.WriteLine(e.Message + "\n" + e.StackTrace); }
            
        }



    }
}
