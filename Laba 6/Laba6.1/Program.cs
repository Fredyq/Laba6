
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1st_ind
{
    class Program
    {
        public int medium = 0;
        static void CopyLast10()
        {
            FileStream fs = new FileStream("sin.dat", FileMode.Open);
            using (BinaryReader br = new BinaryReader(fs))
            {
                using (BinaryWriter bw = new BinaryWriter(File.Open("copy.dat", FileMode.OpenOrCreate)))
                {
                    fs.Seek(-80, SeekOrigin.End);
                    for (int i = 0; i < 100; i++)
                    {
                        bw.Write(br.ReadDouble());
                    }
                }
            }
        }
        
        static void Main(string[] args)
        {
            int medium = 0;
            int cnt = 100;
            FileStream fs = new FileStream("random.dat", FileMode.OpenOrCreate);
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                var rand = new Random();
                for (int i = 0; i < cnt; i++)
                {
                    bw.Write(rand.Next(1,10));
                }
            }
            fs.Close();
            using (BinaryReader br = new BinaryReader(File.OpenRead("random.dat")))
            {
                for (int i = 0; i < cnt; i++)
                {
                  medium += br.ReadInt32();
                }
                Console.WriteLine(medium/cnt);
            }
            FileStream fs2 = new FileStream("end.dat", FileMode.OpenOrCreate);
            using (BinaryWriter bw2 = new BinaryWriter(fs2))
            {
                bw2.Write(medium / cnt);
            }
            using (BinaryReader br2 = new BinaryReader(File.OpenRead("end.dat")))
            {
               Console.WriteLine( br2.ReadInt32());
            }
            Console.ReadKey();

        }
    }
}