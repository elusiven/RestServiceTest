using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Shared.Calculations;

namespace Optimation.Tests.CalculationTests
{
    [TestClass]
    [TestCategory("Financial Calculation Tests")]
    public class FinancialCalculationTests
    {
        [TestMethod]
        public void Calculate_GST_Value()
        {
            // Arrange
            decimal total = 1000.00m;

            // Act
            decimal gstValue = FinancialCalculations.CalculateGSTValueFromTotalGross(total);

            // Assert
            Assert.AreEqual(130.43m, gstValue);
        }

        [TestMethod]
        public void Calculate_Total_Net_Value()
        {
            // Arrange
            decimal total = 1000.00m;

            // Act
            decimal gstValue = FinancialCalculations.CalculateTotalNetFromTotalGross(total);

            // Assert
            Assert.AreEqual(869.57m, gstValue);
        }
    }
}
