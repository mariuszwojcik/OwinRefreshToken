using System;
using System.Net;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace WebApi.IntegrationTests
{
    public class UsingRefreshToken : SpecsFor<TestWebClient>
    {
        [Test]
        public void when_using_expired_access_token_then_should_return_unauthorized()
        {
            var token = SUT.Login("a1@b.com", "Password1!");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(11));
            SUT.LoadSuperProtectedData();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
        }

        [Test]
        public void when_refreshed_access_token_then_should_return_ok()
        {
            var token = SUT.Login("a1@b.com", "Password1!");

            SUT.LoadSuperProtectedData();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(11));
            SUT.LoadSuperProtectedData();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.Unauthorized);


            var token2 = SUT.GetAccessToken(token.refresh_token);
            SUT.LoadSuperProtectedData();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);
            
        }

        [Test]
        public void when_refresh_token_expires_then_should_return_unauthorized()
        {
            var token = SUT.Login("a1@b.com", "Password1!");

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(16));
            var token2 = SUT.GetAccessToken(token.refresh_token);

            token2.ShouldBeNull();
            SUT.LoadSuperProtectedData();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.Unauthorized);

        }
    }
}