using System;
using System.Net;
using NUnit.Framework;
using Should;
using SpecsFor;

namespace WebApi.IntegrationTests
{
    public class UsingRefreshTokenSpecs : SpecsFor<TestWebClient>
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

    public class RevokingUserClaimsSpecs : SpecsFor<TestWebClient>
    {
        protected override void AfterEachTest()
        {
            SUT.RemoveRole("a1@b.com", "Test");
            base.AfterEachTest();
        }

        [Test]
        public void when_claim_revoked_can_still_access_resource_with_access_token()
        {
            SUT.AddRole("a1@b.com", "Test");
            SUT.Login("a1@b.com", "Password1!");

            SUT.LoadSecret();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);

            SUT.RemoveRole("a1@b.com", "Test");
            SUT.LoadSecret();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);
        }

        [Test]
        public void when_claim_revoked_and_access_token_expired_then_cannot_access_resource_anymore()
        {
            SUT.AddRole("a1@b.com", "Test");
            var token = SUT.Login("a1@b.com", "Password1!");

            SUT.LoadSecret();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.OK);

            SUT.RemoveRole("a1@b.com", "Test");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(11));
            var token2 = SUT.GetAccessToken(token.refresh_token);

            SUT.LoadSecret();
            SUT.LastOperationHttpStatusCode.ShouldEqual(HttpStatusCode.Unauthorized);
        }
    }
}