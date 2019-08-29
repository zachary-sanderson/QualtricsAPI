using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualtricsAPI.Models
{

    public class Meta
    {
        public string httpStatus { get; set; }
        public string requestId { get; set; }
    }

    public class Element
    {
        public string responseId { get; set; }
        public string surveyId { get; set; }
        public string surveyStartedDate { get; set; }
        public string surveyCompletedDate { get; set; }
        public string distributionId { get; set; }
        public string surveyFinished { get; set; }
    }

    public class Result
    {
        public List<Element> elements { get; set; }
        public string nextPage { get; set; }
    }

    public class Response
    {
        
        public Result result { get; set; }
        public Meta meta { get; set; }
    }

}
