using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spire.Doc;
//using Microsoft.Office.Interop.Word;
using System.Xml.Linq;

namespace HypernationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HYPController : ControllerBase
    {
        [HttpPost(Name = "HypernateDoc")]
        public string HypernateDoc(string docXml)
        {
            Document doc = new Document();
            Stream st = new MemoryStream();

            //byte[] existingData = System .GetBytes("foo");
            //MemoryStream ms = new MemoryStream();
            //ms.Write(existingData, 0, existingData.Length);
            //WriteUnknownData(ms);

            doc.LoadFromStream(docXml.ToStream(), FileFormat.Xml);
            doc.SaveToFile("", FileFormat.Docx);

            return docXml;
        }
    }
}
