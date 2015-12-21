namespace Virgil.SDK.Keys.Domain
{
    using TransferObject;

    public class IdentityToken 
    {
        public VirgilIndentityToken Token { get; private set; }

        public string Identity { get; private set; }

        public IdentityType IdentityType { get; private set; }

        internal IdentityToken(IdentityTokenRequest request, VirgilIndentityToken token)
        {
            this.Identity = request.Identity;
            this.IdentityType = request.IdentityType;
            this.Token = token;
        }
    }
}