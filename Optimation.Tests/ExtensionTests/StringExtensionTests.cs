using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Tests.Data;
using Optimation.Shared.Extensions;

namespace Optimation.Tests.ExtensionTests
{
    [TestClass]
    [TestCategory("String Extension Tests")]
    public class StringExtensionTests
    {
        [TestMethod]
        public void String_Contains_Valid_Email()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.ValidEmailAddress;

            // Act
            bool result = text.IsValidEmail();

            // Assert
            Assert.IsTrue(result, "Failed to validate email address");
        }

        [TestMethod]
        public void String_Contains_Invalid_Email()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.InvalidEmailAddress;

            // Act
            bool result = text.IsValidEmail();

            // Assert
            Assert.IsFalse(result, "Unexpected behaviour - Invalid email has been validated as valid");
        }
    }
}
