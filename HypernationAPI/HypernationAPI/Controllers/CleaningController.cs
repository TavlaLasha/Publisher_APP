using BLL.Contracts;
using Models.DataViewModels;
using Models.DataViewModels.DocManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HypernationAPI.Controllers
{
    public class CleaningController : ApiController
    {
        readonly IDocManagement _docManagement;
        public CleaningController(IDocManagement docManagement)
        {
            _docManagement = docManagement;
        }
        [HttpGet]
        [Route("api/CleanDoc/{fileName}")]
        public HttpResponseMessage CleanDoc(string fileName, bool CleanSpaces=false, bool CleanExcessParagraphs = false, bool CleanNewLines = false, bool CleanTabs = false, bool CorrectPDashStarts = false)
        {
            try
            {
                if (fileName == "" || fileName == " ")
                    throw new HttpException("File Name is Required Parameter");
                //POST request on this method caused CORS policy problem.
                DocCleanDTO dCl = new DocCleanDTO()
                {
                    CleanSpaces = CleanSpaces,
                    CleanExcessParagraphs = CleanExcessParagraphs,
                    CleanNewLines = CleanNewLines,
                    CleanTabs = CleanTabs,
                    CorrectPDashStarts = CorrectPDashStarts
                };

                HttpResponseMessage result;
                string FileFolder = Path.GetFileNameWithoutExtension(fileName);
                string filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/{fileName}");
                string modifiedFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/Modified/{fileName}");

                if (!File.Exists(filePath))
                    throw new HttpException("File does not exist in temporary storage");

                if (File.Exists(modifiedFilePath))
                    filePath = modifiedFilePath;

                if (!_docManagement.CleanDocument(filePath, modifiedFilePath, dCl))
                {
                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    result.Content = new StringContent("API Failed To Clean Given File");
                    return result;
                }
                FileInfo fs = new FileInfo(filePath);
                long FileSize = fs.Length;

                string jObject = JsonConvert.SerializeObject(new DocDTO() { FileName = fileName, FileSize = FileSize });

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(jObject, Encoding.UTF8, "application/json")
                };

                return result;
            }
            catch (HttpException ex)
            {
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest);
                result.Content = new StringContent(ex.Message);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("api/CleanText")]
        public HttpResponseMessage CleanText(CleanTextDTO dCl)
        {
            try
            {
                string text = dCl.textDTO.Text;
                if (string.IsNullOrEmpty(text))
                    throw new HttpException("File Name is Required Parameter");

                HttpResponseMessage result;

                text = _docManagement.CleanText(text, dCl.docCleanDTO);

                if (string.IsNullOrEmpty(text))
                {
                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    result.Content = new StringContent("API Failed To Clean Given Text");
                    return result;
                }
                dCl.textDTO.Text = text;
                string jObject = JsonConvert.SerializeObject(dCl.textDTO);

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(jObject, Encoding.UTF8, "application/json")
                };

                return result;
            }
            catch (HttpException ex)
            {
                HttpResponseMessage result = Request.CreateResponse(HttpStatusCode.BadRequest);
                result.Content = new StringContent(ex.Message);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
