using CloudCustomers.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
            {
                Name = "Test User 1",
                Email = "teste.user.1@produtiveridy.com",
                Address = new Address 
                { 
                    City = "somewhere",
                    Street = "123 Morket St",
                    ZipCode = "2323334"
                }
            },
            new User
            {
                Name = "Test User 2",
                Email = "teste.user.2@produtiveridy.com",
                Address = new Address
                {
                    City = "somewhere",
                    Street = "123 Morket St",
                    ZipCode = "2323334"
                }
            },
            new User
            {
                Name = "Test User 3",
                Email = "teste.user.3@produtiveridy.com",
                Address = new Address
                {
                    City = "somewhere",
                    Street = "123 Morket St",
                    ZipCode = "2323334"
                }
            }
        };
    }
}
