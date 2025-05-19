using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FortniteClient_Win64_Shipping.Classes.HTTPEngine
{
    class RequestSaving
    {
        public class BuildInfo
        {
            public string? app { get; set; }
            public DateTime serverDate { get; set; }
            public string? cln { get; set; }
            public string? build { get; set; }
            public DateTime buildDate { get; set; }
            public string? version { get; set; }
            public string? branch { get; set; }
        }

        public class GetCreds
        {
            public static string? access_token { get; set; }
            public static int expires_in { get; set; }
            public static string? expires_at { get; set; }
            public static string? token_type { get; set; }
            public static string? refresh_token { get; set; }
            public static int refresh_expires { get; set; }
            public static string? refresh_expires_at { get; set; }
            public static string? account_id { get; set; }
            public static string? client_id { get; set; }
            public static string? client_service { get; set; }
            public static string? displayName { get; set; }
            public static string? app { get; set; }
            public static string? in_app_id { get; set; }
            public static string? device_id { get; set; }
            public static string? authToken { get; set; }
        }

        public class GetAccessToken
        {
            public static string? access_token { get; set; }
            public static string? client_id { get; set; }
            public static string? client_service { get; set; }
            public static string? expires_at { get; set; }
            public static int expires_in { get; set; }
            public static string? token_type { get; set; }
        }
    }
}
