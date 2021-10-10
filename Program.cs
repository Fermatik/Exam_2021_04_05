using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CSharp_Exam_2021_04_05
{

    partial class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            MyDictionary curentDictionary = new MyDictionary();

            string nameNewDictionary, typeNewDictionary, nameFileNewDictionary, message;

          

            //Console.WriteLine(LoadSaveProgramSettings.GenerateNewNameFileDictionary());
            //Console.ReadKey();

            //curentDictionary.NameDictionary = "Словарь";
            //curentDictionary.TypeDictionary = "русско-украинский";
            //curentDictionary.NameFileDictionary = "dictionary01.xml";
            //curentDictionary.Words.Add(new MyWord("воскресенье", "неділя"));
            //curentDictionary.Words.Add(new MyWord("понедельник", "понеділок"));

            //curentDictionary.Print();

            //LoadSaveProgramSettings.SaveDictionary("dictionary01.xml", curentDictionary);

            LoadSaveProgramSettings aplicationSettings = new LoadSaveProgramSettings();
            //aplicationSettings.CurrentProgramSettings.CountDictionaries = 2;
            //aplicationSettings.CurrentProgramSettings.Dictionaries.Add(new Dictionary("Словник","українсько-російсьий","dictionary00.xml"));
            //aplicationSettings.CurrentProgramSettings.Dictionaries.Add(new Dictionary("Словарь","русско-украинский","dictionary01.xml"));
            //aplicationSettings.SaveProgramSettings();
            aplicationSettings.LoadProgramSettings();

            //Console.WriteLine(MyMenu.UserReplyToMessage("Добра погода", Console.BackgroundColor, ConsoleColor.Red));

            //return;
            //foreach(var item in aplicationSettings.CurrentProgramSettings.Dictionaries)
            //{
            //    Console.WriteLine(item);
            //}
            int choiceUser, curentIndexDictionary;

            MyMenu exitMenu = new MyMenu("Закінчити роботу програми ?", new List<string>() { " Tak", " Ні" }, 30, new List<string>(), MyMenu.AlignmentTypes.Midle, MyMenu.AlignmentTypes.Midle);


            List<string> itemsStartMenu = new List<string> { " Створити новий словник", " Відкрити словник", " Вийти з програми" };
            MyMenu startMenu = new MyMenu("СТАРТОВЕ МЕНЮ ПРОГРАМИ", itemsStartMenu, 100, new List<string>());
            startMenu.SetInfoMenu(new List<string>() 
            {   
                " Інформація для користувача програми:",
                "   Для обрання потрібного пункту меню використовуйте клавіши стрілок [ Up, Down ]",
                "   Підтвердження вибоу пункту натисніть клавішу [ Enter ]",
                "   Повернутись до попереднього меню натисніть клавішу [ Esc ]"
            });
            List<string> itemsOpenDictionaryMenu = new List<String>();
            MyMenu openDictionaryMenu = new MyMenu("ВИБІР СЛОВНИКА ДЛЯ РОБОТИ:", itemsOpenDictionaryMenu, 80, new List<string>());

            

            do
            {        
                Console.Title = "Програма роботи з словниками";        
                choiceUser = startMenu.ChoiceMenu(true, false);
                
                switch (choiceUser)
                {
                    case 0:
                        nameFileNewDictionary = LoadSaveProgramSettings.GenerateNewNameFileDictionary();
                        if (nameFileNewDictionary==null)
                        {
                            Console.WriteLine($"Увага! Програма не може автоматично згенерувати нову назву файлу.");
                            Console.WriteLine($"Створення новго словника неможливе!");
                            Console.WriteLine("Для продовження натисніть будь яку клавішу...");
                            Console.ReadKey(true);
                            continue;
                        }
                        Console.WriteLine("Створення нового словника:");
                        Console.Write("Введіть назву словника: ");
                        nameNewDictionary = Console.ReadLine();
                        Console.Write("Введіть тип словника: ");
                        typeNewDictionary = Console.ReadLine();

                        message = $"\nНазва нового словника:                                 [{nameNewDictionary}]\n" +
                                  $"Тип нового словника:                                   [{typeNewDictionary}]\n" +
                                  $"Назва файлу нового словника (сгенеровано автоматично): [{nameFileNewDictionary}]\n" +
                                  $"Ствоорити новий словник з вказаними параметрами:";
                        if (MyMenu.UserReplyToMessage(message, Console.BackgroundColor, ConsoleColor.Red))
                        {
                            curentDictionary = new MyDictionary();
                            curentDictionary.NameDictionary = nameNewDictionary;
                            curentDictionary.TypeDictionary = typeNewDictionary;
                            curentDictionary.NameFileDictionary = nameFileNewDictionary;
                            LoadSaveProgramSettings.SaveDictionary(nameFileNewDictionary, curentDictionary);
                            aplicationSettings.CurrentProgramSettings.Dictionaries.Add(new Dictionary(nameNewDictionary, typeNewDictionary, nameFileNewDictionary));
                            aplicationSettings.SaveProgramSettings();
                            message = $"Відкрити новостворений словник ? ";
                            if (MyMenu.UserReplyToMessage(message, Console.BackgroundColor, ConsoleColor.Gray))
                            {
                                curentDictionary.MainMenuDictionary();
                            }
                        }                                              
                        break;

                    case 1:
                        itemsOpenDictionaryMenu = aplicationSettings.CurrentProgramSettings.Dictionaries.
                            Select(x => $" {MyMenu.StringToWidthLeft(x.NameDictionary,25)} {MyMenu.StringToWidthLeft(x.TypeDictionary, 25)} {MyMenu.StringToWidthRight(x.NameFileDictionary, 25)}").
                            ToList();
                        openDictionaryMenu.SetItemsMenu(itemsOpenDictionaryMenu);
                        choiceUser = openDictionaryMenu.ChoiceMenu(false, false);                 
                        if (choiceUser < 0) continue;
                        curentIndexDictionary = choiceUser;
                        curentDictionary = LoadSaveProgramSettings.LoadDictionary(aplicationSettings.CurrentProgramSettings.Dictionaries[curentIndexDictionary].NameFileDictionary);
                        if (curentDictionary==null)
                        {
                            Console.WriteLine($"Увага! Файл словника [{aplicationSettings.CurrentProgramSettings.Dictionaries[curentIndexDictionary].NameFileDictionary}] не знайдено!");
                            Console.WriteLine("Для продовження натисніть будь яку клавішу...");
                            Console.ReadKey(true);
                            continue;
                        }
                        curentDictionary.MainMenuDictionary();


                        break;
                    default:
                        choiceUser = exitMenu.ChoiceMenu(false, false, ConsoleColor.Red, ConsoleColor.White);
                        if (choiceUser==0)
                        {
                 
                            return;

                        }
                        break;
                }

            } while (true);

        }
    }
}
