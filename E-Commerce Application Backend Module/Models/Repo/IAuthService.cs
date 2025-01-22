using CURDAPI.Models;

namespace E_Commerce_Application_Backend_Module.Models.Repo
{
    public interface IAuthService
    {
        string GenerateToken(Users user);
    }
}
