using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Service.Common.Exceptions.EmailProcessing;
using Optimation.Service.Primitives.Models;
using Optimation.Tests.Base;
using Optimation.Tests.Data;
using Optimation.WebApi.Controllers;
using System;
using System.Threading.Tasks;

namespace Optimation.Tests.EmailProcessingTests
{
    [TestClass]
    [TestCategory("Controller Tests")]
    public class ControllerTests : EmailProcessingTestBase
    {
        #region [ Expense Resource Model ]

        [TestMethod]
        public async Task Controller_Get_ExpenseResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectExpenseTextBlock;

            // Act
            IActionResult result = await EmailProcessingController.Expense1_0(text);

            // Assert
            ExpenseResourceModel model = ((OkObjectResult)result).Value as ExpenseResourceModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("DEV002", model.CostCentre);
            Assert.AreEqual(1024.01m, model.Total);
            Assert.AreEqual(890.44m, model.TotalExcludingGST);
            Assert.AreEqual(133.57m, model.GSTValue);
            Assert.AreEqual("personal card", model.PaymentMethod);
        }

        [TestMethod]
        public async Task Controller_Get_ExpenseResourceModel_No_CostCentre()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.MissingCostCentreExpenseTextBlock;

            // Act
            IActionResult result = await EmailProcessingController.Expense1_0(text);

            // Assert
            ExpenseResourceModel model = ((OkObjectResult)result).Value as ExpenseResourceModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("UNKNOWN", model.CostCentre);
            Assert.AreEqual(1024.01m, model.Total);
            Assert.AreEqual(890.44m, model.TotalExcludingGST);
            Assert.AreEqual(133.57m, model.GSTValue);
            Assert.AreEqual("personal card", model.PaymentMethod);
        }

        [TestMethod]
        public async Task Extracting_Expense_Fail_With_Unclosed_Tag()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.EndTagMissingExpenseTextBlock;

            UnclosedTagException exception = await Assert.ThrowsExceptionAsync<UnclosedTagException>(() => EmailProcessingController.Expense1_0(text));
        }

        [TestMethod]
        public async Task Extracting_Expense_Fail_With_Missing_Element()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.TotalNodeMissingExpenseTextBlock;

            MissingElementException exception = await Assert.ThrowsExceptionAsync<MissingElementException>(() => EmailProcessingController.Expense1_0(text));
        }

        [TestMethod]
        public async Task Extracting_Expense_Fail_With_Empty_Text()
        {
            // Arrange
            string text = string.Empty;

            EmailProcessingException exception = await Assert.ThrowsExceptionAsync<EmailProcessingException>(() => EmailProcessingController.Expense1_0(text));
        }

        #endregion

        #region [ Reservation Resource Model ]

        [TestMethod]
        public async Task Controller_Get_ReservationResourceModel()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.CorrectReservationTextblock;

            // Act
            IActionResult result = await EmailProcessingController.Reservation1_0(text);

            // Assert
            ReservationResourceModel model = ((OkObjectResult)result).Value as ReservationResourceModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("Viaduct Steakhouse", model.Vendor);
            Assert.AreEqual("development team’s project end celebration dinner", model.Description);
            Assert.AreEqual(DateTime.Parse("Thursday 27 April 2017"), model.Date);
        }

        [TestMethod]
        public async Task Extracting_Reservation_Fail_With_Unclosed_Tag()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.EndTagMissingReservationTextBlock;

            UnclosedTagException exception = await Assert.ThrowsExceptionAsync<UnclosedTagException>(() => EmailProcessingController.Reservation1_0(text));
        }

        [TestMethod]
        public async Task Extracting_Reservation_Fail_With_Empty_Text()
        {
            // Arrange
            string text = string.Empty;

            EmailProcessingException exception = await Assert.ThrowsExceptionAsync<EmailProcessingException>(() => EmailProcessingController.Reservation1_0(text));
        }

        #endregion
    }
}
