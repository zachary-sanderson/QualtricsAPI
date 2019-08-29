using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualtricsAPI.Models
{
    public class FollowUpRequest
    {
        public string recipientId { get; set; }
        public string parentId { get; set; }
        public string surveyId { get; set; }
    }
}
