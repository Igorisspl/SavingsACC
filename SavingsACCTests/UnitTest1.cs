using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace SavingsACCTests
{
    [TestFixture]
    public class MainWindowTests
    {
        private MainWindow mainWindow;

        [SetUp]
        public void SetUp()
        {
            mainWindow = new MainWindow();
        }

        [Test]
        public void AddBill_AddsBillCorrectly()
        {
            // Arrange
            double initialBalance = 1000;
            double billAmount = 50;

            mainWindow.txtBalance.Text = initialBalance.ToString();
            mainWindow.txtBillAmount.Text = billAmount.ToString();

            // Act
            mainWindow.AddBill_Click(null, null);

            // Assert
            Assert.AreEqual(initialBalance - (billAmount * 12), mainWindow.accountBalance);
        }

        [Test]
        public void RemoveLastBill_RemovesLastBillCorrectly()
        {
            // Arrange
            double initialBalance = 1000;
            double billAmount1 = 50;
            double billAmount2 = 75;

            mainWindow.txtBalance.Text = initialBalance.ToString();
            mainWindow.txtBillAmount.Text = billAmount1.ToString();
            mainWindow.AddBill_Click(null, null);
            mainWindow.txtBillAmount.Text = billAmount2.ToString();
            mainWindow.AddBill_Click(null, null);

            // Act
            mainWindow.RemoveLastBill_Click(null, null);

            // Assert
            Assert.AreEqual(initialBalance - (billAmount1 * 12), mainWindow.accountBalance);
        }

    }
}

