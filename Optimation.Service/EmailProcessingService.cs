using HtmlAgilityPack;
using Optimation.Service.Abstractions;
using Optimation.Service.Common.Exceptions.EmailProcessing;
using Optimation.Service.Primitives.Models;
using Optimation.Shared.Calculations;
using Optimation.Shared.Extensions;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Optimation.Service
{
    public class EmailProcessingService : IEmailProcessingService
    {
        /// <summary>
        /// Extracts expense data from a block of text
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <param name="cancellationToken">Default cancellation token</param>
        /// <returns></returns>
        public async Task<ExpenseResourceModel> ExtractExpenseAsync(string text, CancellationToken cancellationToken = default)
        {
            ExpenseResourceModel resourceModel = new ExpenseResourceModel();

            await Task.Run(() => 
            {
                if (string.IsNullOrEmpty(text))
                    throw new EmailProcessingException("Text block can not be empty");

                // Load the email text as html doc
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(text);

                // Detect tags that are not closed
                if (htmlDocument.ContainsUnclosedTags())
                    throw new UnclosedTagException("Block of text has unclosed tag(s)");

                // Extract data
                HtmlNode expenseNode = htmlDocument.DocumentNode.SelectSingleNode("//expense");
                if (expenseNode == null) throw new EmailProcessingException("Expense element does not exist or is invalid");

                if (expenseNode.HasChildNodes)
                {
                    HtmlNode costCentre = expenseNode.SelectSingleNode("//cost_centre");
                    HtmlNode total = expenseNode.SelectSingleNode("//total");
                    HtmlNode paymentMethod = expenseNode.SelectSingleNode("//payment_method");

                    if (total == null)
                        throw new MissingElementException("Total node is required");

                    decimal totalValue = decimal.Parse(total.InnerText, CultureInfo.InvariantCulture);

                    resourceModel.CostCentre = costCentre != null ? costCentre.InnerText : "UNKNOWN";
                    resourceModel.Total = totalValue;
                    resourceModel.PaymentMethod = paymentMethod.InnerText;
                    resourceModel.TotalExcludingGST = FinancialCalculations.CalculateTotalNetFromTotalGross(totalValue);
                    resourceModel.GSTValue = FinancialCalculations.CalculateGSTValueFromTotalGross(totalValue);
                }
            }, cancellationToken);

            return resourceModel;
        }

        /// <summary>
        /// Extracts reservation data from a block of text
        /// </summary>
        /// <param name="text">Text to process</param>
        /// <param name="cancellationToken">Default cancellation token</param>
        /// <returns></returns>
        public async Task<ReservationResourceModel> ExtractReservationAsync(string text, CancellationToken cancellationToken = default)
        {
            ReservationResourceModel resourceModel = new ReservationResourceModel();

            await Task.Run(() =>
            {
                if (string.IsNullOrEmpty(text))
                    throw new EmailProcessingException("Text block can not be empty");

                // Load the email text as html doc
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(text);

                // Detect tags that are not closed
                if (htmlDocument.ContainsUnclosedTags())
                    throw new UnclosedTagException("Block of text has unclosed tag(s)");

                // Extract data
                HtmlNode vendorNode = htmlDocument.DocumentNode.SelectSingleNode("//vendor");
                HtmlNode descriptionNode = htmlDocument.DocumentNode.SelectSingleNode("//description");
                HtmlNode dateNode = htmlDocument.DocumentNode.SelectSingleNode("//date");

                if (vendorNode != null && descriptionNode != null && dateNode != null)
                {
                    resourceModel.Vendor = vendorNode.InnerText;
                    resourceModel.Description = descriptionNode.InnerText;
                    resourceModel.Date = DateTime.Parse(dateNode.InnerText);
                }
            }, cancellationToken);

            return resourceModel;
        }
    }
}
