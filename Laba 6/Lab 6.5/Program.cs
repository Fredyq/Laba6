using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace bmpInfo
{
    class Program
    {
        static void Main(string[] args)
        {
                string name = Console.ReadLine();
                string path = @"D:\image\" + name + ".bmp";
                var file_stream = new FileStream(path, FileMode.Open);
                file_stream.Seek(2, SeekOrigin.Begin);
                var br = new BinaryReader(file_stream);
                int size = br.ReadInt32();
                file_stream.Seek(12, SeekOrigin.Current);
                int width = br.ReadInt32();
                int height = br.ReadInt32();
                file_stream.Seek(2, SeekOrigin.Current);
                short bpi = br.ReadInt16();
                int compr = br.ReadInt32();
                string comp = string.Empty;
            switch (compr)
                {
                    case 0:
                        comp = "BI_RGB";
                        break;
                    case 1:
                        comp = "BI_RLE8";
                        break;
                    case 2:
                        comp = "BI_RLE4";
                        break;
                }
            int horizontalResolustion = br.ReadInt32();
            int verticalResolution = br.ReadInt32();
                Console.WriteLine("Размер: {0}\n" +
                    "Ширина: {1}\n" +
                    "Высота: {2}\n" +
                    "Бит/пиксель: {3}\n" +
                    "Тип Сжатия: {4}\n" +
                    "Горизонтальное разрешение: {5}\n" +
                    "Вертикальное разрешение: {6}\n",
                     size, width, height, bpi, comp, horizontalResolustion, verticalResolution);
            Console.WriteLine("Карта цветов:");
            Console.ReadKey();
        }
    }
}

    