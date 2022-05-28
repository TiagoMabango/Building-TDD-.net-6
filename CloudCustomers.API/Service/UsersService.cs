﻿using CloudCustomers.API.Models;
using Microsoft.Extensions.Options;
using System;
using UsersAPI.cofig;

public interface IUsersService
{
    public Task<List<User>> GetAllUsers();
}
public class UsersService : IUsersService
{
    private readonly HttpClient _httpClient;  
    private readonly UsersApiOptions _apiConfig;

    public UsersService(
        HttpClient httpClient, IOptions<UsersApiOptions> apiConfig
        ){
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }
    public async Task<List<User>> GetAllUsers()
    {
        var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);

        if(usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<User> ();
        }
        var responseContent = usersResponse.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();

        return allUsers.ToList();
    }
}
