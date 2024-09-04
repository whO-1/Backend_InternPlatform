using System;
using System.Collections.Generic;

namespace internPlatform.Infrastructure.Constants
{
    public static class Constants
    {
        // { Text, View , Controller, Area : Icon }
        public static List<Tuple<string, string, string, string, string>> PrivateTabs = new List<Tuple<string, string, string, string, string>>
        {
            Tuple.Create( "Entities", "Entities", "Dashboard" , "Admin", "bi-boxes") ,
            Tuple.Create( "Links", "Links","Dashboard" , "Admin","bi-link-45deg") ,
            Tuple.Create( "Users", "Users","Dashboard" , "Admin", "bi-people-fill") ,
            Tuple.Create( "SuperAdmin Statistics", "SuperAdminStatistics", "Dashboard" , "Admin", "bi-bar-chart-line-fill") ,
            Tuple.Create( "Errors Statistics", "ErrorsStatistics", "Dashboard" , "Admin", "bi bi-bug-fill") ,
        };

        // { Text, View , Controller, Area : Icon }
        public static List<Tuple<string, string, string, string, string>> PublicTabs = new List<Tuple<string, string, string, string, string>>
        {
            Tuple.Create( "Events", "Events", "Dashboard" , "Admin", "bi-calendar4-event") ,
            Tuple.Create( "User statistics", "UserStatistics", "Dashboard" , "Admin", "bi bi-graph-up-arrow") ,
        };

        //-------------------Users-----------------------------
        public static string[] Roles = { "SuperAdmin", "Admin" };
        public static string SuperAdminEmail = "admin@mail.com";
        public static string SuperAdminPassword = "master";
        public static string AdminEmail = "user@mail.com";
        public static string AdminPassword = "userPass";


        //----------------File_Service-------------------------
        public static string TempFolder = "Temp";
        public static string FullSizeFolder = "FullSize";
        public static string ThumbnailsFolder = "Thumbnails";
        public static int MaxImageSize = 2048; //bytes
        public static int MaxFilesPerPost = 5;
        public static string[] ValidImageTypes = new string[] { "image/jpeg", "image/png" };
        public static string RootPath = "C:\\Users\\ddiocher\\source\\repos\\internPlatform\\Resources";




    }
}
