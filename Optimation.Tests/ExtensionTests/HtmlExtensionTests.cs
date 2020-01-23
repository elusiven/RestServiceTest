using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Shared.Extensions;
using Optimation.Tests.Data;

namespace Optimation.Tests.ExtensionTests
{
    [TestClass]
    [TestCategory("Html Extension Tests")]
    public class HtmlExtensionTests
    {
        [TestMethod]
        public void Html_Contains_UnclosedTags()
        {
            // Arrange
            string text = FakeDataHelper.EmailProcessing.EndTagMissingExpenseTextBlock;
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(text);

            // Act
            bool result = htmlDocument.ContainsUnclosedTags();

            // Assert
            Assert.IsNotNull(htmlDocument, "Html document failed to instantiate and is null");
            Assert.AreNotEqual(string.Empty, htmlDocument.Text, "Html document failed to load text");
            Assert.IsTrue(result, "Failed to check for unclosed tags within the html document");
        }
    }
}
