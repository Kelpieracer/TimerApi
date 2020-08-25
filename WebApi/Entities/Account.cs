using System;
using System.Collections.Generic;

namespace WebApi.Entities
{
    public class Account
    {
        public virtual int AccountId { get; set; }
        public virtual string Title { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual bool AcceptTerms { get; set; }
        public virtual Role Role { get; set; }
        public virtual string VerificationToken { get; set; }
        public virtual DateTime? Verified { get; set; }
        public virtual bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        public virtual string ResetToken { get; set; }
        public virtual DateTime? ResetTokenExpires { get; set; }
        public virtual DateTime? PasswordReset { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }

        public bool OwnsToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }
}