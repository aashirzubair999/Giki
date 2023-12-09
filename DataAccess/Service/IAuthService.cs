using Model.Dto;
using Model.Models;

namespace Controller.Services;

public interface IAuthService
{
    

    Task<bool> LoginUser(AuthDto auth);
}