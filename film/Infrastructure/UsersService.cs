namespace film.Infrastructure.Repository
{
    public class UsersService
    {
        public static string GetUserRole(string Login, string Password)
        {
            if (Login.ToLower() == "admin" && Password.ToLower() == "admin")
            {
                return "Admin";
            }
            else if (Login.ToLower() == "user" && Password.ToLower() == "user")
            {
                return "User";
            }
            return null;
        }
    }
}