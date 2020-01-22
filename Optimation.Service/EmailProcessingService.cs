using HtmlAgilityPack;
using Optimation.Service.Abstractions;
using Optimation.Service.Common.Exceptions;
using Optimation.Service.Primitives.Models;
using Optimation.Shared.Calculations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Optimation.Service
{
    public class EmailProcessingService : IEmailProcessingService
    {
        public async Task<ExpenseResourceModel> ExtractExpense(string text)
        {
            ExpenseResourceModel resourceModel = new ExpenseResourceModel();

            await Task.Run(() => 
            {
                if (string.IsNullOrEmpty(text))
                    throw new EmailProcessingException("Text block cannot be null or empty");

                // Check for unclosed tags

                // Load the email text
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(text);

                // detect tags that are not closed
                bool textHasUnclosedTag = doc.ParseErrors.Any(e => e.Code == HtmlParseErrorCode.TagNotClosed);
                if (textHasUnclosedTag)
                    throw new EmailProcessingException("Block of text has an unclosed tag(s)");

                // Extract data
                HtmlNode expenseNode = doc.DocumentNode.SelectSingleNode("//expense");

                if (expenseNode.HasChildNodes)
                {
                    HtmlNode costCentre = expenseNode.SelectSingleNode("//cost_centre");
                    HtmlNode total = expenseNode.SelectSingleNode("//total");
                    HtmlNode paymentMethod = expenseNode.SelectSingleNode("//payment_method");

                    if (total == null)
                        throw new EmailProcessingException("Total node is required");

                    decimal totalValue = decimal.Parse(total.InnerText, CultureInfo.InvariantCulture);

                    resourceModel.CostCentre = costCentre != null ? costCentre.InnerText : "UNKNOWN";
                    resourceModel.Total = totalValue;
                    resourceModel.PaymentMethod = paymentMethod.InnerText;
                    resourceModel.TotalExcludingGST = FinancialCalculations.CalculateTotalNetFromTotalGross(totalValue);
                    resourceModel.GSTValue = FinancialCalculations.CalculateGSTValueFromTotalGross(totalValue);
                }
            });

            return resourceModel;
        }

        public async Task<ReservationResourceModel> ExtractReservation(string text)
        {
            ReservationResourceModel resourceModel = new ReservationResourceModel();

            await Task.Run(() =>
            {

            });

            // Check for unclosed tags

            // Load the email text
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(text);

            // detect tags that are not closed
            foreach (HtmlParseError error in doc.ParseErrors.Where(e => e.Code == HtmlParseErrorCode.TagNotClosed))
            {

            }

            // Extract data
            HtmlNode vendorNode = doc.DocumentNode.SelectSingleNode("//vendor");
            HtmlNode descriptionNode = doc.DocumentNode.SelectSingleNode("//description");
            HtmlNode dateNode = doc.DocumentNode.SelectSingleNode("//date");

            if (vendorNode != null && descriptionNode != null && dateNode != null)
            {
                resourceModel.Vendor = vendorNode.InnerText;
                resourceModel.Description = descriptionNode.InnerText;
                resourceModel.Date = DateTime.Parse(dateNode.InnerText);
            }

            return resourceModel;
        }
    }
}
