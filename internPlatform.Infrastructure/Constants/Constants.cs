using System;
using System.Collections.Generic;

namespace internPlatform.Infrastructure.Constants
{
    public static class Constants
    {
        //  Text, View , Controller, Area : Icon
        public static List<Tuple<string, string, string, string, string>> PrivateTabs = new List<Tuple<string, string, string, string, string>>
        {
            Tuple.Create( "Entities", "Entities", "Dashboard" , "Admin", "bi-boxes") ,
            Tuple.Create( "Links", "Links","Dashboard" , "Admin","bi-link-45deg") ,
            Tuple.Create( "Users", "Users","Dashboard" , "Admin", "bi-people-fill") ,

        };

        // ViewName : DisplayName
        public static List<Tuple<string, string, string, string, string>> PublicTabs = new List<Tuple<string, string, string, string, string>>
        {
            Tuple.Create( "Events", "Events", "Dashboard" , "Admin", "bi-calendar4-event") ,
            Tuple.Create( "Statistics", "Statistics", "Dashboard" , "Admin", "bi-boxes") ,
        };

        public static string[] Roles = { "SuperAdmin", "Admin" };
    }
}
