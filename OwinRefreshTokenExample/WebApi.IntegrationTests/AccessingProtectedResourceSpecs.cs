using System.Net;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace WebApi.IntegrationTests
{
    [Explicit("Requires WebApi project to run in the background.")]
    public class AccessingProtectedResourceSpecs : SpecsFor<TestWebClient>
    {
        public class Given_user_not_authenticated : SpecsFor<TestWebClient>
        {
            private string _result;

            protected override void When()
            {
                _result = SUT.LoadProtectedData();
            }

            [Test]
            public void then_should_load_data_successfully()
            {
                SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
            }
        }
    }
}