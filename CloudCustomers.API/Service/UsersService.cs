using CloudCustomers.API.Models;
using System;


public interface IUsersService
{
    public Task<List<User>> GetAllUsers();
}
public class UsersService : IUsersService
{
    public Task<List<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }
}
