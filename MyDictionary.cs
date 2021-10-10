using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSharp_Exam_2021_04_05
{
    [Serializable]
    public class MyDictionary
    {
        public string NameDictionary { set; get; }
        public string TypeDictionary { set; get; }
        public string NameFileDictionary { set; get; }

        public List<MyWord> Words;
        public MyDictionary()
        {
            this.Words = new List<MyWord>();
        }

        public void Print()
        {
            Console.WriteLine($"Name dictionary:      {NameDictionary}");
            Console.WriteLine($"Type dictionary:      {TypeDictionary}");
            Console.WriteLine($"Name file dictionary: {NameFileDictionary}");
            Console.WriteLine();
            if (Words.Count == 0) Console.WriteLine("\nList words is empty!\n");
            for (int i = 0; i < Words.Count; i++)
            {
                Console.WriteLine($"{i+1}: {Words[i].Word}");
                foreach (var item in Words[i].Translations)
                {
                    Console.Write($"[{item}]  ");
                }
                Console.WriteLine("\n");
            }
        }

        

        public void MainMenuDictionary()
        {
            string newWord, newWordTranslation, message;
            bool flagUpdate = false;
            int choiceUser;
            List<string> itemsMainMenuDictionary = new List<string>() 
            { 
                " Перегляд всіх слів словника",
                " Сортування словника по алфавіту",
                " Сортування словника у зворотньому алфавіту порядку",
                " Пошук слова (введення з клавіатури)",
                " Додати нове слово з перекладом",
                " Повернутись до стартового меню програми"
            };
            List<string> infoMainMenuDictionary = new List<string>
            {
                " Назва словника: " + NameDictionary,
                " Тип словника:   " + TypeDictionary,
                " Файл словника:  " + NameFileDictionary
            };
            MyMenu mainMenuDictionary = new MyMenu("ГОЛОВНЕ МЕНЮ РОБОТИ З СЛОВНИКОМ", itemsMainMenuDictionary, 100, infoMainMenuDictionary);

            Console.Title = $"Словник: {NameDictionary} | Тип словника: {TypeDictionary} | Файл словника: {NameFileDictionary}";

            do
            {
                choiceUser = mainMenuDictionary.ChoiceMenu(true, false, ConsoleColor.DarkBlue, ConsoleColor.White);
                switch (choiceUser)
                {
                    case 0:
                        Print();
                        Console.WriteLine("\nДля продовження натисніть будьяку клавішу...");
                        Console.ReadKey();
                        break;
                    case 1:                        
                        Words = Words.OrderBy(x => x.Word).ToList();
                        Print();
                        Console.WriteLine("\nДля продовження натисніть будьяку клавішу...");
                        Console.ReadKey();
                        break;
                    case 2:                        
                        Words = Words.OrderByDescending(x => x.Word).ToList();
                        Print();
                        Console.WriteLine("\nДля продовження натисніть будьяку клавішу...");
                        Console.ReadKey();
                        break;
                    case 3:
                        do
                        {
                            Console.Write("Введіть слово для пошуку: ");
                            newWord = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                        } while (newWord.Length == 0);                        
                        var queryFind = Words.Select(x => x.Word);
                        if (queryFind.Contains(newWord))
                        {
                            MyWord wordFind = Words.Find(x => x.Word == newWord);
                            if (wordFind.MainMenuWord(Words)) flagUpdate = true;
                        }
                        else
                        {
                            message = $"\nУвага! Слово [{newWord}] немає в словнику.\n" +
                                      $"\nДодати до цього слова переклад та зберегти в словнику ?";
                            if (MyMenu.UserReplyToMessage(message, Console.BackgroundColor, ConsoleColor.Red))
                            {
                                do
                                {
                                    Console.Write($"Введіть перший переклад слова [{newWord}] для словника: ");
                                    newWordTranslation = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                                } while (newWordTranslation.Length == 0);
                                Words.Add(new MyWord(newWord, newWordTranslation));
                                Words.Last().Translations = Words.Last().Translations.OrderBy(x => x).ToList();
                                flagUpdate = true;
                            }
                        }
                        break;                    
                    case 4:
                        do
                        {
                            Console.Write("Введіть нове слово для словника: ");
                            newWord = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                        } while (newWord.Length == 0);                       

                        var query = Words.Select(x => x.Word);
                        if (query.Contains(newWord))
                        {
                            message =   $"\nУвага! Слово [{newWord}] вже є в словнику.\n" +
                                        $"\nПродовжити роботу з введеним словом ?";
                            if (MyMenu.UserReplyToMessage(message, Console.BackgroundColor, ConsoleColor.Red))
                            {
                                MyWord word = Words.Find(x => x.Word == newWord);
                                if (word.MainMenuWord(Words)) flagUpdate = true; 
                            }
                        }
                        else
                        {
                            do
                            {
                                Console.Write($"Введіть перший переклад слова [{newWord}] для словника: ");
                                newWordTranslation = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                            } while (newWordTranslation.Length == 0);
                            Words.Add(new MyWord(newWord, newWordTranslation));
                            Words.Last().Translations = Words.Last().Translations.OrderBy(x => x).ToList();
                            flagUpdate = true;
                        }
                        break;
                    default:
                        if (flagUpdate)
                        {
                            LoadSaveProgramSettings.SaveDictionary(this.NameFileDictionary, this);
                        }
                        return;
                        break;
                    
                }

            } while(true);

        }

    }
}
