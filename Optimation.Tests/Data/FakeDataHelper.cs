namespace Optimation.Tests.Data
{
    public static class FakeDataHelper
    {
        public static class EmailProcessing
        {
            #region [ Expense ]

            public const string CorrectExpenseTextBlock = @"Hi Yvaine,  Please create an expense claim for the below. Relevant details are marked up as requested…  <expense> <cost_centre>DEV002</cost_centre> <total>1024.01</total> <payment_method>personal card</payment_method>  </expense>";
            public const string MissingCostCentreExpenseTextBlock = @"Hi Yvaine,  Please create an expense claim for the below. Relevant details are marked up as requested…  <expense> <total>1024.01</total> <payment_method>personal card</payment_method>  </expense>";
            public const string EndTagMissingExpenseTextBlock = @"Hi Yvaine,  Please create an expense claim for the below. Relevant details are marked up as requested…  <expense> <cost_centre>DEV002</cost_centre> <total>1024.01</total> <payment_method>personal card</payment_method>";
            public const string TotalNodeMissingExpenseTextBlock = @"Hi Yvaine,  Please create an expense claim for the below. Relevant details are marked up as requested…  <expense> <cost_centre>DEV002</cost_centre> <payment_method>personal card</payment_method>  </expense> ";

            #endregion

            #region [ Reservation ]

            public const string EndTagMissingReservationTextBlock = @"From: Ivan Castle Sent: Friday, 16 February 2018 10:32 AM  To: Antoine Lloyd <Antoine.Lloyd@example.com>  Subject: test  Hi Antoine,  Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development team’s project end celebration dinner on <date>Thursday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.  Regards,  Ivan   ";
            public const string CorrectReservationTextblock = @"From: Ivan Castle Sent: Friday, 16 February 2018 10:32 AM  To: Antoine Lloyd <Antoine.Lloyd@example.com>  Subject: test  Hi Antoine,  Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our <description>development team’s project end celebration dinner</description> on <date>Thursday 27 April 2017</date>. We expect to arrive around 7.15pm. Approximately 12 people but I’ll confirm exact numbers closer to the day.  Regards,  Ivan   ";

            #endregion

            #region [ Email Scenarios ]

            public const string ValidEmailAddress = "john.dwayne@test.com";
            public const string InvalidEmailAddress = "john.dwayne@@@@@@@test.com.com";

            #endregion
        }
    }
}
