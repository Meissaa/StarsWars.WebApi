using AutoMapper;
using log4net;
using StarsWars.Business.Managers;
using StarsWars.Common.Entities;
using StarsWars.Common.Exceptions;
using StarsWars.Common.Managers;
using StarsWars.Services.Models;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StarsWars.Services.Controllers
{
    [RoutePrefix(WebApiConfig.API_PREFIX + "/starswars")]
    public class StarsWarsDetailsController : ApiController
    {
        private static ILog _log;
        private IStarsWarsManager _starsWarsManager;

        public StarsWarsDetailsController(IStarsWarsManager starsWarsManager)
        {
            _log = LogManager.GetLogger(this.GetType().FullName);
            _starsWarsManager = starsWarsManager;
        }

        [Route("{characterId}/episode")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(EpisodeResponse))]
        [SwaggerResponse(422, "UnprocessableEntity", Type = typeof(EpisodeResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(EpisodeResponse))]
        public IHttpActionResult AddEpisode(int characterId, EpisodeRequest request)
        {
            try
            {
                _log.Info("begin AddEpisode");

                if (request == null)
                    throw new ArgumentNullException("The request content was null or not in the correct format");

                _starsWarsManager.AddEpisode(characterId, Mapper.Map<Episode>(request));

                return Ok();
            }
            catch (ArgumentNullException argEx)
            {
                _log.Error(argEx);

                return BadRequest(argEx.Message);
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);

                return Content<EpisodeResponse>(HttpStatusCode.NotFound, new EpisodeResponse { Message = enEx.Message });
            }
            catch (StarsWarsException swEx)
            {
                _log.Error(swEx);

                return Content<EpisodeResponse>((HttpStatusCode)422, new EpisodeResponse { Message = swEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<EpisodeResponse>(HttpStatusCode.InternalServerError, new EpisodeResponse { Message = ex.Message });
            }
        }

        [Route("{characterId}/episode")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(EpisodeResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(EpisodeResponse))]
        [SwaggerResponse(422, "UnprocessableEntity", Type = typeof(EpisodeResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(EpisodeResponse))]
        public IHttpActionResult UpdateEpisode(EpisodeRequest request)
        {
            try
            {
                _log.Info("begin UpdateEpisode");

                if (request == null)
                    throw new ArgumentNullException("The request content was null or not in the correct format");

                _starsWarsManager.UpdateEpisode(Mapper.Map<Episode>(request));

                return Ok(new EpisodeResponse() { Data = request });
            }
            catch (ArgumentNullException argEx)
            {
                _log.Error(argEx);

                return BadRequest(argEx.Message);
            }
            catch (StarsWarsException swEx)
            {
                _log.Error(swEx);

                return Content<EpisodeResponse>((HttpStatusCode)422, new EpisodeResponse { Message = swEx.Message });
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);

                return Content<EpisodeResponse>(HttpStatusCode.NotFound, new EpisodeResponse { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<EpisodeResponse>(HttpStatusCode.InternalServerError, new EpisodeResponse { Message = ex.Message });
            }
        }

        [Route("{characterId}/episode/{episodeId}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(EpisodeResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(EpisodeResponse))]
        public IHttpActionResult RemoveEpisode(int characterId, int episodeId)
        {
            try
            {
                _log.Info("begin RemoveEpisode");

                _starsWarsManager.RemoveEpisode(characterId,episodeId);

                return Ok();
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);

                return Content<EpisodeResponse>(HttpStatusCode.NotFound, new EpisodeResponse { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<EpisodeResponse>(HttpStatusCode.InternalServerError, new EpisodeResponse { Message = ex.Message });
            }
        }

        [Route("{characterId}/friend")]
        [HttpPost]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(FriendResponse))]
        [SwaggerResponse(422, "UnprocessableEntity", Type = typeof(FriendResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(FriendResponse))]
        public IHttpActionResult AddFriend(int characterId, FriendRequest request)
        {
            try
            {
                _log.Info("begin AddFriend");

                if (request == null)
                    throw new ArgumentNullException("The request content was null or not in the correct format");

                _starsWarsManager.AddFriend(characterId, Mapper.Map<Friend>(request));

                return Ok();
            }
            catch (ArgumentNullException argEx)
            {
                _log.Error(argEx);

                return BadRequest(argEx.Message);
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);

                return Content<FriendResponse>(HttpStatusCode.NotFound, new FriendResponse { Message = enEx.Message });
            }
            catch (StarsWarsException swEx)
            {
                _log.Error(swEx);

                return Content<FriendResponse>((HttpStatusCode)422, new FriendResponse { Message = swEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);

                return Content<FriendResponse>(HttpStatusCode.InternalServerError, new FriendResponse { Message = ex.Message });
            }
        }

        [Route("{characterId}/friend")]
        [HttpPut]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FriendResponse))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(FriendResponse))]
        [SwaggerResponse(422, "UnprocessableEntity", Type = typeof(FriendResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(FriendResponse))]
        public IHttpActionResult UpdateFriend(FriendRequest request)
        {
            try
            {
                _log.Info("begin UpdateFriend");

                if (request == null)
                    throw new ArgumentNullException("The request content was null or not in the correct format");

                _starsWarsManager.UpdateFriend(Mapper.Map<Friend>(request));

                return Ok(new FriendResponse() { Data = request });
            }
            catch (ArgumentNullException argEx)
            {
                _log.Error(argEx);

                return BadRequest(argEx.Message);
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);
                return Content<FriendResponse>(HttpStatusCode.NotFound, new FriendResponse { Message = enEx.Message });
            }
            catch (StarsWarsException swEx)
            {
                _log.Error(swEx);
                return Content<FriendResponse>((HttpStatusCode)422, new FriendResponse { Message = swEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<FriendResponse>(HttpStatusCode.InternalServerError, new FriendResponse { Message = ex.Message });
            }
        }

        [Route("{characterId}/friend/{friendId}")]
        [HttpDelete]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(FriendResponse))]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(FriendResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, Type = typeof(FriendResponse))]
        public IHttpActionResult RemoveFriend(int characterId, int friendId)
        {
            try
            {
                _log.Info("begin RemoveFriend");

                _starsWarsManager.RemoveFriend(characterId, friendId);

                return Ok();
            }
            catch (EntityNotFoundException enEx)
            {
                _log.Error(enEx);

                return Content<FriendResponse>(HttpStatusCode.NotFound, new FriendResponse { Message = enEx.Message });
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return Content<FriendResponse>(HttpStatusCode.InternalServerError, new FriendResponse { Message = ex.Message });
            }
        }
    }
}
