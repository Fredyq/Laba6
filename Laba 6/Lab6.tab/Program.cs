using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tab
{
    class Program
    {
        enum bookType
        {
            Х,
            У,
            С,
            none
        }
        enum Action
        {
            ADD,
            DELETE,
            UPDATE
        }
        struct book
        {
            public string author;
            public bookType type;
            public string name;
            public uint year;

            public book(string name, char type, string author, uint year)
            {
                this.author = author;
                this.type = GetTypebook(type);
                this.name = name;
                this.year = year;
            }
            public void DisplayInfo()
            {
                Console.WriteLine("{0,-20} {1,-15} {2,-20} {3,-10}", author, year, name, type);
            }
        }
        struct Log
        {
            public DateTime time;
            public Action action;
            public string name;

            public void DisplayLog()
            {
                switch (action)
                {
                    case Action.ADD:
                        Console.WriteLine($"{time.ToLongTimeString()} - Добавлена запись \"{name}\"");
                        break;
                    case Action.DELETE:
                        Console.WriteLine($"{time.ToLongTimeString()} - Удалена   запись \"{name}\"");
                        break;
                    case Action.UPDATE:
                        Console.WriteLine($"{time.ToLongTimeString()} - Обновлена запись \"{name}\"");
                        break;
                }
            }
        }

        static void CalcIdleTime(DateTime dt1, TimeSpan old_idle_time, out TimeSpan idle_time)
        {
            DateTime dt2 = DateTime.Now;
            TimeSpan time = dt2 - dt1;
            if (time > old_idle_time)
            {
                idle_time = time;
            }
            else
            {
                idle_time = old_idle_time;
            }
        }

        static int GetNumber(int left_limit, int right_limit)
        {
            int select = Int32.MinValue;
            while (!(select >= left_limit && select <= right_limit))
            {
                Console.Write($"Введите значение от {left_limit} до {right_limit}:");
                try
                {
                    select = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Попробуйте заново!");
                }
            }
            return select;
        }
        static bookType GetTypebook()
        {
            
            string input = Console.ReadLine();
            char type = input[0];

            if (type == 'Х' || type == 'х' || type == 'X' || type == 'x')
            {
                return bookType.Х;
            }
            else if (type == 'У' || type == 'у' ||type == 'Y' || type == 'y')
            {
                return bookType.У;
            }
            else if (type == 'С' || type == 'с' || type == 'C' || type == 'c')
            {
                return bookType.У;
            }
            else
            {
                return bookType.none;
            }
        }
        static bookType GetTypebook(char type)
        {
            if (type == 'Х' || type == 'х' || type == 'X' || type == 'x')
            {
                return bookType.Х;
            }
            else if (type == 'У' || type == 'у' || type == 'Y' || type == 'y')
            {
                return bookType.У;
            }
            else if (type == 'С' || type == 'с' || type == 'C' || type == 'c')
            {
                return bookType.С;
            }
            else
            {
                return bookType.none;
            }
        }
        static decimal GetPrice(decimal left_limit, decimal right_limit)
        {
            decimal price = Decimal.MinValue;
            while (!(price >= left_limit && price <= right_limit))
            {
                Console.Write($"Введите значение от {left_limit} до {right_limit}:");
                try
                {
                    price = Convert.ToDecimal(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Попробуйте заново!");
                }
            }
            return price;
        }
        static void ShowMenu()
        {
            Console.WriteLine("1 – Просмотр таблицы\n" +
                              "2 – Добавить запись\n" +
                              "3 – Удалить запись\n" +
                              "4 – Обновить запись\n" +
                              "5 – Поиск записей\n" +
                              "6 – Просмотреть лог\n" +
                              "7 - Выход");
        }
        static void ShowTab(book[] books)
        {
            string s = " ";
            Console.WriteLine(s);
            Console.WriteLine("Библиотека");
            Console.WriteLine(s);
            Console.WriteLine("{0,-20} {1,-15} {2,-20} {3,-10}", "Автор", "Год", "Название", "Тип");
            Console.WriteLine(s);
            for (int i = 0; i < books.Length; i++)
            {
                books[i].DisplayInfo();
                Console.WriteLine(s);
            }
            Console.WriteLine("Типы:Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра\t\t\t       |");
            Console.WriteLine(s);
        }
        static void ShowLog(Log[] logs, TimeSpan idle_time)
        {
            for (int i = 0; i < logs.Length; i++)
            {
                if (logs[i].name != null)
                {
                    logs[i].DisplayLog();
                }
            }
            Console.WriteLine($"\n{idle_time.Hours:00}:{idle_time.Minutes:00}:{idle_time.Seconds:00} - Самый долгий период бездействия пользователя");
        }
        static void AddNote(ref book[] books, ref Log[] logs, ref int cnt)
        {
            Array.Resize(ref books, books.Length + 1);
            int last_pos = books.Length - 1;

            Console.Write("Введите автора книги:");
            books[last_pos].author = Console.ReadLine();
            Console.WriteLine("Укажите год издания");
            books[last_pos].year = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine("Укажите название");
            books[last_pos].name = Console.ReadLine();
            Console.WriteLine("Укажите тип (Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра)");
            books[last_pos].type = GetTypebook();
            AddLog(ref logs, DateTime.Now, 0, books[last_pos].name, ref cnt);
            
        }
        static void AddNote(ref book[] books, string[] data)
        {
            bool match = false;
            for (int i = 0; i < books.Length; i++)
            {
                if (data[0] == books[i].name)
                {
                    match = true;
                }
            }
            if (!match)
            {
                Array.Resize(ref books, books.Length + 1);
                books[books.Length - 1] = new book(data[0], Convert.ToChar(data[1]),Convert.ToString(data[2]), Convert.ToUInt32(data[3]));
            }
        }
        static void DeleteNote(ref book[] books, ref Log[] logs, ref int cnt)
        {
            Console.WriteLine("Укажите номер книги для удаления");
            int number = GetNumber(0, books.Length - 1);
            AddLog(ref logs, DateTime.Now, 1, books[number].name, ref cnt);
            for (int i = number; i < books.Length - 1; i++)
            {
                books[i] = books[i + 1];
            }
            Array.Resize(ref books, books.Length - 1);
        }
        static void UpdateNote(ref book[] books, ref Log[] logs, ref int cnt)
        {
            Console.WriteLine("Укажите номер записи, которую хотите обновить");
            int number = GetNumber(0, books.Length - 1);
            AddLog(ref logs, DateTime.Now, 2, books[number].name, ref cnt);
            Console.Write("Введите автора книги:");
            books[number].author = Console.ReadLine();
            Console.WriteLine("Укажите цену за 1шт (грн)");
            books[number].year = Convert.ToUInt16(Console.ReadLine());
            Console.WriteLine("Укажите количество");
            books[number].name = Console.ReadLine();
            books[number].type = GetTypebook();
        }
        static void SearchNotes(book[] books)
        {
            Console.WriteLine("Введите тип книги для поиска");
            char searchtype = Convert.ToChar(Console.ReadLine());
            string s = " ";
            if (searchtype == 'Х' || searchtype == 'У' || searchtype == 'С')
            {
                Console.WriteLine(s);
                Console.WriteLine("Библиотека\t\t\t\t\t\t\t\t       |");
                Console.WriteLine(s);
                Console.WriteLine("Автор \tГод издания \tНазвание \tТип   ");
                Console.WriteLine(s);
                for (int i = 0; i < books.Length; i++)
                {

                    if (Convert.ToChar(books[i].type) == searchtype)
                    {
                        books[i].DisplayInfo();
                        Console.WriteLine(s);
                    }
                }
                Console.WriteLine("Перечисляемый тип: Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра\t\t\t       ");
                Console.WriteLine(s);


            }
            else
            {
                Console.WriteLine("Укажите верный тип (Х-худож. лит-ра,У-учеб. лит-ра,С-справочная лит-ра)");
                
            }

            }
            static void AddLog(ref Log[] logs, DateTime time, int action, string name, ref int cnt)
        {
            if (cnt > 49)
            {
                for (int i = 0; i < logs.Length - 1; i++)
                {
                    logs[i] = logs[i + 1];
                }
                cnt = 49;
            }
            logs[cnt].time = time;
            logs[cnt].action = (Action)action;
            logs[cnt].name = name;

            cnt++;
        }
        static void Main(string[] args)
        {
            book[] books = new book[3];
            Log[] logs = new Log[50];
            books[0].author = "Санкевич";
            books[0].year = 1978;
            books[0].name = "Потоп";
            books[0].type = (bookType)0;

            books[1].author = "Ландау";
            books[1].year = 1989;
            books[1].name = "Механика";
            books[1].type = (bookType)1;

            books[2].author = "Дойль";
            books[2].year = 1990;
            books[2].name = "Сумчатые";
            books[2].type = (bookType)2;
            int cnt = 0;
            try
            {
                using (StreamReader sr = new StreamReader(@"D\lab.dat"))
                {
                    int length = Convert.ToInt32(sr.ReadLine());
                    for (int i = 0; i < length; i++)
                    {
                        string[] data = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (data.Length == 4)
                        {
                            AddNote(ref books, data);
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine("Файл не обнаружен!\n");
            }
            int select = 0;
            bool exit = false;
            TimeSpan idle_time = TimeSpan.Zero;
            DateTime time = DateTime.Now;
            while (!exit)
            {
                switch (select)
                {
                    case 1:
                        ShowTab(books);
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case 2:
                        AddNote(ref books, ref logs, ref cnt);
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case 3:
                        DeleteNote(ref books, ref logs, ref cnt);
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case 4:
                        UpdateNote(ref books, ref logs, ref cnt);
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case 5:
                        SearchNotes(books);
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case 6:
                        Console.Clear();
                        ShowLog(logs, idle_time);
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                    case 7:
                        using (StreamWriter sw = new StreamWriter(@"D:\Lab6.tab", append: false))
                        {
                            sw.WriteLine(books.Length);
                            for (int i = 0; i < books.Length; i++)
                            {
                                sw.WriteLine("{0,-20} {1,-15} {2,-20} {3,-10}", books[i].author, books[i].year, books[i].name, books[i].type);
                            }
                        }
                        Console.WriteLine("Данные сохранены...");
                        exit = true;
                        break;
                    default:
                        CalcIdleTime(time, idle_time, out idle_time);
                        time = DateTime.Now;
                        ShowMenu();
                        select = GetNumber(1, 7);
                        break;
                }
            }
        }
    }
}