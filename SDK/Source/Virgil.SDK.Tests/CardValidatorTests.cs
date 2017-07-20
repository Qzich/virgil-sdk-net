namespace Virgil.SDK.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using NUnit.Framework;

    using Virgil.SDK.Client;
    using Virgil.SDK.Common;
    using Virgil.SDK.Cryptography;
    using Client.Models;

    public class CardValidatorTests
    {
        [Test]
        public void Validate_CardWithFakedOwnerSignature_ShouldReturnFalse()
        {
            var card = new CardModel((PublishCardSnapshotModel)new PublishCardSnapshotModel
            {
                PublicKeyData = Convert.FromBase64String((string)"MCowBQYDK2VwAyEAZzBtEQEWMQ9VeJrqSoO939VR5qimaTs4reqe9ut1VPk="),
            })
            {
                Id = "eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a",
                Snapshot = Convert.FromBase64String((string)"eyJpZGVudGl0eSI6ImFsaWNlIiwiaWRlbnRpdHlfdHlwZSI6Im1lbWJlciIsInB1YmxpY19rZXkiOiJNQ293QlFZREsyVndBeUVBWnpCdEVRRVdNUTlWZUpycVNvTzkzOVZSNXFpbWFUczRyZXFlOXV0MVZQaz0iLCJzY29wZSI6ImFwcGxpY2F0aW9uIiwiZGF0YSI6e30sImluZm8iOm51bGx9"),
                Meta = new CardMetaModel
                {
                    Version = "4.0",
                    Signatures = new Dictionary<string, byte[]>
                    {
                        [(string)"eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a"] =
                            Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQMpaO3OmXlsYhzR7pvF0Xuu7Dv84r3SRrmqjMvod9ik+oQ0M0uc+dwHNeNtQpy84qI14cXXaMAJDcfgtKyHPdA0="),
                        [(string)"0b23070f8bafc48765658b92f168ae70b7638bc6fde0d246258de8a1116a52c4"] =
                            Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQJggpfBBpO9mHG2Q7hxdkY5b20krS4w4WG6IxNUHGmN1ZvKq0LECgNc2yuvXkDiSqXQ011zN1yhGwxe/LwtkZg8="),
                        [(string)"3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853"] =
                            Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQMpaO3OmXlsYhzR7pvF0Xuu7Dv84r3SRrmqjMvod9ik+oQ0M0uc+dwHNeNtQpy84qI14cXXaMAJDcfgtKyHPdA0="),
                    }
                }
            };

            var crypto = new VirgilCrypto();
            var validator = new CardValidator(crypto);
            validator.AddDefaultVerifiers();
            validator.Validate(card).Should().BeFalse();
        }

        [Test]
        public void Validate_PredefinedCardGiven_ShouldReutrnTrue()
        {
            var card = new CardModel((PublishCardSnapshotModel)new PublishCardSnapshotModel
            {
                PublicKeyData = Convert.FromBase64String((string)"MCowBQYDK2VwAyEAZzBtEQEWMQ9VeJrqSoO939VR5qimaTs4reqe9ut1VPk="),
            })
            {
                Id = "eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a",
                Snapshot = Convert.FromBase64String((string)"eyJpZGVudGl0eSI6ImFsaWNlIiwiaWRlbnRpdHlfdHlwZSI6Im1lbWJlciIsInB1YmxpY19rZXkiOiJNQ293QlFZREsyVndBeUVBWnpCdEVRRVdNUTlWZUpycVNvTzkzOVZSNXFpbWFUczRyZXFlOXV0MVZQaz0iLCJzY29wZSI6ImFwcGxpY2F0aW9uIiwiZGF0YSI6e30sImluZm8iOm51bGx9"),
                Meta = new CardMetaModel
                {
                    Version = "4.0",
                    Signatures = new Dictionary<string, byte[]>
                    {
                        [(string)"eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a"] =
                        Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQFpw+jB5eDT1Dj3I2WqCewGqhAdG9f8pncAYeYcWHGWIONZlog1gjBb/y5/km8VbIPjrn4wlF0Ld8L5tRqRZOQM="),
                        [(string)"0b23070f8bafc48765658b92f168ae70b7638bc6fde0d246258de8a1116a52c4"] =
                        Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQJggpfBBpO9mHG2Q7hxdkY5b20krS4w4WG6IxNUHGmN1ZvKq0LECgNc2yuvXkDiSqXQ011zN1yhGwxe/LwtkZg8="),
                        [(string)"3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853"] =
                        Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQMpaO3OmXlsYhzR7pvF0Xuu7Dv84r3SRrmqjMvod9ik+oQ0M0uc+dwHNeNtQpy84qI14cXXaMAJDcfgtKyHPdA0="),
                    }
                }
            };

            var crypto = new VirgilCrypto();
            var validator = new CardValidator(crypto);
            validator.AddDefaultVerifiers();
            validator.Validate(card).Should().BeTrue();
        }

        [Test]
        public void Validate_CardWithFakedCardId_ShouldReturnFalse()
        {
            var card = new CardModel
            {
                Id = "3e29d43373348cfb373b7eae189214dc01d7237765e572db685839b64adca853",
                Snapshot = Convert.FromBase64String("eyJpZGVudGl0eSI6ImFsaWNlIiwiaWRlbnRpdHlfdHlwZSI6Im1lbWJlciIsInB1YmxpY19rZXkiOiJNQ293QlFZREsyVndBeUVBWnpCdEVRRVdNUTlWZUpycVNvTzkzOVZSNXFpbWFUczRyZXFlOXV0MVZQaz0iLCJzY29wZSI6ImFwcGxpY2F0aW9uIiwiZGF0YSI6e30sImluZm8iOm51bGx9"),
                Meta = new CardMetaModel
                {
                    Version = "4.0"
                }
            };

            var crypto = new VirgilCrypto();
            var validator = new CardValidator(crypto);
            validator.AddDefaultVerifiers();
            validator.Validate(card).Should().BeFalse();
        }

        [Test]
        public void ValidateWithoutDefaultVerifiers_CardWithoutServiceSignature_ShouldReturnTrue()
        {
            var card = new CardModel((PublishCardSnapshotModel)new PublishCardSnapshotModel
            {
                PublicKeyData = Convert.FromBase64String((string)"MCowBQYDK2VwAyEAZzBtEQEWMQ9VeJrqSoO939VR5qimaTs4reqe9ut1VPk="),
            })
            {
                Id = "eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a",
                Snapshot = Convert.FromBase64String((string)"eyJpZGVudGl0eSI6ImFsaWNlIiwiaWRlbnRpdHlfdHlwZSI6Im1lbWJlciIsInB1YmxpY19rZXkiOiJNQ293QlFZREsyVndBeUVBWnpCdEVRRVdNUTlWZUpycVNvTzkzOVZSNXFpbWFUczRyZXFlOXV0MVZQaz0iLCJzY29wZSI6ImFwcGxpY2F0aW9uIiwiZGF0YSI6e30sImluZm8iOm51bGx9"),
                Meta = new CardMetaModel
                {
                    Version = "4.0",
                    Signatures = new Dictionary<string, byte[]>
                    {
                        [(string)"eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a"] =
                      Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQFpw+jB5eDT1Dj3I2WqCewGqhAdG9f8pncAYeYcWHGWIONZlog1gjBb/y5/km8VbIPjrn4wlF0Ld8L5tRqRZOQM="),
                        [(string)"0b23070f8bafc48765658b92f168ae70b7638bc6fde0d246258de8a1116a52c4"] =
                      Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQJggpfBBpO9mHG2Q7hxdkY5b20krS4w4WG6IxNUHGmN1ZvKq0LECgNc2yuvXkDiSqXQ011zN1yhGwxe/LwtkZg8=")
                      }
                }
            };

            var crypto = new VirgilCrypto();
            var validator = new CardValidator(crypto);
            validator.Validate(card).Should().BeTrue();
        }

        [Test]
        public void ValidateWithDefaultVerifiers_CardWithoutServiceSignature_ShouldReturnFalse()
        {
            var card = new CardModel((PublishCardSnapshotModel)new PublishCardSnapshotModel
            {
                PublicKeyData = Convert.FromBase64String((string)"MCowBQYDK2VwAyEAZzBtEQEWMQ9VeJrqSoO939VR5qimaTs4reqe9ut1VPk="),
            })
            {
                Id = "eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a",
                Snapshot = Convert.FromBase64String((string)"eyJpZGVudGl0eSI6ImFsaWNlIiwiaWRlbnRpdHlfdHlwZSI6Im1lbWJlciIsInB1YmxpY19rZXkiOiJNQ293QlFZREsyVndBeUVBWnpCdEVRRVdNUTlWZUpycVNvTzkzOVZSNXFpbWFUczRyZXFlOXV0MVZQaz0iLCJzY29wZSI6ImFwcGxpY2F0aW9uIiwiZGF0YSI6e30sImluZm8iOm51bGx9"),
                Meta = new CardMetaModel
                {
                    Version = "4.0",
                    Signatures = new Dictionary<string, byte[]>
                    {
                        [(string)"eb95e1b31ff3090598a05bf108c06088af5f70cfd6338924932396e9dfce840a"] =
                      Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQFpw+jB5eDT1Dj3I2WqCewGqhAdG9f8pncAYeYcWHGWIONZlog1gjBb/y5/km8VbIPjrn4wlF0Ld8L5tRqRZOQM="),
                        [(string)"0b23070f8bafc48765658b92f168ae70b7638bc6fde0d246258de8a1116a52c4"] =
                      Convert.FromBase64String((string)"MFEwDQYJYIZIAWUDBAICBQAEQJggpfBBpO9mHG2Q7hxdkY5b20krS4w4WG6IxNUHGmN1ZvKq0LECgNc2yuvXkDiSqXQ011zN1yhGwxe/LwtkZg8=")
                    }
                }
            };

            var crypto = new VirgilCrypto();
            var validator = new CardValidator(crypto);
            validator.AddDefaultVerifiers();
            validator.Validate(card).Should().BeFalse();
        }
    }
}