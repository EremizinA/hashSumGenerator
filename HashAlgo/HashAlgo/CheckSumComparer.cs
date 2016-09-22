using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using SsdeepNET;
using HashCounterLibrary;

namespace HashAlgo
{
   public static class CheckSumComparer
    {
       public static ICollection<KeyValuePair<string, string>> FilesWithHash { get; set; }



       public static ICollection<KeyValuePair<string, string>> ReadFileWithCheckSum(string path)
       {
           FilesWithHash = new Dictionary<string, string>();

           
          try
           {
               using (StreamReader sr = new StreamReader(path))
               {
                   string FileLine;
                   string FilePath;
                   string FileHash;

                   while (!sr.EndOfStream)
                   {
                       FileLine = sr.ReadLine();
                       FilePath = FileLine.Substring(0, FileLine.IndexOf("|"));
                       FileHash = FileLine.Substring(FileLine.IndexOf("|") + 1, FileLine.Length - 1 - FilePath.Length);
                       FilesWithHash.Add(new KeyValuePair<string, string>(FilePath, FileHash));
                     //  Console.WriteLine(FilePath + " " + FileHash);
                    }
               }
           }

           catch (Exception e) { Console.WriteLine(e.Message + "\n" + e.StackTrace); }

           return FilesWithHash;
       }

       public static ICollection<KeyValuePair<string, bool>> 
           CompareFilesCheckSum(ICollection<KeyValuePair<string, string>> oldFiles, 
           ICollection<KeyValuePair<string, string>> newFiles)
          {
              ICollection<KeyValuePair<string, bool>> comparisonResult = new Dictionary<string, bool>(oldFiles.Count);
              ICollection<KeyValuePair<string, string>> oldkvPairs = new Dictionary<string,string>();
              ICollection<KeyValuePair<string, string>> newkvPairs = new Dictionary<string, string>();
              
           if (oldFiles.Count == newFiles.Count)
              {
                 
                  foreach (KeyValuePair<string, string> kvPair in oldFiles)
                   {
                       oldkvPairs.Add(new KeyValuePair<string,string>(kvPair.Key, kvPair.Value));
                   }
                 
               foreach (KeyValuePair<string, string> kvPair in newFiles)
                  {
                      newkvPairs.Add(new KeyValuePair<string, string>(kvPair.Key, kvPair.Value));
                  }

               var result = from okv in oldkvPairs
                            from nkv in newkvPairs
                            where okv.Key == nkv.Key
                            select new KeyValuePair<string, bool>(okv.Key, okv.Value == nkv.Value);
               

               foreach (KeyValuePair<string, bool> kvPair in result)
               {
                   comparisonResult.Add(new KeyValuePair<string, bool>(kvPair.Key, kvPair.Value));
                   Console.WriteLine(kvPair.Key + " " + kvPair.Value);
               }
               }

           return comparisonResult;
                                  
       }


       public static ICollection<KeyValuePair<string, int>>
           CompareFilesSSdeepCheckSum(ICollection<KeyValuePair<string, string>> oldFiles,
           ICollection<KeyValuePair<string, string>> newFiles)
       {
           ICollection<KeyValuePair<string, int>> comparisonResult = new Dictionary<string, int>(oldFiles.Count);
           ICollection<KeyValuePair<string, string>> oldkvPairs = new Dictionary<string, string>();
           ICollection<KeyValuePair<string, string>> newkvPairs = new Dictionary<string, string>();

           if (oldFiles.Count == newFiles.Count)
           {

               foreach (KeyValuePair<string, string> kvPair in oldFiles)
               {
                   oldkvPairs.Add(new KeyValuePair<string, string>(kvPair.Key, kvPair.Value));
               }

               foreach (KeyValuePair<string, string> kvPair in newFiles)
               {
                   newkvPairs.Add(new KeyValuePair<string, string>(kvPair.Key, kvPair.Value));
               }

               var result = from okv in oldkvPairs
                            from nkv in newkvPairs
                            where okv.Key == nkv.Key
                            select new KeyValuePair<string, int>(okv.Key, Comparer.Compare(okv.Value, nkv.Value));


               foreach (KeyValuePair<string, int> kvPair in result)
               {
                   comparisonResult.Add(new KeyValuePair<string, int>(kvPair.Key, kvPair.Value));
                   Console.WriteLine(kvPair.Key + " " + kvPair.Value);
               }
           }

           return comparisonResult;

       }

  


     }

  
}
