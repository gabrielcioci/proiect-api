using System;
namespace backend_II
{
    public interface IJwtAuthManager
    {
        string Authenticate(string email, string password);
    }
}
