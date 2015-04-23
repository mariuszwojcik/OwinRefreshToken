using System.Net;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace WebApi.IntegrationTests
{
    //[Explicit("Requires WebApi project to run in the background.")]
    public class AccessingProtectedResourceSpecs : SpecsFor<TestWebClient>
    {
        [Test]
        public void when_not_authenticated_then_should_fail_with_unauthorized_code()
        {
            SUT.Logout();

            SUT.LoadProtectedData();

            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
        }

        [Test]
        public void when_authenticated_then_should_succeed()
        {
            SUT.Login("a1@b.com", "Password1!");


            SUT.LoadProtectedData();

            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);
        }
    }
}