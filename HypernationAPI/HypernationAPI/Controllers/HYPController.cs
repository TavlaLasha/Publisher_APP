using HypBLL;
using HypBLL.Interfaces;
using Microsoft.Office.Interop.Word;
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
    public class HYPController : ApiController
    {
        readonly IWorkWithDoc _workWithDoc;
        public HYPController(IWorkWithDoc workWithDoc)
        {
            _workWithDoc = workWithDoc;
        }

        [HttpPost]
        [Route("api/TakeDoc")]
        public HttpResponseMessage TakeDoc()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                HttpResponseMessage result;

                if (httpRequest.Files.Count == 1)
                {
                    var postedFile = httpRequest.Files[0];
                    var contentType = Path.GetExtension(postedFile.FileName);
                    string FileName = "Null";
                    //Console.WriteLine(contentType);
                    if (contentType == ".docx" || contentType == ".doc" || contentType == ".rtf")
                    {
                        FileName = postedFile.FileName;
                        var filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileName}");
                        if (File.Exists(filePath))
                        {
                            FileName = $"{Path.GetFileNameWithoutExtension(FileName)}{DateTime.Now.Ticks}{contentType}";
                            filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileName}");
                        }
                        postedFile.SaveAs(filePath);

                        FileInfo fs = new FileInfo(filePath);
                        string FileSize = fs.Length.ToString();
                        //if (fs.Length/1024 > 1024)
                        //{
                        //    FileSize = $"{fs.Length / 1048576} MB";
                        //}
                        //else
                        //{
                        //    FileSize = $"{fs.Length / 1024} Kb";
                        //}

                        var jObject = JsonConvert.SerializeObject(new Dictionary<string, string>() { { "FileName", FileName }, { "FileSize", FileSize } });

                        result = new HttpResponseMessage(HttpStatusCode.Accepted)
                        {
                            Content = new StringContent(jObject, Encoding.UTF8, "application/json")
                            //Content = new StringContent($"{FileName};{FileSize}")
                        };
                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                        result.Content = new StringContent("API Supports Only .docx .doc .rtf Media Types");
                    }
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.ExpectationFailed);
                }
                return result;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/GetDocPage/{fileName}")]
        public HttpResponseMessage GetDocPage(string fileName, int page=1)
        {
            try
            {
                if (fileName == "" || fileName == " ")
                {
                    throw new HttpException("File Name is Required Parameter");
                }
                HttpResponseMessage result;
                var filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/m_{fileName}");
                if (!File.Exists(filePath))
                {
                    filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");
                    if (!File.Exists(filePath))
                    {
                        throw new HttpException("File does not exist in temporary storage");
                    }
                }
                var data = _workWithDoc.GetPage(filePath, page);

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(data[0])
                };
                result.Headers.Add("PageCount", data[1]);
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("api/HypDoc/{fileName}")]
        public HttpResponseMessage HypernateDoc(string fileName)
        {
            try
            {
                if(fileName == "" || fileName == " ")
                {
                    throw new HttpException("File Name is Required Parameter");
                }
                HttpResponseMessage result;
                var filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");
                if (File.Exists(filePath))
                {
                    var modifiedFilePath = HttpContext.Current.Server.MapPath("~/TempDocs/Modified/m_" + fileName);

                    if (!_workWithDoc.HypernateDocument(filePath, modifiedFilePath))
                    {
                        result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        result.Content = new StringContent("API Failed To Hypernate Given File");
                        return result;
                    }

                    result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent($"{fileName}")
                    };
                }
                else
                {
                    throw new HttpException("File does not exist in temporary storage");
                }
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
        [Route("api/SaveWork/{fileName}")]
        public HttpResponseMessage SaveWork(string fileName, bool pdf = false)
        {
            try
            {
                if (fileName == "")
                {
                    throw new HttpException("File name is required parameter");
                }
                string FileName = fileName;
                HttpResponseMessage result;
                var modifiedFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/m_{FileName}");
                if (File.Exists(modifiedFilePath))
                {
                    string OutputFilePath;
                    if (pdf)
                    {
                        FileName = $"{ Path.GetFileNameWithoutExtension(fileName)}.pdf";
                        OutputFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/m_{FileName}");
                        if (!_workWithDoc.ConvertToPDF(modifiedFilePath, OutputFilePath, WdSaveFormat.wdFormatPDF))
                        {
                            throw new HttpException("Could not convert to PDF");
                        }
                    }
                    else
                    {
                        OutputFilePath = modifiedFilePath;
                    }
                    result = new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StreamContent(File.OpenRead(OutputFilePath))
                    };
                    result.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = FileName
                    };
                    var contentType = MimeMapping.GetMimeMapping(Path.GetExtension(OutputFilePath));
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                }
                else
                {
                    throw new HttpException("File does not exist in temporary storage");
                }
                return result;
            }
            catch (HttpException ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
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
