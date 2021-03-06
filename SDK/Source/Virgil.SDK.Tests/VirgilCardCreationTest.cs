namespace Virgil.SDK.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using FluentAssertions;

    using Virgil.SDK.Client;
    using Virgil.SDK.Cryptography;

    using NUnit.Framework;
    using Virgil.SDK.Exceptions;
    using System.Collections.Generic;

    public class VirgilCardCreationTest
    {
        [Test]
        public async Task CreateNewVirgilCard_DuplicateCardCreation_ShouldThrowException()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetVirgilClient();
            
            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var request = new PublishCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntegrationHelper.AppID, appKey);
            
            var virgilCard = await client.PublishCardAsync(request);
            Assert.ThrowsAsync<VirgilClientException>(async () => await client.PublishCardAsync(request));
        }

        [Test]
        public async Task CreateNewVirgilCard_IdentityAndPublicKeyGiven_ShouldBeFoundByIdentity()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetVirgilClient();

            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();

            var request = new PublishCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntegrationHelper.AppID, appKey);

            var newCard = await client.PublishCardAsync(request);
            var cards = await client.SearchCardsAsync(new SearchCriteria { Identities = new[] { aliceIdentity } });
            
            cards.Should().HaveCount(1);
            var foundCard = cards.Single();

            newCard.ShouldBeEquivalentTo(foundCard);
        }

        [Test]
        public async Task CreateNewVirgilCard_SignatureValidation_ShouldPassValidation()
        {
            var crypto = new VirgilCrypto();
            var client = IntegrationHelper.GetVirgilClient();

            // CREATING A VIRGIL CARD

            var appKey = crypto.ImportPrivateKey(IntegrationHelper.AppKey, IntegrationHelper.AppKeyPassword);

            var aliceKeys = crypto.GenerateKeys();
            var exportedPublicKey = crypto.ExportPublicKey(aliceKeys.PublicKey);

            var aliceIdentity = "alice-" + Guid.NewGuid();
            var request = new PublishCardRequest(aliceIdentity, "member", exportedPublicKey);

            var requestSigner = new RequestSigner(crypto);

            requestSigner.SelfSign(request, aliceKeys.PrivateKey);
            requestSigner.AuthoritySign(request, IntegrationHelper.AppID, appKey);

            var aliceCard = await client.PublishCardAsync(request);

            // VALIDATING A VIRGIL CARD

            var appPublicKey = crypto.ExtractPublicKey(appKey);
            var exportedAppPublicKey = crypto.ExportPublicKey(appPublicKey);

            var validator = new CardValidator(crypto);
            validator.AddVerifier(IntegrationHelper.AppID, exportedAppPublicKey);

            validator.Validate(aliceCard).Should().BeTrue();

            await IntegrationHelper.RevokeCard(aliceCard.Id);
        }

    }
}