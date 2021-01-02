using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using testclientadmin.core.Models;

namespace testclientadmin.core
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:8000/api/Admin/User/Get?id=1");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Connection", "keep-alive");
            request.AddHeader("accept-encoding", "gzip, deflate");
            request.AddHeader("Host", "localhost:8000");
            request.AddHeader("Postman-Token", "caab4c4f-1826-4915-95aa-4816c2d9d495,d09b82e1-8e16-47cf-a1e2-cb5a22717480");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("User-Agent", "PostmanRuntime/7.13.0");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIiLCJuYSI6ImFkbWluIiwibm4iOiLnrqHnkIblkZgiLCJ0ZW5hbnRpZCI6IjAiLCJyZSI6IjE2MDk3MDU0ODkiLCJuYmYiOjE2MDk2MTE4ODksImV4cCI6MTYwOTYxOTA4OSwiaXNzIjoiaHR0cDovLzEyNy4wLjAuMTo4MDAwIiwiYXVkIjoiaHR0cDovLzEyNy4wLjAuMTo4MDAwIn0.0ujV8vZJFCsjlL94BKXwanm7wxlyr38CJw__aiv3BsA");
            IRestResponse response = client.Execute(request);

            //var getResponsecontent = response.Content;
  
            var customerDto = JsonConvert.DeserializeObject<Result>(response.Content);
            AdUser user = customerDto.data;
        }
    }
}
