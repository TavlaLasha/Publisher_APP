using HypBLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HypernationAPI.Controllers
{
    public class HYPController : ApiController
    {
        [HttpPost]
        [Route("api/TakeDoc")]
        public HttpResponseMessage HypernateDocAsync()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;
            var filePath = "";
            var modifiedFilePath = "";
            if (httpRequest.Files.Count > 0)
            {
                //var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    filePath = HttpContext.Current.Server.MapPath("~/TempDocs/" + postedFile.FileName);
                    modifiedFilePath = HttpContext.Current.Server.MapPath("~/TempDocs/Modified/m_" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    //docfiles.Add(filePath);

                    if (!WorkWithDoc.ProcessWordDocument(filePath, modifiedFilePath))
                    {
                        result = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                    }


                    result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StreamContent(File.OpenRead(modifiedFilePath))

                    };
                    result.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = postedFile.FileName
                    };
                    var contentType = MimeMapping.GetMimeMapping(Path.GetExtension(modifiedFilePath));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }
    }
}
