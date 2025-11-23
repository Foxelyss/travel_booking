using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TravelBooking.Data;

namespace TravelBooking;

public static class HttpContextExtension
{
    public static Guid GetGuid(this ClaimsPrincipal User)
    {
        Claim? claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null)
        {
            throw new Exception("Токен без ID!");
        }

        string guidString = claim.Value;

        return Guid.Parse(guidString);
    }
}