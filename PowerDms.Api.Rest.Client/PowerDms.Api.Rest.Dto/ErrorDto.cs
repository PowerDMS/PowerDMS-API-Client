using System.Collections.Generic;

namespace PowerDms.Api.Rest.Dto
{
    public class ErrorDto
    {
        public string Code { get; set; }

        public int HttpStatusCode { get; set; }

        public IEnumerable<string> Messages { get; set; }
    }
}