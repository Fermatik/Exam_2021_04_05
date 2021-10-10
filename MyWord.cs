using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace CSharp_Exam_2021_04_05
{
    [Serializable]
    public class MyWord
    {
        private string word;        
        public List<string> Translations;

        public int CountTranslations { get => this.Translations.Count; }

        public string Word
        {
            set => this.word = Regex.Replace(value.Trim(), "\\s+", " ");
            get => this.word;
        }
        public MyWord()
        {
            this.Word = "";
            this.Translations = new List<string>();
        }
        public MyWord(string word, string firstTranslations)
        {
            this.Word = word;
            this.Translations = new List<string>();
            this.Translations.Add(firstTranslations);
        }

        public bool MainMenuWord(List<MyWord> words)
        {
            bool flagUpdate = false;
            List<string> itemsMainMenuWord = new List<string>()
            {
                " Редагувати слово",
                " Редагувати переклад",
                " Видалити слово",
                " Видалити переклад",
                " Додати новий переклад",
                " Повернутись до головного меню словника"
            };
            List<string> infoMainMenuWord = new List<string>();
            infoMainMenuWord.Add($" Слово: [{Word}]");
            infoMainMenuWord.Add($" Переклади: ");
            foreach (var item in Translations)
            {
                infoMainMenuWord.Add($" [{item}]");
            }
            
            MyMenu mainMenuWord = new MyMenu("ГОЛОВНЕ МЕНЮ РОБОТИ З СЛОВОМ", itemsMainMenuWord, 100, infoMainMenuWord);

            List<string> itemsListTranslations = new List<string>();
            foreach (var item in Translations)
            {
                itemsListTranslations.Add(" " + item);
            }
            List<string> infoListTranslation = new List<string>() { $"Слово з словника: [{Word}]"};
            MyMenu listTranslations = new MyMenu("Вибір перекладу слова:", itemsListTranslations,100, infoListTranslation, MyMenu.AlignmentTypes.Midle, MyMenu.AlignmentTypes.Midle);
            
            int choiceUser;

            string newValueWord, newValueTranslation;

            do
            {
                choiceUser = mainMenuWord.ChoiceMenu(true, false, ConsoleColor.Green, ConsoleColor.White);
                switch (choiceUser)
                {
                    case 0:
                        do
                        {
                            Console.Write($"Введіть нову редакцію слова [{Word}]: ");
                            newValueWord = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                        } while (newValueWord.Length==0);
                        
                        var query = words.Select(x => x.Word).ToList();
                        if (query.Contains(newValueWord))
                        {
                            Console.WriteLine($"Увага! Введена редакція слова [{newValueWord}] вже є у словнику!") ;
                            Console.WriteLine($"Введену редакцію неможливо використовувати для редагування слова!");
                            Console.WriteLine($"Для продовження натисніть будьяку клавішу...");
                            Console.ReadKey();
                        }
                        else
                        {
                            this.Word = newValueWord;
                            infoMainMenuWord = new List<string>();
                            infoMainMenuWord.Add($" Слово: [{Word}]");
                            infoMainMenuWord.Add($" Переклади: ");
                            foreach (var item in Translations)
                            {
                                infoMainMenuWord.Add($" [{item}]");
                            }
                            mainMenuWord.SetInfoMenu(infoMainMenuWord);

                            infoListTranslation = new List<string>() { $"Слово з словника: [{Word}]" };
                            listTranslations.SetInfoMenu(infoListTranslation,MyMenu.AlignmentTypes.Midle);
                            flagUpdate = true;
                        }
                        break;
                    case 1:
                        if (Translations.Count==1)
                        {
                            do
                            {
                                Console.Write($"Введіть нову редакцію перекладу [{Translations[0]}]: ");
                                newValueTranslation = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                            } while (newValueTranslation.Length == 0);
                            Translations[0] = newValueTranslation;
                            infoMainMenuWord = new List<string>();
                            infoMainMenuWord.Add($" Слово: [{Word}]");
                            infoMainMenuWord.Add($" Переклади: ");
                            foreach (var item in Translations)
                            {
                                infoMainMenuWord.Add($" [{item}]");
                            }
                            mainMenuWord.SetInfoMenu(infoMainMenuWord);
                            itemsListTranslations = new List<string>();
                            foreach (var item in Translations)
                            {
                                itemsListTranslations.Add(" " + item);
                            }
                            listTranslations.SetItemsMenu(itemsListTranslations);
                            flagUpdate = true;
                        }
                        else
                        {
                            choiceUser = listTranslations.ChoiceMenu(false, false, ConsoleColor.Red, ConsoleColor.White);
                            if (choiceUser < 0) continue;
                            do
                            {
                                Console.Write($"Введіть нову редакцію перекладу [{Translations[0]}]: ");
                                newValueTranslation = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                            } while (newValueTranslation.Length == 0);
                            Translations[choiceUser] = newValueTranslation;
                            infoMainMenuWord = new List<string>();
                            infoMainMenuWord.Add($" Слово: [{Word}]");
                            infoMainMenuWord.Add($" Переклади: ");
                            foreach (var item in Translations)
                            {
                                infoMainMenuWord.Add($" [{item}]");
                            }
                            mainMenuWord.SetInfoMenu(infoMainMenuWord);
                            itemsListTranslations = new List<string>();
                            foreach (var item in Translations)
                            {
                                itemsListTranslations.Add(" " + item);
                            }
                            listTranslations.SetItemsMenu(itemsListTranslations);
                            flagUpdate = true;
                        }
                        break;
                    case 2:
                        if (MyMenu.UserReplyToMessage($" Видалити слово [{Word}] з словника ?", Console.BackgroundColor, ConsoleColor.Red))
                        {
                            words.Remove(this);
                            return true;
                        }
                        break;
                    case 3:
                        if (Translations.Count == 1)
                        {
                            Console.WriteLine($"Увага! Слово [{Word}] має єдиний переклад [{Translations[0]}] !") ;
                            Console.WriteLine("Видалення єдиного перекладу неможливе!");
                            Console.WriteLine("Для продовження натисніть будьяку клавішу...");
                            Console.ReadKey(true);

                        }
                        else
                        {
                            choiceUser = listTranslations.ChoiceMenu(false, false, ConsoleColor.DarkRed, ConsoleColor.White);
                            if (choiceUser < 0) continue;

                            if (MyMenu.UserReplyToMessage($" Видалити переклад [{Translations[choiceUser]}] ?", Console.BackgroundColor, ConsoleColor.Red))
                            {
                                Translations.RemoveAt(choiceUser);                            
                                infoMainMenuWord = new List<string>();
                                infoMainMenuWord.Add($" Слово: [{Word}]");
                                infoMainMenuWord.Add($" Переклади: ");
                                foreach (var item in Translations)
                                {
                                    infoMainMenuWord.Add($" [{item}]");
                                }
                                mainMenuWord.SetInfoMenu(infoMainMenuWord);
                                itemsListTranslations = new List<string>();
                                foreach (var item in Translations)
                                {
                                    itemsListTranslations.Add(" " + item);
                                }
                                listTranslations.SetItemsMenu(itemsListTranslations);
                                flagUpdate = true;
                            }

                        }
                        break;
                    case 4:
                        do
                        {
                            Console.Write($"Введіть переклад слова [{Word}] який треба додати до списку: ");
                            newValueTranslation = Regex.Replace(Console.ReadLine().Trim(), "\\s+", " ");
                        } while (newValueTranslation.Length == 0);
                        Translations.Add(newValueTranslation);
                        infoMainMenuWord = new List<string>();
                        infoMainMenuWord.Add($" Слово: [{Word}]");
                        infoMainMenuWord.Add($" Переклади: ");
                        foreach (var item in Translations)
                        {
                            infoMainMenuWord.Add($" [{item}]");
                        }
                        mainMenuWord.SetInfoMenu(infoMainMenuWord);
                        itemsListTranslations = new List<string>();
                        foreach (var item in Translations)
                        {
                            itemsListTranslations.Add(" " + item);
                        }
                        listTranslations.SetItemsMenu(itemsListTranslations);
                        flagUpdate = true;
                        break;

                    default:
                        return flagUpdate;
                        break;
                }

            } while (true);
        }
    }
}
