using BusinessLogicLayer.Data;

namespace GestContactAPI.Tools
{
    public interface ITokenManager
    {
        string GenerateJWT(UserClient user);
    }
}