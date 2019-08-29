using QualtricsAPI.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace QualtricsAPI.Services
{
    public class QualtricsService : IApiService
    {

        private readonly string token = "qoQi1tYZz45kPt4pObPqq3hovaNAnh0nYZAYrEfc";


        public QualtricsService()
        {
            
        }

        public async Task<Hashtable> FollowUp(string recipientId, string surveyId)
        {
            using (var client = new HttpClient())
            {
                await GetClient(client);
                
                Hashtable ht = new Hashtable();

                var responseId = await GetFollowUp(client, recipientId, surveyId);

                string response = await GetResponse(client, surveyId, responseId);

                ht.Add(surveyId, response);

                return ht;
            }
        }

        public async Task<Hashtable> ResponseGetter(string recipientId, string parentId)
        {
            using (var client = new HttpClient())
            {
                await GetClient(client);

                Hashtable ht = new Hashtable();

                await GetResponseId(client, recipientId, ht);

                await GetResponseId(client, parentId, ht);

                Hashtable responses = new Hashtable();

                foreach(DictionaryEntry pair in ht)
                {
                    var json = await GetResponse(client, pair.Key.ToString(), pair.Value.ToString());
                    responses.Add(pair.Key, json);
                }

                return responses;
            }
        }

        public async Task<string> GetResponse(HttpClient client, string surveyId, string responseId)
        {
            //GET request that gets the survey answers and returns them
            string responseAddress = "https://exetercles.eu.qualtrics.com/API/v3/surveys/" + surveyId + "/responses/" + responseId;

            var response = await client.GetAsync(responseAddress);

            var json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task GetResponseId(HttpClient client, string recipientId, Hashtable ht)
        {
            //GET request that gets the surveyId's and responseId's for all a participants finished surveys 
            string address =
                "https://exetercles.eu.qualtrics.com/API/v3/directories/POOL_bQr7Tx7RBmmX6WF/mailinglists/CG_aVkjcpSJNDUAsFT/contacts/" +
                recipientId + "/history?type=response";

            var response = await client.GetAsync(address);

            var json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);
            Response dynamicJson = JsonConvert.DeserializeObject<Response>(json);
            
            if (dynamicJson != null)
            {
                foreach (Element element in dynamicJson.result.elements)
                {
                    if (!ht.ContainsKey(element.surveyId))
                        ht.Add(element.surveyId, element.responseId);
                }
            }
        }

        public async Task<string> GetFollowUp(HttpClient client, string recipientId, string surveyId)
        {
            //GET request that gets the follow up survey response given the surveyId and recipient
            string address =
                "https://exetercles.eu.qualtrics.com/API/v3/directories/POOL_bQr7Tx7RBmmX6WF/mailinglists/CG_aVkjcpSJNDUAsFT/contacts/" +
                recipientId + "/history?type=response";

            var response = await client.GetAsync(address);

            var json = await response.Content.ReadAsStringAsync();
            Debug.WriteLine(json);
            Response dynamicJson = JsonConvert.DeserializeObject<Response>(json);

            if (dynamicJson != null)
            {
                foreach (Element element in dynamicJson.result.elements)
                {
                    if (element.surveyId == surveyId)
                        return element.responseId;
                }

                return null;
            }

            return null;
        }

        public async Task GetClient(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("X-API-TOKEN", token);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
