using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimation.Shared.Calculations
{
    public class FinancialCalculations
    {
        /// <summary>
        /// Calculates total net value from GST inclusive total gross based on GST 15%
        /// </summary>
        /// <param name="totalGross"></param>
        /// <returns></returns>
        public static decimal CalculateTotalNetFromTotalGross(decimal totalGross)
        {
            return decimal.Round(totalGross / Convert.ToDecimal(1.15), 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Calculates GST Value by substracting total net value from total gross
        /// </summary>
        /// <param name="totalGross"></param>
        /// <returns></returns>
        public static decimal CalculateGSTValueFromTotalGross(decimal totalGross)
        {
            return decimal.Round(totalGross - CalculateTotalNetFromTotalGross(totalGross), 2, MidpointRounding.AwayFromZero);
        }
    }
}
