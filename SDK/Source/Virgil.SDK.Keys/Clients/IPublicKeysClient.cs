namespace Virgil.SDK.Keys.Clients
{
    using System;
    using System.Threading.Tasks;

    using Virgil.SDK.Keys.TransferObject;

    /// <summary>
    /// Provides common methods to interact with Public Keys resource endpoints.
    /// </summary>
    public interface IPublicKeysClient : IVirgilService
    {
        /// <summary>
        /// Gets the specified public key by it identifier.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <returns>Public key dto</returns>
        Task<PublicKeyDto> Get(Guid publicKeyId);

        /// <summary>
        /// Gets the specified public key by it identifier with extended data.
        /// </summary>
        /// <param name="publicKeyId">The public key identifier.</param>
        /// <param name="virgilCardId">The virgil card identifier.</param>
        /// <param name="privateKey">The private key. Private key is used to produce sign. It is not transfered over network</param>
        /// <returns>Extended public key dto response</returns>
        Task<GetPublicKeyExtendedResponse> GetExtended(Guid publicKeyId, Guid virgilCardId, byte[] privateKey);
    }
}