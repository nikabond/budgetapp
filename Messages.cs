using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget
{
    internal static class Messages
    {
        private static string hello = "Вітаю у програмі для розрахунку місячного бюджету.";
        private static string getFilePath = "Введіть шлях для збереження файлу.\n(Enter — використати шлях за замовчуванням):";
        private static string dataLoaded = "✅ Дані успішно завантажено!";
        private static string nextStep = "Натисніть будь-яку клавішу, щоб перейти до запису поточних витрат.\n";
        private static string spengingMoney = "Введіть суму, яку ви витратили: ";
        private static string spengingCategory = "З якої категорії ви витратили {0}{1}? Введіть номер категорії: ";
        private static string categoryFormatException = "Такої категорії не існує. Спробуйте ще.";
        private static string chooseCurrency = "Оберіть валюту: 1 - гривня, 2 - долар, 3 - євро.";
        private static string enterWholeAmount = "Введіть повну сумму {0}, що ви маєте: ";
        private static string moneyFormatException = "Введіть ціле число.";
        private static string exitRequest = "Щоб вийти з режиму додавання категорій, натисніть Q.";
        private static string enterCategory = "Введіть назву {0} категорії: ";
        private static string currentBalance = "Залишок - {0}{1}.";
        private static string categoryMoney = "Скільки грошей ви хочете внести в цю категорію?";
        private static string lackOfMoney = "У вас недостатньо грошей.";
        private static string lackOfCategories = "Можна додати лише {0} категорій.";
        private static string saveIsRequested = "Натисніть s, щоб зберегти дані.";
        private static string totalExit = "Щоб вийти з програми розрахунку бюджету, натисніть Q.";
        private static string saveIsMade = "✅ Дані успішно збережено!";
        private static string allTheBudget = "Ваш бюджет по категоріях:";
        private static string clearBudget = "Щоб очистити всі дані, натисніть С.";


        public static void ShowGreeting()
        {
            Console.WriteLine(hello);
        }

        public static void GetFilePath(string defaultFilePath)
        {
            Console.WriteLine(getFilePath);
            Console.WriteLine(defaultFilePath);
        }

        public static void LoadData()
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(dataLoaded);
            Console.ForegroundColor = defaultColor;
        }

        public static void SaveData()
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(saveIsMade);
            Console.ForegroundColor = defaultColor;
            
        }

        public static void DoNextStep()
        {
            Console.WriteLine(nextStep);
        }

        public static void EnterSpenging()
        {
            Console.WriteLine(spengingMoney);
        }

        public static void EnterNumberOfCategory(int spending, string currency)
        {
            Console.WriteLine(string.Format(spengingCategory, spending, currency));
        }

        public static void ShowCategoryError()
        {
            Console.WriteLine(categoryFormatException);
        }

        public static void ChooseCurrency()
        {
            Console.WriteLine(chooseCurrency);
        }

        public static void EnterWholeAmount(string currency)
        {
            Console.WriteLine(string.Format(enterWholeAmount, currency));
        }

        public static void ShowNumberError()
        {
            Console.WriteLine(moneyFormatException);
        }

        public static void ShowExit()
        {
            Console.SetCursorPosition(65, 0);
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(exitRequest);
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(0, 0);
        }
        public static void ShowTotalExit()
        {
            (int x, int y) = Console.GetCursorPosition();
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(65, 0);
            Console.WriteLine(totalExit);
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(x, (y + 1));
        }

        public static void EnterNameOfCategory(int number)
        {
            Console.WriteLine(string.Format(enterCategory, number));
        }

        public static void ShowCurrentBalance(int balance, string currency)
        {
            Console.WriteLine(string.Format(currentBalance, balance, currency));
        }

        public static void EnterMoneyToCategory()
        {
            Console.WriteLine(categoryMoney);
        }

        public static void InformAboutLackOfMoney()
        {
            Console.WriteLine(lackOfMoney);
        }

        public static void InformAboutLackOfCategories(int number)
        {
            Console.WriteLine(string.Format(lackOfCategories, number));
        }

        public static void AskForSaveData()
        {
            Console.WriteLine(saveIsRequested);
        }

        public static void ShowTheBudget()
        {
            Console.WriteLine(allTheBudget);
        }

        public static void AskForClear ()
        {
            (int x, int y) = Console.GetCursorPosition();
            Console.SetCursorPosition(65, 2);
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(clearBudget);
            Console.ForegroundColor = defaultColor;
            Console.SetCursorPosition(x, y);
        }
    }
}
