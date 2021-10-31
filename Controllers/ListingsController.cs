using System.Runtime.CompilerServices;
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
    public class ListingsController : ControllerBase
    {
        // POST api/city
        [HttpPost]
        public List<Listing> Post([FromBody] AddRequest req)
        {
            int numOfPass = req.numOfPass;
            string url = "https://jayridechallengeapi.azurewebsites.net/api/QuoteRequest";        
            string jsstr = PostHelper(url,"");
            Vehicle vehicle = JsonConvert.DeserializeObject<Vehicle>(jsstr);
            List<Listing> filteredList = new List<Listing>();
            
            foreach (var item in vehicle.listings)
            {
                if(item.vehicleType.maxPassengers >= numOfPass)
                {
                    item.totalPrice = item.pricePerPassenger*numOfPass;
                    filteredList.Add(item);
                }
            }

            List<Listing> newList=filteredList.OrderBy(o =>o.totalPrice).ToList();
  
            return newList;
        }
      
         public  string PostHelper(string Url, string jsonParas)
        {
            string strURL = Url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);

            request.Method = "GET";

            request.ContentType = "application/json";

            request.Timeout = 10000;
 
            string paraUrlCoded = jsonParas;//System.Web.HttpUtility.UrlEncode(jsonParas);   
  
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
