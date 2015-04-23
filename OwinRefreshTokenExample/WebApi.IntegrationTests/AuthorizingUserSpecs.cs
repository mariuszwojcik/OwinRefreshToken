using System;
using System.Net;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace WebApi.IntegrationTests
{
    //[Explicit("Requires WebApi project to run in the background.")]

    public class AuthorizingUserSpecs : SpecsFor<TestWebClient>
    {
        [Test]
        public void when_logged_in_then_returns_token()
        {
            var result = SUT.Login("a1@b.com", "Password1!");
            
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);
            result.ShouldNotBeNull();
            result.access_token.ShouldNotBeEmpty();
        }

        [Test]
        public void when_invalid_password_then_result_code_is_bad_request()
        {
            SUT.Login("a1@b.com", "Password");

            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }

        [Test]
        public void when_invalid_credentials_then_result_code_is_bad_request()
        {
            SUT.Login("test@b.com", "Password");

            Console.WriteLine(SUT.LastOperationResponse);
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.BadRequest);
        }
    }
}