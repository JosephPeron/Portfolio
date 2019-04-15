using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;

namespace SecuroteckClient
{
    #region Task 10 and beyond
    class Client
    {
        static HttpClient client = new HttpClient();
        static string apiKey = "";
        static string userName = "";
        static string publicKey = "";
        static UnicodeEncoding _encoder = new UnicodeEncoding();

        static void Main(string[] args)
        {
            string myserver = "http://localhost:24702/";
            string testserver = "http://secur-o-teck-student-test.azurewebsites.net/1472622/";
            client.BaseAddress = new Uri(testserver);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine("Hello. What would you like to do?");

            string response = Console.ReadLine();
            while (response != "Exit")
            {
                Console.Clear();
                string output = "";
                string path = "";

                string[] parts = response.Split(new char[] { ' ' }, 3);

                switch (parts[0])
                {
                    case "TalkBack":
                        if (parts[1] == "Hello")
                        {
                            path = "api/talkback/hello";
                            Console.WriteLine("...please wait...");
                            output = TalkBackHello(path).Result;
                        }
                        else if (parts[1] == "Sort")
                        {
                            path = "api/talkback/sort?";
                            Console.WriteLine("...please wait...");
                            output = TalkBackSort(parts[2], path).Result;
                        }
                        break;
                    case "User":
                        if (parts[1] == "Get")
                        {
                            path = "api/user/new?";
                            Console.WriteLine("...please wait...");
                            output = UserGet(path, parts[2]).Result;
                        }
                        else if (parts[1] == "Post")
                        {
                            path = "api/user/new";
                            Console.WriteLine("...please wait...");
                            output = UserPost(path, parts[2]).Result;
                        }
                        else if (parts[1] == "Set")
                        {
                            apiKey = parts[1];
                            userName = parts[2];
                            output = "Stored";
                        }
                        else if (parts[1] == "Delete")
                        {
                            path = "api/user/removeuser?";
                            Console.WriteLine("...please wait...");
                            if (apiKey != "" || userName != "")
                            {
                                output = UserDelete(path).Result;

                            }
                            else
                            {
                                output = "You need to do a User Post or User Set first";
                            }
                        }
                        else if (parts[1] == "Role")
                        {
                            path = "api/user/changerole";
                            Console.WriteLine("...please wait...");
                            if (apiKey != "")
                            {
                                string[] inputs = parts[2].Split(' ');
                                string name = inputs[0];
                                string role = inputs[1];
                                output = UserRole(path, name, apiKey, role).Result;

                            }
                            else
                            {
                                output = "You need to do a User Post or User Set first";
                            }

                        }
                        break;
                    case "Protected":
                        if (parts[1] == "Hello")
                        {
                            path = "api/protected/hello";
                            Console.WriteLine("...please wait...");
                            if (apiKey != "")
                            {
                                output = ProtectedHello(path).Result;
                            }
                            else { output = "You need to do a User Post or User Set first"; }
    }
                        else if (parts[1] == "SHA1")
                        {
                            path = "api/protected/sha1?";
                            Console.WriteLine("...please wait...");                         
                            if (apiKey != "")
                            {
                                output = output = ProtectedSHA1(path, parts[2]).Result;
                            }
                            else { output = "You need to do a User Post or User Set first"; }
                        }
                        else if (parts[1] == "SHA256")
                        {
                            path = "api/protected/sha256?";
                            Console.WriteLine("...please wait...");
                            if (apiKey != "")
                            {
                                output = output = ProtectedSHA256(path, parts[2]).Result;
                            }
                            else { output = "You need to do a User Post or User Set first"; }
                        }
                        else if (parts[1] == "Get" && parts[2] == "PublicKey")
                        {
                            path = "api/protected/getpublickey";
                            Console.WriteLine("...please wait...");
                            if (apiKey != "")
                            {
                                output = ProtectedGetPublicKey(path).Result;
                            }
                            else { output = "You need to do a User Post or User Set first"; }
                        }
                        else if (parts[1] == "Sign")
                        {
                            if (apiKey != "")
                            {
                                path = "api/protected/sign?";
                                Console.WriteLine("...please wait...");
                                output = ProtectedSign(path, parts[2]).Result;
                            }
                            else { output = "You need to do a User Post or User Set first"; }
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }
                Console.WriteLine(output);
                Console.WriteLine("What would you like to do next ?");
                response = Console.ReadLine();
            }
        }

        public static async Task<string> TalkBackHello(string path)
        {
            string output = "";
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsStringAsync();
            }
            return output;
        }

