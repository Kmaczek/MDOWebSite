using System;

namespace Mdo.Website.Security
{
    public class Token
    {
        public static readonly TimeSpan DefaultTokenSpan = new TimeSpan(1,0,0,0);

        public Guid Value { get; }

        public TimeSpan TokenSpan { get; }

        public DateTime CreationTime { get; }

        public Token()
        {
            Value = Guid.NewGuid();
            TokenSpan = DefaultTokenSpan;
            CreationTime = DateTime.Now;
        }

        public BearerToken CreateBearerToken()
        {
            var bearerToken = new BearerToken {Value = Value.ToString()};

            return bearerToken;
        }

        public bool IsBearerTokenMatching(BearerToken bearerToken)
        {
            return Value.ToString().Equals(bearerToken.Value);
        }
    }
}