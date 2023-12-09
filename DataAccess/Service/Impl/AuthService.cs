using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using ClassLibraryDAL;
using Controller.Services;
using DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Model.Dto;
using Model.Models;

namespace DataAccess.Service.Impl;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;


    public AuthService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }
   
    public async Task<bool> LoginUser(AuthDto auth)
    {
        User? user =  await _userRepo.GetUser(auth.Email);
        
        if (user == null)
        {
            throw new Exception("User Not Found");
        }

        if (!BCrypt.Net.BCrypt.Verify(auth.Password, user.PasswordHash))
        {
            throw new Exception("Incorrect Credentials");
        }
        
        return true;
    }
    
   

  
}