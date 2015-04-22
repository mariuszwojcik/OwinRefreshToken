using System;
using System.Net;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace WebApi.IntegrationTests
{
    [Explicit("Requires WebApi project to run in the background.")]

    public class AuthorizingUserSpecs : SpecsFor<TestWebClient>
    {
        private string _result;

        protected override void When()
        {
            _result = SUT.Login("a1@b.com", "Password1!");
        }

        [Test]
        public void then_should_load_data_successfully()
        {
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);
            Console.WriteLine(SUT.LastOperationResponse);
        }

    }
}