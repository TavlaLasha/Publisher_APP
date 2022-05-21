﻿using BLL;
using BLL.Interfaces;
using Microsoft.Office.Interop.Word;
using Models.DataViewModels;
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
        readonly IDocManagement _docManagement;
        public HYPController(IDocManagement docManagement)
        {
            _docManagement = docManagement;
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
                        long FileSize = fs.Length;

                        var jObject = JsonConvert.SerializeObject(new DocDTO() { FileName = FileName, FileSize = FileSize });

                        result = new HttpResponseMessage(HttpStatusCode.Accepted)
                        {
                            Content = new StringContent(jObject, Encoding.UTF8, "application/json")
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
                    result.Content = new StringContent("No File Received");
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
        [Route("api/GetDocPages/{fileName}")]
        public HttpResponseMessage GetDocPages(string fileName, int page=1)
        {
            try
            {
                if (fileName == "" || fileName == " ")
                    throw new HttpException("File Name is Required Parameter");

                HttpResponseMessage result;
                var filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/{fileName}");
                if (!File.Exists(filePath))
                {
                    filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");
                    if (!File.Exists(filePath))
                        throw new HttpException("File does not exist in temporary storage");
                }
                var data = _docManagement.GetPages(filePath, page);

                string zipFileName = $"{Path.GetFileNameWithoutExtension(fileName)}.zip";
                string zipFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Temp/{zipFileName}");

                if (!_docManagement.ZipUpFiles(data[0], zipFilePath))
                    throw new HttpException("Error when zipping up files");

                result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(File.OpenRead(zipFilePath))
                };
                result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = zipFileName
                };
                string contentType = MimeMapping.GetMimeMapping(Path.GetExtension(zipFilePath));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
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
        [Route("api/CleanDoc/{fileName}")]
        public HttpResponseMessage CleanDoc(string fileName)
        {
            try
            {
                if (fileName == "" || fileName == " ")
                    throw new HttpException("File Name is Required Parameter");

                HttpResponseMessage result;
                string filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");

                if (!File.Exists(filePath))
                    throw new HttpException("File does not exist in temporary storage");

                string modifiedFilePath = HttpContext.Current.Server.MapPath("~/TempDocs/Modified/" + fileName);

                if (!_docManagement.CleanDocument(filePath, modifiedFilePath))
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

        [HttpGet]
        [Route("api/HypDoc/{fileName}")]
        public HttpResponseMessage HypernateDoc(string fileName)
        {
            try
            {
                if(fileName == "" || fileName == " ")
                    throw new HttpException("File Name is Required Parameter");

                HttpResponseMessage result;
                string filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");

                if (!File.Exists(filePath))
                    throw new HttpException("File does not exist in temporary storage");

                string modifiedFilePath = HttpContext.Current.Server.MapPath("~/TempDocs/Modified/" + fileName);

                if (!_docManagement.HypernateDocument(filePath, modifiedFilePath))
                {
                    result = Request.CreateResponse(HttpStatusCode.InternalServerError);
                    result.Content = new StringContent("API Failed To Hypernate Given File");
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

        [HttpGet]
        [Route("api/SaveWork/{fileName}")]
        public HttpResponseMessage SaveWork(string fileName, bool pdf = false)
        {
            try
            {
                if (fileName == "" || fileName == " ")
                    throw new HttpException("File name is required parameter");

                string FileName = fileName;
                HttpResponseMessage result;
                string modifiedFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/{FileName}");

                if (!File.Exists(modifiedFilePath))
                    throw new HttpException("File does not exist in temporary storage");

                string OutputFilePath;
                if (pdf)
                {
                    FileName = $"{ Path.GetFileNameWithoutExtension(fileName)}.pdf";
                    OutputFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/{FileName}");
                    if (!_docManagement.ConvertToPDF(modifiedFilePath, OutputFilePath, WdSaveFormat.wdFormatPDF))
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
                string contentType = MimeMapping.GetMimeMapping(Path.GetExtension(OutputFilePath));
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
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
