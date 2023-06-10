using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static task15.Книга;
using System.IO;

namespace task15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Publication> publications = new List<Publication>();
            while (true)
            {
                Console.WriteLine("\n" + $"Меню:\n" +
                                  "     1. Добавить новую книгу в коллекцию.\n" +
                                  "     2. Удалить книгу из колекции.\n" +
                                  "     3. Найти книгу по названию.\n" +
                                  "     4. Сортировка колекции по заданному атрибуту.\n" +
                                  "     5. Вывести коллекцию.\n" +
                                  "     6. Чтение или запись коллекции в файл\n" +
                                  "     7. Выход.\n" + "" 
                                 );
                string choise = Console.ReadLine();
                Console.WriteLine();
                if (choise == "1")
                {
                    string book1 = "";
                    Console.WriteLine("Введите Название:" ); book1 += Console.ReadLine() + ",";
                    Console.WriteLine("Введите автора:"); book1 += Console.ReadLine() + ",";
                    Console.WriteLine("Введите год издания:"); book1 += Console.ReadLine() + ",";
                    Console.WriteLine("Введите жанр:"); book1 += Console.ReadLine() + ",";
                    string[] book = book1.Split(',');
                    int year = Int32.Parse(book[2]);
                    publications.Add(new Book(book[0], book[1], year, book[3]));
                }
                else if (choise == "2")
                {
                    Console.WriteLine("Выберите книгу которую хотите удалить: (введите её номер)\n" + "");
                    Catalog catalog1 = new Catalog(publications);
                    catalog1.DisplayAll();

                    int number = Int32.Parse(Console.ReadLine());

                    catalog1.DeletePublication(number);


                }
                else if (choise == "3")
                {
                    Catalog catalog2 = new Catalog(publications);

                    Console.WriteLine("Введите атрибут для поиска (название, автор или жанр)");   // надо вводить именно атрибут , не цифру

                    string attribute = Console.ReadLine();

                    if (attribute.ToLower() == "автор")
                    {
                        Console.WriteLine("введите автора: ");
                        string attribute2 = Console.ReadLine();
                        catalog2.SearchByAuthor(attribute2);
                    }
                    else if (attribute.ToLower() == "жанр")
                    {
                        Console.WriteLine("Введите жанр: ");
                        string attribute2 = Console.ReadLine();
                        catalog2.SearchByGenre(attribute2);

                    }
                    else if (attribute.ToLower() == "название")
                    {

                        Console.WriteLine("Введите название");
                        string attribute2 = Console.ReadLine();
                        catalog2.SearchByName(attribute2);
                    }

                }
                else if (choise == "4")
                {
                    Catalog catalog2 = new Catalog(publications);

                    catalog2.Sorting();
                }
                else if (choise == "5")
                {
                    Catalog catalog = new Catalog(publications);
                    catalog.DisplayAll();
                }
                else if (choise == "6")
                {
                    Console.WriteLine("что вы хотите?\n" +
                                      "1. Прочитать файл\n" +
                                      "2. Записать файл"
                                     ); string choise1 = Console.ReadLine();
                    if (choise1 == "1")
                    {
                        Catalog catalog1 = new Catalog(publications);
                        catalog1.ReadFile("1.txt");
                    }
                    else if (choise1 == "2") 
                    {
                        Catalog catalog1 = new Catalog(publications);
                        catalog1.WriteInfile();
                    }
                    
                }
                else if (choise == "7") { break; }
            }
        }
    }
}
