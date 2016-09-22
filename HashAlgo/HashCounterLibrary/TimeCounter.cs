using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;



namespace HashCounterLibrary
{
    public static class TimeCounter
    {
  
        public static TimeSpan CountTime<T>(T hashType, string input) where T : HashAlgorithm
        {
            string countedHash;
            HashCounter.GetHash hashFunction;
            HashCounter.InitHashFunction<T>(hashType, out hashFunction);
            
                  Stopwatch sw1 = new Stopwatch();

                    sw1.Start();
                    countedHash = hashFunction(input);
                    sw1.Stop();

                    return sw1.Elapsed;
        }
    }
}
