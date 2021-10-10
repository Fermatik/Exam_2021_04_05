using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Exam_2021_04_05
{
    class MyMenu
    {
        public enum AlignmentTypes
        {
            Left,
            Right,
            Midle
        }

        const int MIN_WIDTH_MENU = 10;
        const int MAX_WIDTH_MENU = 100;
        
        private int widthMenu;

        private List<string> itemsMenu;
        private List<string> infoMenu;

        private string nameMenu;
        public MyMenu(string nameMenu, int widthMenu)
        {
            this.itemsMenu = new List<string>();
            this.infoMenu = new List<string>();
            this.WidthMenu = widthMenu;
            this.nameMenu = StringToWhidthMidle(nameMenu, this.WidthMenu);
        }
        public MyMenu(
            string nameMenu, List<string> itemsMenu, int widthMenu, List<string> infoMenu, 
            AlignmentTypes alignmentTypesItemsMenu = AlignmentTypes.Left, AlignmentTypes alignmentTypesInfoMenu = AlignmentTypes.Left)
        {
            this.WidthMenu = widthMenu;
            this.nameMenu = StringToWhidthMidle(nameMenu, this.WidthMenu);            
            this.itemsMenu = new List<string>();
            SetItemsMenu(itemsMenu, alignmentTypesItemsMenu);
            this.infoMenu = new List<string>();
            SetInfoMenu(infoMenu, alignmentTypesInfoMenu);
        }
        
        public void SetItemsMenu(List<string> itemsMenu, AlignmentTypes alignmentTypes = AlignmentTypes.Left)
        {
            this.itemsMenu.Clear();
            foreach (var item in itemsMenu)
            {
                switch (alignmentTypes)
                {
                    case AlignmentTypes.Left:
                        this.itemsMenu.Add(StringToWidthLeft(item, this.WidthMenu));
                        break;
                    case AlignmentTypes.Right:
                        this.itemsMenu.Add(StringToWidthRight(item, this.WidthMenu));
                        break;
                    case AlignmentTypes.Midle:
                        this.itemsMenu.Add(StringToWhidthMidle(item, this.WidthMenu));
                        break;
                }
                
            }
        }

        public void SetInfoMenu(List<string> infoMenu, AlignmentTypes alignmentTypes = AlignmentTypes.Left)
        {
            this.infoMenu.Clear();
            foreach (var item in infoMenu)
            {
                switch (alignmentTypes)
                {
                    case AlignmentTypes.Left:
                        this.infoMenu.Add(StringToWidthLeft(item, this.WidthMenu));
                        break;
                    case AlignmentTypes.Right:
                        this.infoMenu.Add(StringToWidthRight(item, this.WidthMenu));
                        break;
                    case AlignmentTypes.Midle:
                        this.infoMenu.Add(StringToWhidthMidle(item, this.WidthMenu));
                        break;
                }

            }
        }

        public int WidthMenu
        {
            private set => this.widthMenu = (value < MIN_WIDTH_MENU ? MIN_WIDTH_MENU : (value <= MAX_WIDTH_MENU ? value : MAX_WIDTH_MENU));
            get => this.widthMenu;
        }

        public int CountItemsMenu
        {
            get => this.itemsMenu.Count;
        }

        private readonly char[] border =   
        {
            '╭', '┬', '╮',
            '├', '┼', '┤',
            '╰', '┴', '╯'
        };
        const char hor = '─', ver = '│';

        public static string StringToWidthLeft(string s, int width)
        {
            string result;
            if (s.Length >= width) result = s.Substring(0, width);
            else result = s + new string(' ', width - s.Length);
            return result;
        }

        public static string StringToWidthRight(string s, int width)
        {
            string result;
            if (s.Length >= width) result = s.Substring(0, width);
            else result = new string(' ', width - s.Length) + s;
            return result;
        }

        public static string StringToWhidthMidle(string s, int width)
        {
            string result = s.Trim();
            if (result.Length >= width) result = result.Substring(0, width);
            else result = StringToWidthLeft(new string(' ', (width - result.Length) / 2) + result, width);
            return result;
        }



        public static bool UserReplyToMessage(string message, ConsoleColor colorBackgroundMessage, ConsoleColor colorForegroundMessage )
        {
            bool stop = false;
            bool result = false;
            bool saveCursorVisible = Console.CursorVisible;
            ConsoleColor saveBackgroundColor = Console.BackgroundColor;
            ConsoleColor saveForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = saveBackgroundColor;
            Console.ForegroundColor = saveForegroundColor;

            Console.CursorVisible = false;
            Console.BackgroundColor = colorBackgroundMessage;
            Console.ForegroundColor = colorForegroundMessage;

            Console.WriteLine(message);
            Console.WriteLine("  Так  - натисніть клавішу [ Y ]");
            Console.WriteLine("  Hi   - натисніть клавішу [ N ]");

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.Y:
                        result = true;
                        stop = true;
                        break;
                    case ConsoleKey.N:
                        result = false;
                        stop = true;
                        break;
                }

            } while (!stop);

            Console.CursorVisible = saveCursorVisible;
            Console.BackgroundColor = saveBackgroundColor;
            Console.ForegroundColor= saveForegroundColor;
            return result;
        }


        public int ChoiceMenu(bool FirstNameMenu = true, bool stopRange = true, ConsoleColor activBackgroundColor = ConsoleColor.Gray, ConsoleColor activForegroundColor = ConsoleColor.Black)
        {
            Console.Clear();           

            ConsoleColor saveBackgroundColor = Console.BackgroundColor;
            ConsoleColor saveForegroundColor = Console.ForegroundColor;
            bool saveCursorVisible = Console.CursorVisible;
                
            
            Console.CursorVisible = false;
            
            string horizont1 = border[0] + new string(hor, WidthMenu) + border[2];
            string horizont2 = border[3] + new string(hor, WidthMenu) + border[5];
            string horizont3 = border[6] + new string(hor, WidthMenu) + border[8];

            int shiftYpos=0;

            Console.WriteLine(horizont1);
            if (FirstNameMenu)
            {
                Console.WriteLine($"{ver}{nameMenu}{ver}");
                Console.WriteLine(horizont2);
                if (infoMenu.Count > 0)
                {
                    foreach (var item in infoMenu)
                    {
                        Console.WriteLine($"{ver}{item}{ver}");
                    }
                    Console.WriteLine(horizont2);
                    shiftYpos = infoMenu.Count + 1;
                }
            }
            else
            {
                if (infoMenu.Count > 0)
                {
                    foreach (var item in infoMenu)
                    {
                        Console.WriteLine($"{ver}{item}{ver}");
                    }
                    Console.WriteLine(horizont2);
                    shiftYpos = infoMenu.Count + 1;
                }
                Console.WriteLine($"{ver}{nameMenu}{ver}");
                Console.WriteLine(horizont2);
            }
            foreach (var item in itemsMenu)
            {
                Console.WriteLine($"{ver}{item}{ver}");
            }
            Console.WriteLine(horizont3);
            
            int posX = 1;
            int posYmin = 3 + shiftYpos;
            int posYmax = 3 + shiftYpos + CountItemsMenu - 1;

            Console.BackgroundColor = activBackgroundColor;
            Console.ForegroundColor = activForegroundColor;
            Console.SetCursorPosition(posX, posYmin);
            Console.Write(this.itemsMenu[0]);            

            int curentChoice = 0;

            ConsoleKeyInfo key;
            
            do
            {
                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (curentChoice == CountItemsMenu - 1)
                            if (stopRange) continue;
                            else
                            {
                                Console.SetCursorPosition(posX, posYmin + curentChoice);
                                Console.BackgroundColor = saveBackgroundColor;
                                Console.ForegroundColor = saveForegroundColor;
                                Console.Write(this.itemsMenu[curentChoice]);
                                curentChoice = 0;
                                Console.SetCursorPosition(posX, posYmin + curentChoice);
                                Console.BackgroundColor = activBackgroundColor;
                                Console.ForegroundColor = activForegroundColor;
                                Console.Write(this.itemsMenu[curentChoice]);
                                continue;
                            }
                        Console.SetCursorPosition(posX, posYmin+curentChoice);
                        Console.BackgroundColor = saveBackgroundColor;
                        Console.ForegroundColor = saveForegroundColor;
                        Console.Write(this.itemsMenu[curentChoice]);
                        ++curentChoice;
                        Console.SetCursorPosition(posX, posYmin + curentChoice);
                        Console.BackgroundColor = activBackgroundColor;
                        Console.ForegroundColor = activForegroundColor;
                        Console.Write(this.itemsMenu[curentChoice]);
                        break;
                    case ConsoleKey.UpArrow:
                        if (curentChoice == 0)
                            if (stopRange) continue;
                            else
                            {
                                Console.SetCursorPosition(posX, posYmin + curentChoice);
                                Console.BackgroundColor = saveBackgroundColor;
                                Console.ForegroundColor = saveForegroundColor;
                                Console.Write(this.itemsMenu[curentChoice]);
                                curentChoice = CountItemsMenu - 1;
                                Console.SetCursorPosition(posX, posYmin + curentChoice);
                                Console.BackgroundColor = activBackgroundColor;
                                Console.ForegroundColor = activForegroundColor;
                                Console.Write(this.itemsMenu[curentChoice]);
                                continue;
                            }
                        Console.SetCursorPosition(posX, posYmin + curentChoice);
                        Console.BackgroundColor = saveBackgroundColor;
                        Console.ForegroundColor = saveForegroundColor;
                        Console.Write(this.itemsMenu[curentChoice]);
                        --curentChoice;
                        Console.SetCursorPosition(posX, posYmin + curentChoice);
                        Console.BackgroundColor = activBackgroundColor;
                        Console.ForegroundColor = activForegroundColor;
                        Console.Write(this.itemsMenu[curentChoice]);
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(0, posYmax + 2);
                        Console.BackgroundColor = saveBackgroundColor;
                        Console.ForegroundColor = saveForegroundColor;
                        Console.CursorVisible = saveCursorVisible;                        
                        return curentChoice;
                        break;
                    case ConsoleKey.Escape:
                        Console.SetCursorPosition(0, posYmax + 2);
                        Console.BackgroundColor = saveBackgroundColor;
                        Console.ForegroundColor = saveForegroundColor;
                        Console.CursorVisible = saveCursorVisible;
                        return -1;
                        break;
                }
            } while (true);
        }
    }
}