        public static async Task<string> TalkBackSort(string input, string path)
        {
            string output = "";
            string request = "";
            string trimmedinput = input.Trim(new char[] { '[', ']' });
            string[] numbers = trimmedinput.Split(',');
            request = "integers=" + numbers[0];
            for(int i = 1; i < numbers.Length; i++)
            {
                request += "&integers=" + numbers[i];
            }
            HttpResponseMessage response = await client.GetAsync(path + request);
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsStringAsync();
            }
            return output;
        }

        public static async Task<string> UserGet(string path, string user)
        {
            string output = "";
            string request = "username=" + user;
            HttpResponseMessage response = await client.GetAsync(path + request);
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsStringAsync();
            }
            return output;
        }

        public static async Task<string> UserPost(string path, string user)
        {
            string output = "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent("'" + user + "'", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            output = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                apiKey = output;
                apiKey = apiKey.Replace("\"", "");
                userName = user;
                return "Got API Key";
            }
            return output;
        }

        public static async Task<string> UserDelete(string path)
        {
            string output = "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, path + "username=" + userName);
            request.Headers.Add("ApiKey", apiKey);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return "True";
            }
            return "False";
        }

        public static async Task<string> UserRole(string path, string name, string api, string role)
        {
            string output = "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Headers.Add("ApiKey", api);
            request.Content = new StringContent("{ \"username\" : \"" + name + "\",\"role\" : \"" + role + "\" }", Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            output = await response.Content.ReadAsStringAsync();
            return output;
        }

        public static async Task<string> ProtectedHello(string path)
        {
            string output = "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Add("ApiKey", apiKey);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsStringAsync();
            }
            return output;
        }

        public static async Task<string> ProtectedSHA1(string path, string message)
        {
            string output = "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path+"message="+message);
            request.Headers.Add("ApiKey", apiKey);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsStringAsync();
            }
            return output;
        }

        public static async Task<string> ProtectedSHA256(string path, string message)
        {
            string output = "";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path + "message=" + message);
            request.Headers.Add("ApiKey", apiKey);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                output = await response.Content.ReadAsStringAsync();
            }
            return output;
        }

        public static async Task<string> ProtectedGetPublicKey(string path)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path);
            request.Headers.Add("ApiKey", apiKey);
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                publicKey = await response.Content.ReadAsStringAsync();
                publicKey = publicKey.Replace("\"",string.Empty);
                return "Got Public Key";
            }
            return "Couldn’t Get the Public Key";
        }

        public static async Task<string> ProtectedSign(string path, string message)
        {
            if (publicKey != "")
            {
                string signature = "";
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                byte[] asciiByteMessage = Encoding.ASCII.GetBytes(message);
                rsa.FromXmlString(publicKey);
                byte[] decryptedMsg = rsa.Decrypt(asciiByteMessage, false);
                string puzzle = _encoder.GetString(decryptedMsg);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, path + "message=" + message);
                request.Headers.Add("ApiKey", apiKey);
                HttpResponseMessage response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    signature = await response.Content.ReadAsStringAsync();
                    byte[] retval = new byte[signature.Length / 2];
                    for (int i = 0; i < signature.Length; i += 2)
                    {
                        retval[i / 2] = Convert.ToByte(signature.Substring(i, 2), 16);
                    }
                    if (rsa.VerifyData(decryptedMsg, new SHA1CryptoServiceProvider(), retval))
                    {
                        return "Message was successfully signed";
                    }
                    return "Message was not successfully signed";
                }
                return "Bad Request";
            }
            return "Client doesn’t yet have the public key";
        }
    }
    #endregion

}
