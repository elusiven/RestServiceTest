using Microsoft.VisualStudio.TestTools.UnitTesting;
using Optimation.Service;
using Optimation.Service.Abstractions;
using Optimation.WebApi.Controllers;

namespace Optimation.Tests.Base
{
    public abstract class EmailProcessingTestBase
    {
        protected IEmailProcessingService EmailProcessingService { get; private set; }
        protected EmailProcessingController EmailProcessingController { get; private set; }

        [TestInitialize]
        [TestCategory("Test Initialization")]
        public void Init()
        {
            EmailProcessingService = new EmailProcessingService();
            EmailProcessingController = new EmailProcessingController(EmailProcessingService);
        }
    }
}
