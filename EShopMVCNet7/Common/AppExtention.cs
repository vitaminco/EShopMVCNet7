using Microsoft.AspNetCore.Http;

namespace EShopMVCNet7.Common
{
    public static class AppExtention
    {
        public static void SetUserId(this HttpContext context, int userId)
        {
            context.Session.SetInt32("userId", userId);
        }
        public static int? GetUserId(this HttpContext context)
        {
            return context.Session.GetInt32("userId");
        }

        public static void SetUserName(this HttpContext context, string userName)
        {
            context.Session.SetString("userName", userName);
        } 
        public static string GetUserName(this HttpContext context)
        {
            return context.Session.GetString("userName") ?? "";
        }
        public static void SetRole(this HttpContext context, UserRole role)
        {
            context.Session.SetInt32("role",(int) role);
        }
        public static bool IsAdmin(this HttpContext context)
        {
            return context.Session.GetInt32("role") == (int)UserRole.ROLEADMIN;
        }
    }
}
