using System.Security.Claims;

namespace CodeVote.src.Utils
{
    public class RetrieveUserId
    {
        // Method to retrieve the user ID from the claims of the authenticated user
        public static Guid? GetUserId(ClaimsPrincipal user)
        {
            var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            if (Guid.TryParse(userIdClaim, out var guid)) 
                return guid;
            return null;

        }
    }
}
