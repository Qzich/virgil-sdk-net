﻿namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Interface that specifies communication with Virgil Security Identity service.
    /// </summary>
    public interface IIdentityClient : IVirgilService
    {
        /// <summary>
        /// Sends the request for identity verification, that's will be processed depending of specified type.
        /// </summary>
        /// <param name="identityValue">An unique string that represents identity.</param>
        /// <param name="type">The type of identity.</param>
        /// <returns>An instance of <see cref="IndentityTokenDto"/> response.</returns>
        /// <remarks>
        /// Use method <see cref="Confirm(Guid, string, int, int)" /> to confirm and get the indentity token.
        /// </remarks>
        Task<VirgilVerifyResponse> Verify(string identityValue, IdentityType type);

        /// <summary>
        /// Confirms the identity using confirmation code, that has been generated to confirm an identity.
        /// </summary>
        /// <param name="actionId">The action identifier.</param>
        /// <param name="confirmationCode">The confirmation code.</param>
        /// <param name="timeToLive">The time to live.</param>
        /// <param name="countToLive">The count to live.</param>
        Task<IndentityTokenDto> Confirm(Guid actionId, string confirmationCode, int timeToLive = 3600, int countToLive = 1);

        /// <summary>
        /// Checks whether the validation token is valid for specified identity.
        /// </summary>
        /// <param name="type">The type of identity.</param>
        /// <param name="value">The identity value.</param>
        /// <param name="validationToken">The string value that represents validation token for Virgil Identity Service.</param>
        Task<bool> IsValid(IdentityType type, string value, string validationToken);

        /// <summary>
        /// Checks whether the validation token is valid for specified identity.
        /// </summary>
        /// <param name="token">The identity token DTO that represents validation token and identity information.</param>
        Task<bool> IsValid(IndentityTokenDto token);
    }
}