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
    public class DocumentController : ApiController
    {
        readonly IDocManagement _docManagement;
        public DocumentController(IDocManagement docManagement)
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
                        var workingDir = Directory.CreateDirectory(HttpContext.Current.Server.MapPath($"~/TempDocs/{Path.GetFileNameWithoutExtension(FileName)}")).FullName;
                        Directory.CreateDirectory(Path.Combine(workingDir, "Modified"));
                        var filePath = Path.Combine(workingDir, FileName);
                        if (File.Exists(filePath))
                        {
                            FileName = $"{Path.GetFileNameWithoutExtension(FileName)}{DateTime.Now.Ticks}{contentType}";
                            filePath = Path.Combine(workingDir, FileName);
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("api/GetDocPages/{fileName}")]
        public HttpResponseMessage GetDocPages(string fileName, int page = 1, bool clean=true)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new HttpException("File Name is Required Parameter");

                HttpResponseMessage result;
                string Folder = Path.GetFileNameWithoutExtension(fileName);

                var filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{Folder}/Modified/{fileName}");
                if (!File.Exists(filePath))
                {
                    filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{Folder}/{fileName}");
                    if (!File.Exists(filePath))
                        throw new HttpException("File does not exist in temporary storage");
                }
                var data = _docManagement.GetPages(filePath, page, clean);

                string zipFileName = $"{Path.GetFileNameWithoutExtension(fileName)}.zip";
                string zipFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{Path.GetFileNameWithoutExtension(fileName)}/{zipFileName}");

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
        [Route("api/SaveWork/{fileName}")]
        public HttpResponseMessage SaveWork(string fileName, bool pdf = false)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new HttpException("File name is required parameter");

                string FileName = fileName;
                string FileFolder = Path.GetFileNameWithoutExtension(fileName);
                HttpResponseMessage result;

                var filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/Modified/{fileName}");
                if (!File.Exists(filePath))
                {
                    filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/{fileName}");
                    if (!File.Exists(filePath))
                        throw new HttpException("File does not exist in temporary storage");
                }

                string OutputFilePath;
                if (pdf)
                {
                    FileName = $"{FileFolder}.pdf";
                    OutputFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{FileFolder}/Modified/{FileName}");
                    if (!_docManagement.ConvertToPDF(filePath, OutputFilePath, WdSaveFormat.wdFormatPDF))
                    {
                        throw new HttpException("Could not convert to PDF");
                    }
                }
                else
                {
                    OutputFilePath = filePath;
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

        [HttpDelete]
        [Route("api/DeleteDoc/{fileName}")]
        public HttpResponseMessage DeleteDoc(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    throw new HttpException("File name is required parameter");

                string FileName = fileName;

                //string mFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/{fileName}");
                //string filePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{fileName}");
                //string zipFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/{Path.GetFileNameWithoutExtension(fileName)}.zip");
                //string pdfFilePath = HttpContext.Current.Server.MapPath($"~/TempDocs/Modified/{Path.GetFileNameWithoutExtension(fileName)}.pdf");

                string tempDirectory = HttpContext.Current.Server.MapPath($"~/TempDocs/{Path.GetFileNameWithoutExtension(fileName)}");
                if (Directory.Exists(tempDirectory))
                    Directory.Delete(tempDirectory, true);

                //if (File.Exists(filePath))
                //    File.Delete(filePath);

                //if (File.Exists(zipFilePath))
                //    File.Delete(zipFilePath);

                //if (File.Exists(mFilePath))
                //    File.Delete(mFilePath);

                //if (File.Exists(mFilePath))
                //    File.Delete(mFilePath);

                return new HttpResponseMessage(HttpStatusCode.OK);
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
    }
}
