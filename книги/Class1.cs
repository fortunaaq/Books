using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Data;
using System.Linq;
using static task15.Книга;
using System.Xml.Linq;

namespace task15
{
    internal class Книга
    {
        public abstract class Publication
        {
            public string Name { get; set; }                 //свойства
            public string AuthorLastName { get; set; }

            public Publication(string name, string authorLastName)
            {
                Name = name;
                AuthorLastName = authorLastName;
            }

            public abstract void DisplayInfo();        //создаем методы
            public abstract string GiveInfo();
            public abstract string[] GiveInfo2();

            public abstract bool IsSearchable(string searchQuery);
            public abstract bool IsSearchable2(string searchQuery);
            public abstract bool IsSearchable3(string searchQuery);

        }
        public class Book : Publication
        {
            public int Year { get; set; }        //свойства
            public string Genre { get; set; }

            public Book(string name, string authorLastName, int year, string genre)
                : base(name, authorLastName)
            {
                Year = year;
                Genre = genre;
            }

            public override void DisplayInfo()                 //вывести данные книги
            {
                Console.WriteLine($"Книга \"{Name}\", автор: {AuthorLastName}, " +
                                  $"год_издания: {Year}, жанр: {Genre}");
            }
            public override string GiveInfo()                  // метод для записи в файл
            {
                string info = $" Книга {Name}," + $" автор: {AuthorLastName}," +
                                  $" год_издания: {Year}," + $" жанр: {Genre}";
                return info;
            }
            public override string[] GiveInfo2()                      // получить данные книги для обработки    
            {
                string[] info = new string[4] {Name, AuthorLastName, Convert.ToString(Year), Genre };
                return info;
            }


            public override bool IsSearchable(string searchQuery)
            {
                return AuthorLastName.ToLower().Contains(searchQuery.ToLower());
            }
            public override bool IsSearchable2(string searchQuery)
            {
                return Genre.ToLower().Contains(searchQuery.ToLower());
            }
            public override bool IsSearchable3(string searchQuery)             
            {
                return Name.ToLower().Contains(searchQuery.ToLower());
            }
        }

            public class Catalog                                   //создаем класс для работы с коллекцией
        {
                public List<Publication> publications;

                public Catalog(List<Publication> publications)
                {
                    this.publications = publications;
                }

                public void DisplayAll()                    //выводит все книги из коллекции
            {
                    int i = 1;
                    foreach (Publication publication in publications)
                    {
                    Console.Write(" " + i + ". "); publication.DisplayInfo();
                    i++;
                    }
                }

                public void SearchByAuthor(string authorLastName)                //ищет книги по автору
            {
                    List<Publication> foundPublications = new List<Publication>();

                    foreach (Publication publication in publications)
                    {
                        if (publication.IsSearchable(authorLastName))
                        {
                            foundPublications.Add(publication);
                        }
                    }

                    if (foundPublications.Count == 0)
                    {
                        Console.WriteLine("Ничего не найдено");
                    }
                    else
                    {
                        Console.WriteLine($"Найдено {foundPublications.Count} изданий:");
                        foreach (Publication publication in foundPublications)
                        {
                            publication.DisplayInfo();
                        }
                    }
                }
                public void SearchByGenre(string genre)            //ищет книги по жанру
            {
                    List<Publication> foundPublications = new List<Publication>();

                    foreach (Publication publication in publications)
                    {
                        if (publication.IsSearchable2(genre))
                        {
                            foundPublications.Add(publication);
                        }
                    }

                    if (foundPublications.Count == 0)
                    {
                        Console.WriteLine("Ничего не найдено");
                    }
                    else
                    {
                        Console.WriteLine($"Найдено {foundPublications.Count} изданий:");
                        foreach (Publication publication in foundPublications)
                        {
                            publication.DisplayInfo();
                        }
                    }
                }
                public void SearchByName(string name)            //ищет книгу по её названию
            {
                    List<Publication> foundPublications = new List<Publication>();

                    foreach (Publication publication in publications)
                    {
                        if (publication.IsSearchable3(name))
                        {
                            foundPublications.Add(publication);
                        }
                    }

                    if (foundPublications.Count == 0)
                    {
                        Console.WriteLine("Ничего не найдено");
                    }
                    else
                    {
                        Console.WriteLine($"Найдено {foundPublications.Count} изданий:");
                        foreach (Publication publication in foundPublications)
                        {
                            publication.DisplayInfo();
                        }
                    }
                }
                public void DeletePublication(int number)              // удаляет книгу из коллекции
                {
                    if (number <= publications.Count & number > 0)
                        {
                            publications.RemoveAt(number - 1);
                        }
                    else
                        {
                            Console.WriteLine("Ошибка такого номера нет!");
                        }
                }
            public void ReadFile(string lines)                              //прочитать данные из файла
                                                                            //(сначала удаляет текущую коллекцию а потом читает и создает новую)
            {
                List<string> InfoForBook = new List<string>();
                List<Publication> publications1 = new List<Publication>();
                string word = "";
                string[] file = File.ReadAllLines(lines);
                foreach (string line in file)
                {
                    string[] split2 = line.Split(',');
                    foreach (string item in split2)
                    {

                        string[] split3 = item.Split(' ');
                        for (int i = 2; i + 1 < split3.Length; i++)
                        {

                            word += split3[i] + " ";
                        }
                        word += split3.Last();

                        InfoForBook.Add(word);
                        word = "";
                    }

                    int Year = Int32.Parse(InfoForBook[2]);
                    publications1.Add(new Book(InfoForBook[0], InfoForBook[1], Year, InfoForBook[3]));
                    InfoForBook.RemoveAt(0); InfoForBook.RemoveAt(0); InfoForBook.RemoveAt(0); InfoForBook.RemoveAt(0);
                }
                publications.Clear();
                foreach (var item in publications1) 
                    {
                    publications.Add(item);
                    }
                }
                public void WriteInfile()                             //записать данные в файл
            {
                    string text = "";
                    foreach (var item in publications)
                    {
                        string item1 = item.GiveInfo();
                        text += item1 + "\n";
                    }
                    File.WriteAllText("1.txt", text);

                }
            public void Sorting()                                                   //сортировка пузырьком по году издания
                {
                int k = 0;
                foreach (Publication publication in publications) { k++; }

                for (int i = 0; i  < k; i++)
                    {
                        for (int j = 0; j < k - i - 1; j++)
                        {

                            if (Int32.Parse(publications[j].GiveInfo2()[2]) < Int32.Parse(publications[j + 1].GiveInfo2()[2]))
                            {
                            var temp = publications[j];
                            publications[j] = publications[j + 1];
                            publications[j+1] = temp;
                            }
                        }
                    }
                }



            }


    }
}
