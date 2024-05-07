using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SavingsACC
{
    public partial class MainWindow : Window
    {

        public double accountBalance = 0;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public List<(double amount, string type)> bills = new List<(double amount, string type)>();

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            bills = new List<(double amount, string type)>();
            // Generate a random 8-digit account number
            Random random = new Random();
            int accountNumber = random.Next(10000000, 99999999);

            // Display the account number in a text block at the top of the window
            txtAccountNumber.Text = $"Account Number: {accountNumber}";
         
            UpdateAccountBalanceDisplay();
        }

        public void AddBalance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double balanceToAdd = double.Parse(txtBalance.Text);
                accountBalance += balanceToAdd;
                UpdateAccountBalanceDisplay();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid balance.");
            }
        }

        public void UpdateAccountBalanceDisplay()
        {
            txtAccountBalance.Text = $"Account Balance: {accountBalance:C}";
        }

        public void AddBill_Click(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the ComboBox
            if (cmbBillType.SelectedItem == null)
            {
                MessageBox.Show("Please select a bill type.");
                return; // Exit the method without adding a bill
            }

            try
            {
                // Add the current bill input to the list
                double billAmount = double.Parse(txtBillAmount.Text);
                string billType = (cmbBillType.SelectedItem as ComboBoxItem)?.Content.ToString();
                bills.Add((billAmount, billType));

                // Update the account balance by deducting the bill amount multiplied by 12
                accountBalance -= billAmount * 12;

                // Update the display of account balance
                UpdateAccountBalanceDisplay();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid bill amount.");
            }
        }

        public void RemoveLastBill_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are any bills in the list
            if (bills.Count == 0)
            {
                MessageBox.Show("There are no bills to remove.");
                return; // Exit the method with attempting to remove a bill
            }

            // Remove the last bill input from the list
            var lastBill = bills.Last();
            bills.Remove(lastBill);

            // Update the account balance by adding back the amount of the removed bill multiplied by 12
            accountBalance += lastBill.amount * 12;

            // Update the display of account balance
            UpdateAccountBalanceDisplay();
        }

        public void CalculateSavings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double balance = accountBalance; // Use the current account balance
                double desiredSavings = double.Parse(txtDesiredSavings.Text);

                // Calculate the remaining balance after deducting bills for the year
                double remainingBalance = balance - (GetTotalBillsAmount() * 12);

                // Calculate the monthly savings needed to reach the desired savings
                double monthlySavings = desiredSavings / 12;

                if (remainingBalance < desiredSavings)
                {
                    MessageBox.Show("Your account balance after paying bills for 12 months is below the desired savings. You need to adjust your desired savings or bills.");
                }
                else
                {
                    // Create a message box to display the savings results
                    StringBuilder message = new StringBuilder();
                    message.AppendLine($"Monthly savings needed: {monthlySavings:C}");

                    MessageBox.Show(message.ToString(), "Savings Results");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers.");
            }
        }


        public double GetTotalBillsAmount()
        {
            double totalBillsAmount = 0;
            foreach (var bill in bills)
            {
                totalBillsAmount += bill.amount;
            }
            return totalBillsAmount;
        }

    }
}
