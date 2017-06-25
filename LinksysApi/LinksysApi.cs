using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace LinksysApi
{
    public class LinksysApi
    {
        public class TcpClient
        {
            public string Expire { get; set; }
            public string IP { get; set; }
            public string Mac { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Strength { get; set; }
        }

        public static IEnumerable<TcpClient> GetClients(string hostname, string username, string password)
        {
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(username, password);

                var clientPageUrl = string.Format("http://{0}/DHCPTable.asp", hostname);
                var input = client.DownloadString(clientPageUrl);

                //table[0] = new AAA('192-168-1-102', '192.168.1.102', '00:26:18:AA:28:01', '16:01:45', 'LAN');
                var matches = Regex.Matches(input, @"^table(.*)\('(.*)','(.*)','(.*)','(.*)','(.*)'\);$", RegexOptions.Multiline);

                return from match in matches.Cast<Match>()
                       select new TcpClient
                       {
                           Name = match.Groups[2].Value,
                           IP = match.Groups[3].Value,
                           Mac = match.Groups[4].Value,
                           Expire = match.Groups[5].Value,
                           Type = match.Groups[6].Value,
                       };
            }
        }
    }
}
