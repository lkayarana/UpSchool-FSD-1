using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomPasswordGenerator
{
    public static class Inputs
    {
        public static class Questions 
        {
            public static string IncludeNumbers => "Do you want to include numbers?";

            public static string IncludeLowercaseCharacters => "OK! How about lowercase characters?";

            public static string IncludeUppercaseCharacters => "Very nice! How about uppercase characters?";

            public static string IncludeSpecialCharachters => "All right! We are almost done. Would you also like to add speacial characters?";

            public static string PasswordLength => "Great! Lastly, how long do you want to keep ypur password lenght?";

        }

    }
}
