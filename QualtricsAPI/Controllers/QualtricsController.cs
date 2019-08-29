using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using QualtricsAPI.Services;
using System.Threading.Tasks;
using QualtricsAPI.Models;
using QualtricsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections;

namespace QualtricsAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/qualtrics")]
    [ApiController]
    public class QualtricsController : ControllerBase
    {
        private readonly IApiService apiService;

        public QualtricsController(IApiService apiService)
        {

            this.apiService = apiService;

        }

        [Route("GetResponse")]
        public async Task<Hashtable> GetResponse(Request request)
        {
            if (request.recipientId == null || request.parentId == null)
            {
                throw new Exception("Must pass in valid recipientId and parentId.");
            }
            Hashtable surveyResponse = await apiService.ResponseGetter(request.recipientId, request.parentId);
            return surveyResponse;
        }

        [Route("GetFollowUp")]
        public async Task<Hashtable> FollowUp(FollowUpRequest request)
        {
            if (request.recipientId == null || request.surveyId == null)
            {
                throw new Exception("Must pass in valid recipientId and surveyId.");
            }
            Hashtable surveyResponse = await apiService.FollowUp(request.recipientId, request.surveyId);
            return surveyResponse;
        }
    }
}
