using System;
using System.IO;


namespace Assignment1
{
    public class Program
    {
        private List<Accounts> accounts = new List<Accounts>();

        public static void Main(String[] args)
        {
            new Program().LoginMenu();

            //new Program().MainMenu();
            //new Program().CreateAccount();
            //new Program().SearchAccount();
            //new Program().DeleteAccount();
            //new Program().AccountStatement();
            //new Program().Deposit();
            //new Program().Withdrawal();
        }

        private void LoginMenu()
        {
            Console.Clear();// clear the screen

            ShowLoginMenu();

            Console.SetCursorPosition(14, 5);
            string userName = Console.ReadLine();
            Console.SetCursorPosition(13, 6);
            string password = "";

            ConsoleKeyInfo key;

            do
            {

                key = Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;

                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\n\n");
                }

            } while (key.Key != ConsoleKey.Enter) ;

            Console.SetCursorPosition(0, 8);

            if (File.Exists("login.txt"))
            {
                bool loginSucc = false;
                string[] loginInfo = File.ReadAllLines("login.txt");
                try
                {
                    // Display the file contents by using a foreach loop
                    foreach (string login in loginInfo)
                    {
                        string[] separator = { "|", " " };
                        string[] userInput = login.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        if (userName == userInput[0] && password == userInput[1])
                        {
                            loginSucc = true;
                            Console.WriteLine("Valid credidentials!... Please enter. ");
                            Console.ReadKey();
                            MainMenu();
                            break;
                        }
                    }

                    if (loginSucc == false)
                    {
                        Console.WriteLine("Incorrect username or password... ");
                        Console.WriteLine();
                        Console.WriteLine("Retry (y/n)? ");
                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            LoginMenu();
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
                        {
                            Console.SetCursorPosition(0, 12);
                            Console.WriteLine("BANKING SYSTEM EXIT... ");
                        }
                    }
                    
                }
                catch (IndexOutOfRangeException)
                {
                    // Keep the console window open in debug mode.
                    Console.WriteLine("Error... ");
                    Console.WriteLine("Press any key to exit... ");
                    Console.ReadKey();
                }
                
            }
            else
            {
                Console.WriteLine("Error... ");
                Console.WriteLine("Press any key to exit... ");
                Console.ReadKey();
            }


        }

