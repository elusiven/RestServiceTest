using HtmlAgilityPack;
using Optimation.Shared.Exceptions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Optimation.Shared.Extensions
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// Filters out email address element and checks for unclosed tags in a html document
        /// </summary>
        /// <param name="htmlDocument"></param>
        /// <returns></returns>
        public static bool ContainsUnclosedTags(this HtmlDocument htmlDocument)
        {
            try
            {
                List<HtmlParseError> errors = htmlDocument.ParseErrors.Where(e => e.Code == HtmlParseErrorCode.TagNotClosed).ToList();

                foreach (HtmlParseError error in errors.ToList())
                {
                    // Find element name in the block of text
                    string startTag = "</";
                    int startIndex = error.Reason.IndexOf(startTag) + startTag.Length;
                    int endIndex = error.Reason.IndexOf(">", startIndex);
                    string elementName = error.Reason.Substring(startIndex, endIndex - startIndex);

                    // Remove elements that contain email addresses
                    if (elementName.IsValidEmail())
                        errors.Remove(error);
                }

                // Throw exception if there are any errors that are not emails
                return errors.Count > 0;
            }
            catch (Exception exception)
            {
                throw new HtmlExtensionException(exception.Message, exception.InnerException);
            }
        }
    }
}
