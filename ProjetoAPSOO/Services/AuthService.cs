using ProjetoAPSOO.Adapters;

namespace ProjetoAPSOO.Services
{
    // AuthService.cs
    public class AuthService : IAuthService
    {
        private readonly IExternalAuthAdapter _externalAuthAdapter;

        public AuthService(IExternalAuthAdapter externalAuthAdapter)
        {
            _externalAuthAdapter = externalAuthAdapter;
        }

        public string LoginWithGoogle(string provider, string token)
        {
            if (provider.ToLower() == "google")
            {
                return _externalAuthAdapter.AuthWithGoogle(token);
            }

            throw new NotSupportedException($"Provider {provider} is not supported.");
        }
    }
}
