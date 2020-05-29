using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2_nd_ind
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathOriginalFile = @"D:\test.txt";
            string pathEditFile = @"D:\test1.txt";
            int cntOfLines = 0;
            using (var readTest = new StreamReader(pathOriginalFile))
            {
                var writeToSecondFile = new System.IO.StreamWriter(pathEditFile, false);
                while (readTest.Peek() > -1)
                {
                    string line = readTest.ReadLine();
                    writeToSecondFile.WriteLine(line);
                    string line2 = Convert.ToString(line.Length);
                    writeToSecondFile.WriteLine(line2);
                    cntOfLines++;
                }
                writeToSecondFile.Close();
            }
            Console.Write(cntOfLines);
            Console.ReadKey();





        }
    }
}