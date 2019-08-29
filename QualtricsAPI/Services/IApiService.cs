using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QualtricsAPI.Services
{
    public interface IApiService
    {
        Task<Hashtable> FollowUp(string recipientId, string surveyId);

        Task<Hashtable> ResponseGetter(string recipientId, string parentId);

        Task<string> GetResponse(HttpClient client, string surveyId, string responseId);

        Task GetResponseId(HttpClient client, string recipientId, Hashtable ht);

        Task GetClient(HttpClient client);

        Task<string> GetFollowUp(HttpClient client, string recipientId, string surveyId);
    }
}
