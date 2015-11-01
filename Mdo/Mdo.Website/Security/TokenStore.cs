using System.Collections.Generic;
using System.Linq;

namespace Mdo.Website.Security
{
    public static class TokenStore
    {
        private static List<Token> storedTokens;
        public static List<Token> StoredTokens
        {
            get { return storedTokens ?? (storedTokens = new List<Token>()); }
        }
        

        public static void AddToken(Token token)
        {
            StoredTokens.Add(token);
        }

        public static Token FindMatching(BearerToken bearerToken)
        {
            var token = StoredTokens.FirstOrDefault(x => x.IsBearerTokenMatching(bearerToken));
            return token;
        }
    }
}