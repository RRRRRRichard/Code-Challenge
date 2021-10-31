using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Code.Models;
using System.Net;
using System.IO;
using Newtonsoft.Json;
namespace Code.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        // POST api/city
        [HttpPost]
        public String Post([FromBody] AddRequest req)
        {
            string ip = req.ip;
            string url = "http://api.ipstack.com/"+ ip +"?access_key=aadde9a2e8e1ce56f04fd8dc505bccad";        
            string jsstr = Post(url,"");
            var address = JsonConvert.DeserializeObject<Address>(jsstr);

            Response2 resp = new Response2();
            try
            {
                resp.city = address.city;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return resp.city;
        }
        [HttpGet]
         public  string Post(string Url, string jsonParas)
        {
            string strURL = Url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);

            request.Method = "POST";

            request.ContentType = "application/json";

            request.Timeout = 10000;
 
            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   
 
            byte[] payload;

            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
 
            request.ContentLength = payload.Length;

 
            Stream writer;
            try
            {
                writer = request.GetRequestStream();
            }
            catch (Exception)
            {
                writer = null;
                Console.Write("Failed to connect!");
            }
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response as HttpWebResponse;
            }
            Stream s = response.GetResponseStream();
            StreamReader sRead = new StreamReader(s);
            string postContent = sRead.ReadToEnd();
            sRead.Close();
            return postContent;
        }
    }
}
