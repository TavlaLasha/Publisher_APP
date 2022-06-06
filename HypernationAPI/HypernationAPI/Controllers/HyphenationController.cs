using BLL;
using BLL.Contracts;
using Microsoft.Office.Interop.Word;
using Models.DataViewModels;
using Models.DataViewModels.DocManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HypernationAPI.Controllers
{
    public class HyphenationController : ApiController
    {
        readonly IDocManagement _docManagement;
        public HyphenationController(IDocManagement docManagement)
        {
            _docManagement = docManagement;
        }        

        [HttpGet]
        [Route("api/HypDoc/{fileName}")]
        public HttpResponseMessage HyphenateDoc(string fileName)
        {
            try
            {
                if(fileName == "" || fileName == " ")
                    throw new HttpException("File Name is Required Parameter");

                HttpResponseMessage result;
                string FileFolder = Path.GetFileNameWithoutExtension(fileName);
                string filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/{fileName}");

                if (!File.Exists(filePath))
                    throw new HttpException("File does not exist in temporary storage");

                string modifiedFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/Modified/{fileName}");

                if (!_docManagement.HyphenateDocument(filePath, modifiedFilePath))
                {
                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    result.Content = new StringContent("API Failed To Hyphenate Given File");
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
        [Route("api/HypText")]
        public HttpResponseMessage HyphenateText(TextDTO text)
        {
            try
            {
                if (string.IsNullOrEmpty(text.Text) || text.Text.Length < 3)
                    throw new HttpException("Text length should be minimum 3 characters");

                HttpResponseMessage result;
                text.Text = _docManagement.HyphenateText(text.Text);
                if (string.IsNullOrEmpty(text.Text))
                {
                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    result.Content = new StringContent("API Failed To Hyphenate Given Text");
                    return result;
                }

                string jObject = JsonConvert.SerializeObject(text);

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

        [HttpGet]
        [Route("api/Test")]
        public HttpResponseMessage Test()
        {
            HttpResponseMessage result = Request.CreateResponse((HttpStatusCode)418);
            result.Content = new StringContent("You are the best! Keep on going!");
            return result;
        }

    }
}
