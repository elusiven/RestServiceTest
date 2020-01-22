using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Service;
using Optimation.Service.Abstractions;
using Optimation.Service.Primitives.Models;
using Optimation.Tests.Data;
using Optimation.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Tests
{
    [TestClass]
    public class EmailProcessingTests
    {
        [TestMethod]
        public void Extracting_Fail_With_Unclosed_Tag()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.EndTagMissingExpenseTextBlock;
            IEmailProcessingService emailProcessingService = new EmailProcessingService();

            // Act
            ExpenseResourceModel model = emailProcessingService.ExtractExpense(text).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(model);
        }

        #region Controller Tests
        
        [TestMethod]
        public async Task Controller_Get_ExpenseResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectExpenseTextBlock;
            IEmailProcessingService emailProcessingService = new EmailProcessingService();
            EmailProcessingController emailProcessingController = new EmailProcessingController(emailProcessingService);

            // Act
            var result = await emailProcessingController.Expense1_0(text);

            // Assert
            var model = ((OkObjectResult)result).Value as ExpenseResourceModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("DEV002", model.CostCentre);
            Assert.AreEqual(Convert.ToDecimal(1024.01), model.Total);
            Assert.AreEqual(Convert.ToDecimal(890.44), model.TotalExcludingGST);
            Assert.AreEqual(Convert.ToDecimal(133.57), model.GSTValue);
            Assert.AreEqual("personal card", model.PaymentMethod);
        }

        [TestMethod]
        public async Task Controller_Get_ReservationResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectReservationTextblock;
            IEmailProcessingService emailProcessingService = new EmailProcessingService();
            EmailProcessingController emailProcessingController = new EmailProcessingController(emailProcessingService);

            // Act
            var result = await emailProcessingController.Reservation1_0(text);

            // Assert
            var model = ((OkObjectResult)result).Value as ReservationResourceModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("Viaduct Steakhouse", model.Vendor);
            Assert.AreEqual("development team’s project end celebration dinner", model.Description);
            Assert.AreEqual("Tuesday 27 April 2017", model.Date);
        }

        #endregion

        #region Service Tests

        [TestMethod]
        public void Service_Get_ExpenseResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectExpenseTextBlock;
            IEmailProcessingService emailProcessingService = new EmailProcessingService();

            // Act
            ExpenseResourceModel model = emailProcessingService.ExtractExpense(text).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual("DEV002", model.CostCentre);
            Assert.AreEqual(Convert.ToDecimal(1024.01), model.Total);
            Assert.AreEqual(Convert.ToDecimal(890.44), model.TotalExcludingGST);
            Assert.AreEqual(Convert.ToDecimal(133.57), model.GSTValue);
            Assert.AreEqual("personal card", model.PaymentMethod);
        }

        [TestMethod]
        public void Service_Get_ReservationResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectReservationTextblock;
            IEmailProcessingService emailProcessingService = new EmailProcessingService();

            // Act
            ReservationResourceModel model = emailProcessingService.ExtractReservation(text).GetAwaiter().GetResult();

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual("Viaduct Steakhouse", model.Vendor);
            Assert.AreEqual("development team’s project end celebration dinner", model.Description);
            Assert.AreEqual("Tuesday 27 April 2017", model.Date);
        }

        #endregion
    }
}
