using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual string Token { get; set; }
        public virtual DateTime Expires { get; set; }
        public virtual bool IsExpired => DateTime.UtcNow >= Expires;
        public virtual DateTime Created { get; set; }
        public virtual string CreatedByIp { get; set; }
        public virtual DateTime? Revoked { get; set; }
        public virtual string RevokedByIp { get; set; }
        public virtual string ReplacedByToken { get; set; }
        public virtual bool IsActive => Revoked == null && !IsExpired;
    }
}