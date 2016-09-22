using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HashCounterLibrary
{
    public static class RandomDataGenerator
    {
      public static void CreateFileWithPseudoRandomData(double [] sizes, string [] path)
        {
            const int preffixDecreaser = 1024;
            const string randAlphabet = "abcdfghijklmnopqrstuvwxyz1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();

            for (int i = 0; i < path.Length; i++)
            {
                using (StreamWriter sw = File.CreateText(path[i]))
                {

                    for (int j = 0; j < sizes[i] * preffixDecreaser * preffixDecreaser; j++)
                        sw.Write(randAlphabet[rand.Next(randAlphabet.Length)]);

                }
            }
        }
    }
}
