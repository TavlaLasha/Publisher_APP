using BLL.Contracts;
using Models.DataViewModels.DocManagement;
using Models.DataViewModels.WordManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace HypernationAPI.Controllers
{
    public class BarbarismController : ApiController
    {
        readonly IBarbarismManagement _barbManagement;
        public BarbarismController(IBarbarismManagement barbManagement)
        {
            _barbManagement = barbManagement;
        }

        [Route("api/Barbarism/FindBarbarisms")]
        [HttpPost]
        public HttpResponseMessage FindBarbarisms(TextDTO textDTO)
        {
            try
            {
                FoundOccurrencesDTO fbdt = new FoundOccurrencesDTO();
                fbdt = _barbManagement.FindBarbarisms(textDTO.Text);

                string jObject = JsonConvert.SerializeObject(fbdt);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(jObject, Encoding.UTF8, "application/json")
                };
                return result;

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Barbarism/GetAllBarbarisms")]
        [HttpGet]
        public HttpResponseMessage GetAllBarbarisms()
        {
            try
            {
                IEnumerable<BarbarismDTO> dt = _barbManagement.GetAllBarbarisms();

                string jObject = JsonConvert.SerializeObject(dt);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(jObject, Encoding.UTF8, "application/json")
                };
                return result;

            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Barbarism/GetBarbarism/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBarbarism(int id)
        {
            try
            {
                BarbarismDTO dt = _barbManagement.GetBarbarism(id);

                string jObject = JsonConvert.SerializeObject(dt);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
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
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Barbarism/GetBarbarismWName/{wrongWord}")]
        [HttpGet]
        public HttpResponseMessage GetBarbarismWName(string wrongWord)
        {
            try
            {
                BarbarismDTO dt = _barbManagement.GetBarbarism(wrongWord);

                string jObject = JsonConvert.SerializeObject(dt);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
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
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Barbarism/AddBarbarism")]
        [HttpPost]
        public HttpResponseMessage AddBarbarism([FromBody] BarbarismDTO bdt)
        {
            try
            {
                if(bdt == null || bdt.Wrong_Word == string.Empty)
                    throw new HttpException("Model invalid or null");

                bool dt = _barbManagement.AddBarbarism(bdt);

                if (!dt)
                    throw new Exception("There was an error when adding to database");

                string jObject = JsonConvert.SerializeObject(bdt);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
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
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Barbarism/EditBarbarism/{id}")]
        [HttpPut]
        public HttpResponseMessage EditBarbarism(string id, [FromBody] BarbarismDTO bdt)
        {
            try
            {
                if(id == string.Empty)
                    throw new HttpException("ID is important parameter");

                if (bdt == null || (bdt.Wrong_Word == string.Empty && bdt.Correct_Word == string.Empty && bdt.Description == string.Empty))
                    throw new HttpException("Model invalid or null");

                bool dt = _barbManagement.EditBarbarism(id, bdt);

                if (!dt)
                    throw new Exception("There was an error when updating record");

                string jObject = JsonConvert.SerializeObject(bdt);

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
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
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Route("api/Barbarism/DeleteBarbarism/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteBarbarism(string id)
        {
            try
            {
                if (id == string.Empty)
                    throw new HttpException("ID is important parameter");

                bool dt = _barbManagement.DeleteBarbarism(id);

                if (!dt)
                    throw new Exception("There was an error when updating record");

                string jObject = JsonConvert.SerializeObject(new Dictionary<string, bool>() { { "Deleted", true } });

                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
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
            catch
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
