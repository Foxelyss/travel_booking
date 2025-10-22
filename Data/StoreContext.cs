using System;
using Microsoft.EntityFrameworkCore;

namespace TravelBooking.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{

}
