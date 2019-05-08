using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using log4net;
using StarsWars.Business.Managers;
using StarsWars.Common.Entities;
using StarsWars.Common.Exceptions;
using StarsWars.Common.Managers;
using StarsWars.Services.Models;
using Swashbuckle.Swagger.Annotations;

namespace StarsWars.Services.Controllers
{
    [RoutePrefix(WebApiConfig.API_PREFIX + "/starswars")]
    public class StarsWarsController : ApiController
    {
        private static ILog _log;
        private IStarsWarsManager _starsWarsManager;
        private int _pageSize;

        public StarsWarsController(IStarsWarsManager starsWarsManager)
        {
            _log = LogManager.GetLogger(this.GetType().FullName);
            _starsWarsManager = starsWarsManager;
            _pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }

        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(CharacterResponse))]
        public IHttpActionResult GetAllCharacters(int page = 1)
        { 
            try
            {
                _log.Info("begin GetAllCharacters");

                var characters = _starsWarsManager.GetCharacters()
                                 .OrderBy(p=>p.Id)
                                 .Skip((page-1)*_pageSize)
                                 .Take(_pageSize);

                var pageInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = _starsWarsManager.GetCharactersCount()
                };

                return Ok(new CharacterResponse() { Data = characters, PagingInfo = pageInfo });
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);

                return Content<CharacterResponse>(HttpStatusCode.NotFound, new CharacterResponse { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return Content<CharacterResponse>(HttpStatusCode.InternalServerError, new CharacterResponse() { Message = ex.Message });
            }
        }

        [Route("{id}")]
        [HttpGet]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(CharacterResponse))]
        public IHttpActionResult GetCharacter(int id)
        {
            try
            {
                _log.Info("begin GetCharacter");

                var character = _starsWarsManager.GetCharacter(id);

                return Ok(new CharacterResponse() { Data = character });
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);
                return Content<CharacterResponse>(HttpStatusCode.NotFound, new CharacterResponse { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<CharacterResponse>(HttpStatusCode.InternalServerError, new CharacterResponse() { Message = ex.Message });
            }
        }

        [HttpPost]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(CharacterResponse))]
        [SwaggerResponse(422, "UnprocessableEntity", Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(CharacterResponse))]
        public IHttpActionResult CreateCharacter(CharacterRequest request)
        {
            try
            {
                _log.Info("begin CreateCharacter");

                if (request == null)
                    throw new ArgumentNullException("The request content was null or not in the correct format");

                _starsWarsManager.CreateCharacter(Mapper.Map<Character>(request));

                return Content<CharacterResponse>(HttpStatusCode.Created, new CharacterResponse() { Data = request });
            }
            catch (ArgumentNullException argEx)
            {
                _log.Error(argEx);
                return BadRequest(argEx.Message);
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);
                return Content<CharacterResponse>(HttpStatusCode.NotFound, new CharacterResponse() { Message = enEx.Message });
            }
            catch (StarsWarsException swEx)
            {
                _log.Error(swEx);
                return Content<CharacterResponse>((HttpStatusCode)422, new CharacterResponse() { Message = swEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<CharacterResponse>(HttpStatusCode.InternalServerError, new CharacterResponse() { Message = ex.Message });
            }
        }

        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(CharacterResponse))]
        public IHttpActionResult UpdateCharacter(CharacterRequest request)
        {
            try
            {
                _log.Info("begin UpdateCharacter");

                if (request == null)
                    throw new ArgumentNullException("The request content was null or not in the correct format");

                _starsWarsManager.UpdateCharacter(Mapper.Map<Character>(request));

                return Ok(new CharacterResponse() { Data = request });
            }
            catch (ArgumentNullException argEx)
            {
                _log.Error(argEx);
                return BadRequest(argEx.Message);
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);
                return Content<CharacterResponse>(HttpStatusCode.NotFound, new CharacterResponse() { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<CharacterResponse>(HttpStatusCode.InternalServerError, new CharacterResponse() { Message = ex.Message });
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(CharacterResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(CharacterResponse))]
        public IHttpActionResult RemoveCharacter(int id)
        {
            try
            {
                _log.Info("begin RemoveCharacter");

                _starsWarsManager.RemoveCharacter(id);

                return Ok();
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);
                return Content<CharacterResponse>(HttpStatusCode.NotFound, new CharacterResponse() { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<CharacterResponse>(HttpStatusCode.InternalServerError, new CharacterResponse() { Message = ex.Message });
            }
        }
    }
}
