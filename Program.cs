using Budget;
using System;
using System.IO;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;

namespace Budget
{
    class Program
    {      

        static string defaultFileName = "budget.json";
        static string defaultFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), defaultFileName);
        

        static void Main()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            Messages.ShowGreeting();

            // Введення та розподіл коштів.

            int total = 0;
            string currency = "";

            List<Category> categories = new List<Category>();

            AppConfig config = AppConfig.LoadConfig();

            string filePath = config.LastBudgetPath;

            bool isFixing = true;

            while (isFixing)
            {
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    // Вивід бюджету по категоріях.

                    string json = File.ReadAllText(filePath);
                    BudgetData loadedData = JsonSerializer.Deserialize<BudgetData>(json);
                    total = loadedData.Total;
                    categories = loadedData.Categories;
                    currency = loadedData.Currency;
                    Messages.LoadData();

                    ShowBudget(total, currency, categories);

                    Messages.DoNextStep();
                    Console.ReadKey();                   

                    // Поточні витрати.

                    int spending = 0;                  

                    while (true)
                    {
                        Messages.ShowTotalExit();
                        Messages.AskForClear();

                        Messages.EnterSpenging();

                        if (!TryReadIntOrClear(filePath, out spending))
                        {
                            isFixing = false;
                            break;
                        }

                        Messages.EnterNumberOfCategory(spending, currency);

                        if (!TryReadIntOrClear(filePath, out int userInput))
                        {
                            isFixing = false;
                            break;
                        }

                        if ((userInput < 1) || (userInput > categories.Count))
                        {
                            Messages.ShowCategoryError();
                            continue;
                        }
                        else
                        {
                            categories[userInput - 1].Amount -= spending;
                            Save(total, currency, filePath, categories);
                            ShowBudget(total, currency, categories);
                        }

                    }
                }

                // Дії при першому використанні програми.

                else
                {
                    Messages.GetFilePath(filePath);

                    string input = Console.ReadLine();

                    filePath = string.IsNullOrEmpty(input) ? defaultFilePath : input;

                    bool currencyIsChosen = false;

                    while (!currencyIsChosen)
                    {
                        Messages.ChooseCurrency();
                        switch (Console.ReadLine())
                        {
                            case "1":
                                currency = "₴";
                                break;
                            case "2":
                                currency = "$";
                                break;
                            case "3":
                                currency = "€";
                                break;
                            default:
                                continue;
                        }
                        currencyIsChosen = true;
                    }

                    Messages.EnterWholeAmount(currency);
                    TryReadInt(out total);

                    Console.Clear();

                    int counter = 1;
                    int amount = 0;

                    while (true)
                    {    
                        Messages.ShowExit();

                        Messages.EnterNameOfCategory(counter);

                        if (!TryReadLine(out string userInput))
                            break;

                        string categoryName = Convert.ToString(counter) + " " + userInput;
                        categories.Add(new Category(categoryName, amount));
                        categories[counter - 1].Name = string.IsNullOrEmpty(categoryName) ? Convert.ToString(counter) + " Без назви" : categoryName;

                        bool moneyIsCorrect = false;

                        while (!moneyIsCorrect)
                        {
                            Messages.ShowCurrentBalance(total, currency);
                            Messages.EnterMoneyToCategory();

                            TryReadInt(out int moneyInput);

                            categories[counter - 1].Amount = moneyInput;

                            total -= categories[counter - 1].Amount;

                            if (total < 0)
                            {
                                Messages.InformAboutLackOfMoney();
                                total += categories[counter - 1].Amount;
                                continue;
                            }

                            moneyIsCorrect = true;
                            Console.Clear();
                        } 


                        if (total == 0)
                        {
                            break;
                        }

                        counter++;

                    }

                    Save(total, currency, filePath, categories);

                    Console.Clear();

                }

            }

        }


        // Методи для роботи з BudgetData.

        static void Save(int total, string currency, string filePath, List<Category> categories)
        {
            Messages.AskForSaveData();
            string save = Console.ReadLine();

            if (save == "s")
            {
                BudgetData data = new BudgetData();
                AppConfig config = new AppConfig();
                config.LastBudgetPath = filePath;
                data.Total = total;
                data.Categories = categories;
                data.Currency = currency;

                string jsonSave = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                AppConfig.SaveConfig(config);

                File.WriteAllText(filePath, jsonSave);

                Messages.SaveData();
            }

            Console.ReadKey();
            Console.Clear();

        }

        static void ShowBudget(int total, string currency, List<Category> categories)
        {
            Messages.ShowTheBudget();

            foreach (Category category in categories)
            {
                Console.Write(category.Name + " - ");
                Console.WriteLine(category.Amount + currency);
            }

            Messages.ShowCurrentBalance(total, currency);
        }



        // Методи для перевірки вводу.

        static bool TryReadLine(out string userInput)
        {
            userInput = Console.ReadLine();
            if (userInput == "Q")
            {
                Console.Clear();
                return false;
            }

                return true;
        }

        static bool TryReadLineOrClear(string filePath, out string userInput)
        {

            if(!TryReadLine(out userInput))
                return false;
            if (userInput == "C")
            {
                File.Delete(filePath);               
                return false;
            }

            return true;

        }

        static bool TryReadInt(out int value)
        {
            value = 0;

            if (!TryReadLine(out string input))
                return false;

            if (!int.TryParse(input, out value))
            {
                Messages.ShowNumberError();
                return TryReadInt(out value);
            }

            return true;
        }

        static bool TryReadIntOrClear(string filePath, out int value)
        {
            value = 0;

            if (!TryReadLineOrClear(filePath, out string input))
                return false;

            if (!int.TryParse(input, out value))
            {
                Messages.ShowNumberError();
                return TryReadInt(out value);
            }
            
            return true;

        }

    }

}
