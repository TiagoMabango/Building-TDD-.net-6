using CloudCustomers.API.Models;
using System;


public interface IUsersService
{
    public Task<List<User>> GetAllUsers();
}
public class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;  
    
    public UsersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<User>> GetAllUsers()
    {
        var usersResponse = await _httpClient.GetAsync("https://example.com");
        return new List<User> { };
    }
}
