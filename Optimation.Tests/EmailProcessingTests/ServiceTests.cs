using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Service.Primitives.Models;
using Optimation.Tests.Base;
using Optimation.Tests.Data;
using System;
using System.Threading.Tasks;

namespace Optimation.Tests.EmailProcessingTests
{
    [TestClass]
    [TestCategory("Service Tests")]
    public class ServiceTests : EmailProcessingTestBase
    {
        [TestMethod]
        public async Task Service_Get_ExpenseResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectExpenseTextBlock;

            // Act
            ExpenseResourceModel model = await EmailProcessingService.ExtractExpenseAsync(text);
            

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual("DEV002", model.CostCentre);
            Assert.AreEqual(1024.01m, model.Total);
            Assert.AreEqual(890.44m, model.TotalExcludingGST);
            Assert.AreEqual(133.57m, model.GSTValue);
            Assert.AreEqual("personal card", model.PaymentMethod);
        }

        [TestMethod]
        public async Task Service_Get_ReservationResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectReservationTextblock;

            // Act
            ReservationResourceModel model = await EmailProcessingService.ExtractReservationAsync(text);

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual("Viaduct Steakhouse", model.Vendor);
            Assert.AreEqual("development team’s project end celebration dinner", model.Description);
            Assert.AreEqual(DateTime.Parse("Thursday 27 April 2017"), model.Date);
        }
    }
}
