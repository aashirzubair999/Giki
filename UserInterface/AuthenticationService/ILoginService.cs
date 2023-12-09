
using Model.Models;

namespace UserInterface.AuthenticationService;

public interface ILoginService
{
    Task Login(UserSession userSession);
    Task Logout();
}