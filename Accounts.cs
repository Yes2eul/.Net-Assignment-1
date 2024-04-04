using System;
using System.IO;
using System.Net;
using System.Net.Mail;


namespace Assignment1
{
    public class Accounts
    {
        private int accNumber;
        private double balance;
        private string firstName, lastName, address, phoneNumber, email;

        public Accounts(int accNumber, string firstName, string lastName, string address, string phoneNumber, string email, double balance)
        {
            this.accNumber = accNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.balance = balance;
        }

        public void deposit(double amount)
        {
            balance += amount;
            updateFile();

        }

        public void withdrawal(double amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                updateFile();
            }
        }

        // <account_number>.txt file
        public void updateFile()
        {
            File.WriteAllText(string.Format($"{accNumber}.txt"),
                              string.Format($"Account No| {accNumber}\nFirst Name| {firstName}\nLast Name| {lastName}\n"
                              + $"Address| {address}\nPhone| {phoneNumber}\nEmail| {email}\nBalance| {balance:0.00}\n"));
        }

        public void showBalance()
        {
            Console.WriteLine($"Current Balance: $ {balance}");
        }

        public bool sendEmail()
        {
            /*
            // set SMTP host and port number
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            // sender email and password required by SMTP account
            client.Credentials = new NetworkCredential("yeseul5611@gmail.com", "shin134679!!");
            // enable SSL for encryption
            client.EnableSsl = true;

            try
            {
                MailMessage mail = new MailMessage();
                // set sender email address and show name
                mail.From = new MailAddress("yeseul5611@gmail.com", "Simple Bank");

                // receiver
                mail.To.Add(new MailAddress(email));
                
                mail.Subject = "Welcome to Simple Banking System!";
                mail.Body = string.Format($"Hello {firstName},<br><br>" +
                    "Your bank account details displayed below...<br><br>" +

                    $"Account number: {accNumber}<br>" +
                    $"First name: {firstName}<br>" +
                    $"Last name: {lastName}<br>" +
                    $"Address: {address}<br>" +
                    $"Phone number: {phoneNumber}<br>" +
                    $"Email: {email}<br><br>" +

                    "Regards,<br>" +
                    "Simple Bank Team<br>");

                mail.IsBodyHtml = true;

                client.Send(mail);
                return true;
            }
            catch (Exception ex) when (ex is FormatException || ex is SmtpException)
            {
                return false;
            }
            */

            return true;
        }

        public bool hasAcc(int accNumber)
        {
            return this.accNumber == accNumber;
        }

        public bool hasFunds(double amount)
        {
            return balance >= amount;
        }

        public void show_AccountDetails()
        {
            Console.WriteLine();
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│          ACCOUNT DETAILS           │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│                                    │");
            Console.WriteLine($"│  Account No: {accNumber}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Account Balance: ${balance}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  First Name: {firstName}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Last Name: {lastName}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Address: {address}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Phone: {phoneNumber}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Email: {email}".PadRight(37, ' ') + "│");
            Console.WriteLine("╘════════════════════════════════════╛");
            Console.WriteLine();
        }

        public void show_AccountStatement()
        {
            Console.WriteLine();
            Console.WriteLine("╒════════════════════════════════════╕");
            Console.WriteLine("│       SIMPLE BANKING SYSTEM        │");
            Console.WriteLine("│════════════════════════════════════│");
            Console.WriteLine("│  Account Statement                 │");
            Console.WriteLine("│                                    │");
            Console.WriteLine($"│  Account No: {accNumber}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Account Balance: ${balance}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  First Name: {firstName}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Last Name: {lastName}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Address: {address}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Phone: {phoneNumber}".PadRight(37, ' ') + "│");
            Console.WriteLine($"│  Email: {email}".PadRight(37, ' ') + "│");
            Console.WriteLine("╘════════════════════════════════════╛");
            Console.WriteLine();
        }



    }

    
}

