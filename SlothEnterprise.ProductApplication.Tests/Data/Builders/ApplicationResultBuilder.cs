using Moq;
using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Tests.Data.Builders
{
    public class ApplicationResultBuilder
    {
        private int? applicationId;
        private bool success;

        public ApplicationResultBuilder WithApplicationId(int? applicationId)
        {
            this.applicationId = applicationId;
            return this;
        }

        public ApplicationResultBuilder WithSuccess(bool success)
        {
            this.success = success;
            return this;
        }

        public IApplicationResult Build()
        {
            var result = new Mock<IApplicationResult>();
            result.SetupProperty(p => p.ApplicationId, this.applicationId);
            result.SetupProperty(p => p.Success, this.success);

            return result.Object;
        }
    }
}