using HypBLL;
using Microsoft.Office.Interop.Word;
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
using System.Web.Http.Cors;

namespace HypernationAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HYPController : ApiController
    {
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
                            string newFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileName}");
                            postedFile.SaveAs(newFilePath);
                        }
                        else
                        {
                            postedFile.SaveAs(filePath);
                        }

                        result = new HttpResponseMessage(HttpStatusCode.Accepted)
                        {
                            Content = new StringContent($"{FileName}")
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
                var filePath = HttpContext.Current.Server.MapPath("~/TempDocs/Modified/m_" + fileName);
                if (!File.Exists(filePath))
                {
                    filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");
                    if (!File.Exists(filePath))
                    {
                        throw new HttpException("File does not exist in temporary storage");
                    }
                }



                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent($"{fileName}")
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

                    if (!WorkWithDoc.ProcessWordDocument(filePath, modifiedFilePath))
                    {
                        result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                        result.Content = new StringContent("API Failed In Hypernating Given File");
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
                        if (!WorkWithDoc.ConvertToPDF(modifiedFilePath, OutputFilePath, WdSaveFormat.wdFormatPDF))
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
