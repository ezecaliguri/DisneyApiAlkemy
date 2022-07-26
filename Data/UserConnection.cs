using DisneyApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DisneyApi.Data
{
    public class UserConnection : IdentityDbContext<User>
    {
        public UserConnection(DbContextOptions<UserConnection> options) : base(options)
        {

        }
    }
}
