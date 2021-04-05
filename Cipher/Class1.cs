using System;
using System.IO;
using System.Linq;

namespace Cipher
{
    public class Cipher
    {
        public void Code(string fileName, int k = 10)
        {
            if (!File.Exists(fileName))
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                        //string _s = "";
                        foreach (var c in s)
                        {
                           
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        public void Decode(string fileName, int k = 10)
        {
            File.Open(fileName, FileMode.Open);   
        }
    }
}