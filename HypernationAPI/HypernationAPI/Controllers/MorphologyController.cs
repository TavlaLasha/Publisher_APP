using BLL.Contracts;
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
    public class MorphologyController : ApiController
    {
        readonly IMorphologyManagement _morphManagement;
        public MorphologyController(IMorphologyManagement morphManagement)
        {
            _morphManagement = morphManagement;
        }
        [Route("api/Morphology/GetAllMorphologies")]
        [HttpGet]
        public HttpResponseMessage GetAllMorphologys()
        {
            try
            {
                IEnumerable<MorphologyDTO> dt = _morphManagement.GetAllMorphologies();

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

        [Route("api/Morphology/GetMorphology/{id}")]
        [HttpGet]
        public HttpResponseMessage GetMorphology(string id)
        {
            try
            {
                MorphologyDTO dt = _morphManagement.GetMorphology(id);

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

        [Route("api/Morphology/AddMorphology")]
        [HttpPost]
        public HttpResponseMessage AddMorphology([FromBody] MorphologyDTO bdt)
        {
            try
            {
                if (bdt == null || bdt.Wrong_Word == string.Empty)
                    throw new HttpException("Model invalid or null");

                bool dt = _morphManagement.AddMorphology(bdt);

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

        [Route("api/Morphology/EditMorphology/{id}")]
        [HttpPut]
        public HttpResponseMessage EditMorphology(string id, [FromBody] MorphologyDTO bdt)
        {
            try
            {
                if (id == string.Empty)
                    throw new HttpException("ID is important parameter");

                if (bdt == null || bdt.Wrong_Word == string.Empty)
                    throw new HttpException("Model invalid or null");

                bool dt = _morphManagement.EditMorphology(id, bdt);

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

        [Route("api/Morphology/DeleteMorphology/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteMorphology(string id)
        {
            try
            {
                if (id == string.Empty)
                    throw new HttpException("ID is important parameter");

                bool dt = _morphManagement.DeleteMorphology(id);

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
