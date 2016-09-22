using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading;
using System.IO;
using HashCounterLibrary;
using HashAlgo;
using SsdeepNET;
using System.Runtime.InteropServices;


namespace MyScanner
{
    class Program
    
    {
        public delegate void HashSumScanner<T>(T algorithm, string path) where T : HashAlgorithm;

        const int waitTime = 10000;

       static void Main(string[] args)
        {
            string MD5OldPath = @"D:\ScannerResult\md5checksums.txt";
            string SHA256OldPath = @"D:\ScannerResult\sha256checksum1.txt";
            string MD5NewPath = @"D:\ScannerResult\md5checksumsnew.txt";
            string SHA256NewPath = @"D:\ScannerResult\sha256checksum1new.txt";
            string SsdeepOldPath = @"D:\ScannerResult\ssdeepchecksums.txt";
            string SsdeepNewPath = @"D:\ScannerResult\ssdeepchecksumsnew.txt";

            string dirPath = @"D:\RandomDataFiles";
            

            try
            {
              


              Console.ForegroundColor = ConsoleColor.Yellow;
              Console.WriteLine("*********************Hash sum generator**********************");
              Console.WriteLine("All files in directory {0}:", dirPath);
              CheckSumCreator.GetDirectoriesRec(dirPath);
              Console.WriteLine("\n\n");
              Console.ResetColor();


              //  CheckSumCreator.ShowFileList();


                Console.WriteLine("Choose hash function to proceed files:");
                Console.WriteLine("1.) MD5 Hash");
                Console.WriteLine("2.) SHA-256 Hash");


                int choice = Convert.ToInt32(Console.ReadLine());
                 switch(choice)
                {
                    case 1:
                        HashSumScanner<MD5> md5Scanner = ScanSystem<MD5>;
                        md5Scanner(MD5.Create(), MD5OldPath);
                        break;

                    case 2:
                        HashSumScanner<SHA256> sha256Scanner = ScanSystem<SHA256>;
                        sha256Scanner(SHA256.Create(), SHA256OldPath);
                        break;

                    default: Console.WriteLine("No correct variant has been chosen"); break;


                };

               
                
              

                //while (true)
                //{
     
                //  CheckSumCreator.WriteFileWithCheckSum<MD5>(MD5.Create(), MD5OldPath);
                //    hashContainer.oldFilesWithMD5Hash = CheckSumComparer.ReadFileWithCheckSum(MD5OldPath);
       

                //    CheckSumCreator.WriteFileWithCheckSum<SHA256>(SHA256.Create(), SHA256OldPath);
                //    hashContainer.oldFilesWithSHA256Hash = CheckSumComparer.ReadFileWithCheckSum(SHA256OldPath);

                //    CheckSumCreator.WriteFileWithCheckSum<HashAlgorithm>(HashAlgorithm.Create(), SsdeepOldPath);
                //    hashContainer.oldFilesWithSsdeep = CheckSumComparer.ReadFileWithCheckSum(SsdeepOldPath);

                //    Thread.Sleep(waitTime);

                //    CheckSumCreator.WriteFileWithCheckSum<MD5>(MD5.Create(), MD5NewPath);
                //    hashContainer.newFilesWithMD5Hash = CheckSumComparer.ReadFileWithCheckSum(MD5NewPath);
                //    CheckSumCreator.WriteFileWithCheckSum<SHA256>(SHA256.Create(), SHA256NewPath);
                //    hashContainer.newFilesWithSHA256Hash = CheckSumComparer.ReadFileWithCheckSum(SHA256NewPath);
                //    CheckSumCreator.WriteFileWithCheckSum<HashAlgorithm>(HashAlgorithm.Create(), SsdeepNewPath);
                //    hashContainer.newFilesWithSsdeep = CheckSumComparer.ReadFileWithCheckSum(SsdeepNewPath);

                //    Console.WriteLine("Files with MD5 sum:");
                //    ICollection<KeyValuePair<string, bool>> md5comparisonResult = CheckSumComparer.CompareFilesCheckSum(hashContainer.oldFilesWithMD5Hash, hashContainer.newFilesWithMD5Hash);
                //    Console.WriteLine("Files with SHA256 sum");
                //    ICollection<KeyValuePair<string, bool>> SHA256comparisonResult = CheckSumComparer.CompareFilesCheckSum(hashContainer.oldFilesWithSHA256Hash, hashContainer.newFilesWithSHA256Hash);
                //    Console.WriteLine("Files with Ssdeep");
                //    ICollection<KeyValuePair<string, int>> SsdeepcomparisonResult = CheckSumComparer.CompareFilesSSdeepCheckSum(hashContainer.oldFilesWithSsdeep, hashContainer.newFilesWithSsdeep);
                //   }
 
            }

            catch (Exception e) { Console.WriteLine(e.Message + e.StackTrace); }
        }

        private static void ScanSystem<T>(T algorithm, string path) where T : HashAlgorithm
        {

            HashContainer hashContainer = new HashContainer();
            while (true)
            {
                CheckSumCreator.WriteFileWithCheckSum<T>(algorithm, path);
                hashContainer.oldFilesWithHash = CheckSumComparer.ReadFileWithCheckSum(path);

                Thread.Sleep(waitTime);

                CheckSumCreator.WriteFileWithCheckSum<T>(algorithm, path);
                hashContainer.newFilesWithHash = CheckSumComparer.ReadFileWithCheckSum(path);

                Console.WriteLine("Files were processed by {0} hash: ", algorithm.GetType().Name);
                ICollection<KeyValuePair<string, bool>> comparisonResult = CheckSumComparer.CompareFilesCheckSum(hashContainer.oldFilesWithHash, hashContainer.newFilesWithHash);
                Console.WriteLine("\n\n");
            }
        }


    }




}
