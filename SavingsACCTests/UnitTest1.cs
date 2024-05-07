using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using SavingsACC;

namespace SavingsACCTests
{
    [TestFixture]
    public class MainWindowTests
    {
        private MainWindow _window;

        [SetUp]
        public void SetUp()
        {
            _window = new MainWindow();
        }

        [Test]
        public void AddBalance_AddsToAccountBalance()
        {
            double initialBalance = _window.accountBalance;
            double amountToAdd = 100;

            _window.txtBalance.Text = amountToAdd.ToString();
            _window.AddBalance_Click(null, null);

            Assert.AreEqual(initialBalance + amountToAdd, _window.accountBalance);
        }

        [Test]
        public void AddBill_AddsToBillsAndDeductsFromAccountBalance()
        {
            double initialBalance = _window.accountBalance;
            double billAmount = 50;

            _window.txtBillAmount.Text = billAmount.ToString();
            _window.cmbBillType.SelectedIndex = 0; // Selecting the first bill type
            _window.AddBill_Click(null, null);

            Assert.AreEqual(initialBalance - (billAmount * 12), _window.accountBalance);
            Assert.AreEqual(1, _window.bills.Count);
        }

        [Test]
        public void RemoveLastBill_RemovesFromBillsAndAddsToAccountBalance()
        {
            double initialBalance = _window.accountBalance;
            double billAmount = 50;

            _window.txtBillAmount.Text = billAmount.ToString();
            _window.cmbBillType.SelectedIndex = 0; // Selecting the first bill type
            _window.AddBill_Click(null, null);

            double updatedBalance = _window.accountBalance;
            _window.RemoveLastBill_Click(null, null);

            Assert.AreEqual(initialBalance, _window.accountBalance);
            Assert.AreEqual(0, _window.bills.Count);
            Assert.AreEqual(initialBalance + (billAmount * 12), updatedBalance);
        }

        [Test]
        public void CalculateSavings_CorrectlyCalculatesMonthlySavings()
        {
            double balance = 1000;
            double desiredSavings = 5000;

            _window.accountBalance = balance;
            _window.txtDesiredSavings.Text = desiredSavings.ToString();
            _window.CalculateSavings_Click(null, null);

            // Expected monthly savings = Desired savings / 12
            double expectedMonthlySavings = desiredSavings / 12;

            Assert.AreEqual(expectedMonthlySavings, _window.accountBalance);
        }

        [Test]
        public void InputValidation_HandlesInvalidInputs()
        {
            // Test invalid balance input
            _window.txtBalance.Text = "abc";
            _window.AddBalance_Click(null, null);
            Assert.AreEqual(0, _window.accountBalance);

            // Test invalid bill amount input
            _window.txtBillAmount.Text = "xyz";
            _window.cmbBillType.SelectedIndex = 0;
            _window.AddBill_Click(null, null);
            Assert.AreEqual(0, _window.bills.Count);

            // Test invalid desired savings input
            _window.txtDesiredSavings.Text = "123abc";
            _window.CalculateSavings_Click(null, null);
            // Assert the appropriate message box is shown
        }
    }
}