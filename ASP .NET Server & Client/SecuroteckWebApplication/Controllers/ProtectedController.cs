using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Web.Http.Results;
using System.Security.Cryptography;
using SecuroteckWebApplication.Models;

using System.Text;


namespace SecuroteckWebApplication.Controllers
{
    public class ProtectedController : ApiController
    {
        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

        [ActionName("hello")]
        [APIAuthorise]
        public IHttpActionResult Get()
        {
            if (HttpContext.Current.Request.Headers["apikey"] != null)
            {
                string apikey = HttpContext.Current.Request.Headers["apikey"].ToString();
                if (UserDatabaseAccess.CheckAPIKeyExists(apikey))
                {
                    string name = UserDatabaseAccess.CheckAPIReturnName(apikey);
                    return Ok("Hello " + name);
                }
                return Ok("Hello");
            }
            return Ok("Hello");

        }

        [ActionName("sha1")]
        [APIAuthorise]
        public IHttpActionResult GetSha1([FromUri]string message)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                SHA1 sha1 = SHA1.Create();
                byte[] hashBytes = sha1.ComputeHash(bytes);
                return Ok(HexStringFromBytes(hashBytes).ToUpperInvariant());
            }
            catch
            { return BadRequest("Bad Request"); }
        }

        [ActionName("sha256")]
        [APIAuthorise]
        public IHttpActionResult GetSha256([FromUri]string message)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                SHA256 sha1 = SHA256.Create();
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return Ok(HexStringFromBytes(hashBytes).ToUpperInvariant());
            }
            catch { return BadRequest("Bad Request"); }
        }

        [ActionName("sign")]
        [APIAuthorise]
        public IHttpActionResult GetSign([FromUri]string message)
        {
            byte[] asciiByteMessage = Encoding.ASCII.GetBytes(message);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(WebApiConfig.privateKey);
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(rsa);
            RSAFormatter.SetHashAlgorithm("SHA1");
            SHA1Managed SHhash = new SHA1Managed();
            byte[] signedBytes = RSAFormatter.CreateSignature(SHhash.ComputeHash(asciiByteMessage));
            StringBuilder hex = new StringBuilder(signedBytes.Length * 2);
            foreach (byte b in signedBytes)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            string output = "";
            char[] hexes = hex.ToString().ToCharArray();
            output = hexes[0].ToString() + hexes[1].ToString();
            for (int i = 2; i < hexes.Length; i += 2)
            {
                output += "-" + hexes[i] + hexes[i + 1];
            }
            return Ok(output);
        }

        [ActionName("getpublickey")]
        [APIAuthorise]
        public IHttpActionResult GetPublicKey()
        {
            //send back RSA public key
            return Ok(WebApiConfig.publicKey);
        }
    }
}