        private void MainMenu()
        {
            int choice = 0;
            bool invalidInput = false;
            try
            {
                while (0 < choice || choice < 8)
                {
                    Console.Clear();// clear the screen

                    ShowMainMenu();

                    if (invalidInput)
                        Console.WriteLine("Invalid input. Please enter a number between 1 - 7: ");
                    // invalidInput = false;

                    Console.SetCursorPosition(28, 11);
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            CreateAccount();
                            break;
                        case 2:
                            SearchAccount();
                            break;
                        case 3:
                            Deposit();
                            break;
                        case 4:
                            Withdrawal();
                            break;
                        case 5:
                            AccountStatement();
                            break;
                        case 6:
                            DeleteAccount();
                            break;
                        case 7:
                            Exit();
                            break;

                        default:
                            invalidInput = true;
                            break;
                    }

                }
            }
            catch (FormatException ex)
            {
                MainMenu();
            }
        }

        private void CreateAccount()
        {
            Console.Clear();// clear the screen
            ShowCreateAccount();

            Console.SetCursorPosition(15, 4);
            string firstName = Console.ReadLine();
            Console.SetCursorPosition(14, 5);
            string lastName = Console.ReadLine();
            Console.SetCursorPosition(12, 6);
            string address = Console.ReadLine();
            Console.SetCursorPosition(10, 7);
            string phoneNumber = Console.ReadLine();
            Console.SetCursorPosition(10, 8);
            string email = Console.ReadLine();

            Console.SetCursorPosition(0, 11);
            Console.WriteLine("Is the information correct (y/n)? ");

            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {

                Console.WriteLine();

                if (Int64.TryParse(phoneNumber, out long phoneNumberInt) && phoneNumber.Length == 10)
                {
                    int accNumber;

                    do
                    {
                        accNumber = new Random().Next(100000, 99999999);
                    } while (File.Exists($"{accNumber}.txt"));

                    Accounts newAccount = new Accounts(accNumber, firstName, lastName, address, phoneNumber, email, 0);

                    if (newAccount.sendEmail())
                    {
                        accounts.Add(newAccount);
                        newAccount.updateFile();
                        Console.WriteLine("Account Created! details will be provided via email. ");
                        Console.WriteLine();
                        Console.WriteLine($"Account number is: {accNumber} ");
                        Console.WriteLine();
                        Console.WriteLine("Create another account (y/n)? ");

                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            CreateAccount();
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
                        {
                            MainMenu();
                        }

                    }

                    else
                    {
                        Console.WriteLine("Error in sending email... ");
                        Console.WriteLine();
                        Console.WriteLine("Retry (y/n)? ");

                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            CreateAccount();
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
                        {
                            MainMenu();
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Invalid phone number! ");
                    Console.WriteLine();
                    Console.WriteLine("Retry (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        CreateAccount();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
            }
            else
            {
                CreateAccount();
            }


        }

        private void SearchAccount()
        {
            Console.Clear();// clear the screen
            ShowSearchAccount();

            Console.SetCursorPosition(19, 5);
            string accNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 7);

            if (AccountExists(accNumber))
            {
                Console.WriteLine();
                Console.WriteLine($"Account {accNumber} Found! \n");
                Account(Convert.ToInt32(accNumber)).show_AccountDetails();
                Console.WriteLine();
                Console.Write("Check another account (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    SearchAccount();
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }
            else
            {
                Console.WriteLine("Account Not Found! ");
                Console.WriteLine();
                Console.WriteLine("Check another account (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    SearchAccount();
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }


        }

        private void AccountStatement()
        {
            Console.Clear();// clear the screen
            ShowAccountStatement();

            Console.SetCursorPosition(19, 5);
            string accNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 7);

            if (AccountExists(accNumber))
            {
                Console.WriteLine();
                Console.WriteLine($"Account {accNumber} Found! The statement is displayed below... ");
                Account(Convert.ToInt32(accNumber)).show_AccountStatement();
                Console.WriteLine();
                Console.WriteLine("Email statement (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Console.WriteLine("Email sent successfully!...");
                    Console.WriteLine();
                    Console.WriteLine("Check another account (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {

                        AccountStatement();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    Console.WriteLine();
                    Console.WriteLine("Check another account (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        AccountStatement();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }

            }
            else
            {
                Console.WriteLine("Account Not Found! ");
                Console.WriteLine();
                Console.WriteLine("Check another account (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    AccountStatement();
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }

        }

        private void DeleteAccount()
        {

            Console.Clear();// clear the screen
            ShowDeleteAccount();

            Console.SetCursorPosition(19, 5);
            string accNumber = Console.ReadLine();
            Console.SetCursorPosition(0, 7);

            if (AccountExists(accNumber))
            {
                Console.WriteLine();
                Console.WriteLine($"Account {accNumber} Found! Details displayed below... ");
                Account(Convert.ToInt32(accNumber)).show_AccountDetails();
                Console.WriteLine();
                Console.WriteLine("Delete (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    File.Delete($"{accNumber}.txt");
                    Console.WriteLine();
                    Console.WriteLine("Account Deleted!... ");
                    Console.WriteLine();
                    Console.WriteLine("Delete another account (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        DeleteAccount();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    Console.WriteLine();
                    Console.WriteLine("Cancelled account delete. ");
                    Console.WriteLine();
                    Console.WriteLine("Retry (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        DeleteAccount();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
            }
            else
            {
                Console.WriteLine("Account Not Found! ");
                Console.WriteLine();
                Console.WriteLine("Delete another account (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    DeleteAccount();
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }


        }

        private void Exit()
        {
            Console.Clear();
            Console.WriteLine("BANKING SYSTEM EXIT... ");
            Console.WriteLine();
            Console.WriteLine("Retry (y/n)? ");

            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                MainMenu();
            }
            else if (Console.ReadKey(true).Key == ConsoleKey.N)
            {
                Environment.Exit(0);
            }
        }


        private void Deposit()
        {
            Console.Clear();// clear the screen
            ShowDeposit();

            Console.SetCursorPosition(19, 5);
            string accNumber = Console.ReadLine();

            if (AccountExists(accNumber))
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"Account {accNumber} Found! Enter the amount... ");
                Console.SetCursorPosition(13, 6);
                string tempAmount = Console.ReadLine();

                if (Double.TryParse(tempAmount, out double amount))
                {

                    Account(Convert.ToInt32(accNumber)).deposit(amount);
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Deposit successfull! ");
                    Console.WriteLine();
                    Account(Convert.ToInt32(accNumber)).showBalance(); // current balance
                    Console.WriteLine();
                    Console.WriteLine("Make another deposit (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Deposit();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }

                else
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Invalid amount. ");
                    Console.WriteLine();
                    Console.WriteLine("Retry (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Deposit();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
            }

            else
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Account Not Found!");
                Console.WriteLine();
                Console.WriteLine("Retry (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Deposit();
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }

        }

        private void Withdrawal()
        {
            Console.Clear();// clear the screen
            ShowWithdrawal();

            Console.SetCursorPosition(19, 5);
            string accNumber = Console.ReadLine();
            if (AccountExists(accNumber))
            {

                Console.SetCursorPosition(0, 9);
                Console.WriteLine($"Account {accNumber} Found! Enter the amount... ");
                Console.SetCursorPosition(13, 6);
                string tempAmount = Console.ReadLine();

                if (Double.TryParse(tempAmount, out double amount))
                {

                    if (Account(Convert.ToInt32(accNumber)).hasFunds(amount))
                    {
                        Account(Convert.ToInt32(accNumber)).withdrawal(amount);
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine("Withdraw successfull! ");
                        Console.WriteLine();
                        Account(Convert.ToInt32(accNumber)).showBalance(); // current balance
                        Console.WriteLine();
                        Console.WriteLine("Make another withdrawal (y/n)? ");

                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Withdrawal();
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
                        {
                            MainMenu();
                        }
                    }

                    else
                    {
                        Console.SetCursorPosition(0, 11);
                        Console.WriteLine("Insufficient funds.");
                        Console.WriteLine();
                        Console.WriteLine("Retry (y/n)? ");

                        if (Console.ReadKey(true).Key == ConsoleKey.Y)
                        {
                            Withdrawal();
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
                        {
                            MainMenu();
                        }
                    }
                }

                else
                {
                    Console.SetCursorPosition(0, 11);
                    Console.WriteLine("Invalid amount. ");
                    Console.WriteLine();
                    Console.WriteLine("Retry (y/n)? ");

                    if (Console.ReadKey(true).Key == ConsoleKey.Y)
                    {
                        Withdrawal();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        MainMenu();
                    }
                }
            }

            else
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Account Not Found!");
                Console.WriteLine();
                Console.WriteLine("Retry (y/n)? ");

                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Withdrawal();
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    MainMenu();
                }
            }

        }

        private bool AccountExists(string accNumber)
        {
            if (accNumber.Length >= 6 && accNumber.Length <= 8 && Int32.TryParse(accNumber, out int accNumberInt) && File.Exists($"{accNumber}.txt"))
            {

                if (Account(accNumberInt) == null)
                {
                    string[] accountInfo = File.ReadAllLines($"{accNumber}.txt").Take(7).ToArray();

                    string FN = accountInfo[0].Split('|')[1];
                    string LN = accountInfo[1].Split('|')[1];
                    string AD = accountInfo[2].Split('|')[1];
                    string PN = accountInfo[3].Split('|')[1];
                    string EM = accountInfo[4].Split('|')[1];
                    string BL = accountInfo[6].Split('|')[1];

                    try
                    {
                        Accounts existingAcc = new Accounts(accNumberInt, FN, LN, AD, PN, EM, Convert.ToDouble(BL));
                        accounts.Add(existingAcc);
                    }
                    catch (Exception ex) when (ex is IndexOutOfRangeException || ex is FormatException)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private Accounts Account(int accNumber)
        {
            foreach (Accounts account in accounts)
            {
                if (account.hasAcc(accNumber))
                    return account;
            }
            return null;
        }


        // Menu
        private void ShowLoginMenu()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│  WELCOME TO SIMPLE BANKING SYSTEM  │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│           LOGIN TO START           │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│  USER NAME:                        │");
            Console.WriteLine("│  PASSWORD:                         │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowMainMenu()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│  WELCOME TO SIMPLE BANKING SYSTEM  │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│  1. Create a new account           │");
            Console.WriteLine("│  2. Search for an account          │");
            Console.WriteLine("│  3. Deposit                        │");
            Console.WriteLine("│  4. Withdraw                       │");
            Console.WriteLine("│  5. A/C statement                  │");
            Console.WriteLine("│  6. Delete account                 │");
            Console.WriteLine("│  7. Exit                           │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│  Enter your choice (1-7):          │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowCreateAccount()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│        CREATE A NEW ACCOUNT        │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│         ENTER THE DETAILS          │");
            Console.WriteLine("│  First NAME:                       │");
            Console.WriteLine("│  Last NAME:                        │");
            Console.WriteLine("│  Address:                          │");
            Console.WriteLine("│  Phone:                            │");
            Console.WriteLine("│  Email:                            │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowSearchAccount()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│         SEARCH AN ACCOUNT          │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│         ENTER THE DETAILS          │");
            Console.WriteLine("│                                    │");
            Console.WriteLine("│  Account Number:                   │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowDeleteAccount()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│         DELETE AN ACCOUNT          │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│         ENTER THE DETAILS          │");
            Console.WriteLine("│                                    │");
            Console.WriteLine("│  Account Number:                   │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowAccountStatement()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│             STATEMENT              │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│         ENTER THE DETAILS          │");
            Console.WriteLine("│                                    │");
            Console.WriteLine("│  Account Number:                   │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowDeposit()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│              DEPOSIT               │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│         ENTER THE DETAILS          │");
            Console.WriteLine("│                                    │");
            Console.WriteLine("│  Account Number:                   │");
            Console.WriteLine("│  Amount: $                         │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }

        private void ShowWithdrawal()
        {
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│              WITHDRAW              │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│         ENTER THE DETAILS          │");
            Console.WriteLine("│                                    │");
            Console.WriteLine("│  Account Number:                   │");
            Console.WriteLine("│  Amount: $                         │");
            Console.WriteLine("╘════════════════════════════════════╛");
        }
    }
}

