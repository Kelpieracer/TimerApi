using System.Collections.Generic;

namespace WebApi.Helpers
{
    public static class ErrorMessages
    {
        public enum Code { 
            Ok, 
            BadRequest, 
            Conflict, 
            UnAuthorized, 
            NotFound, 
            Created, 
            NoContent 
        }
        public static readonly Dictionary<Code, string> Text = new Dictionary<Code, string> {
            { Code.BadRequest, "Bad request." },
            { Code.Conflict, "Conflict or item already exists." },
            { Code.NotFound, "Item not found." },
            { Code.Created, "Item created." },
            { Code.NoContent, "Item removed." },
            { Code.UnAuthorized, "Unauthorized access." },
        };
        public static void Throw(Code code)
        {
            throw new AppException(Text.GetValueOrDefault(code));
        }
    }
}
