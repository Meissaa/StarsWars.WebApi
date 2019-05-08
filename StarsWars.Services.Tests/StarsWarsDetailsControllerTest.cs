using System;
using System.Web.Http.Results;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarsWars.Common.Managers;
using StarsWars.Services.Controllers;
using StarsWars.Services.Models;
using Unity;

namespace StarsWars.Services.Tests
{
    [TestClass]
    public class StarsWarsDetailsControllerTest
    {
        private IUnityContainer _container;
        private IStarsWarsManager _starsWarsManager;

        [TestInitialize]
        public void Initialize()
        {
            StarsWars.Services.Tests.Config.AutoMapperConfig.Initialize();
            _container = new UnityContainer();
            _container.LoadConfiguration();
            _starsWarsManager = _container.Resolve<IStarsWarsManager>();

        }

        #region TestEpisode

        [TestMethod]
        public void AddEpisode__WithNullRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int id = 3;
            EpisodeRequest request = null;
            #endregion

            #region Act
            var response = controller.AddEpisode(id, request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));
            #endregion
        }

        [TestMethod]
        public void AddEpisode__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int characterId = 7;
            EpisodeRequest request = new EpisodeRequest()
            {
                Name = "NEWHOPE"
            };
            #endregion

            #region Act
            var response = controller.AddEpisode(characterId, request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
            #endregion
        }

        [TestMethod]
        public void AddEpisode__DuplicatedRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int characterId = 7;
            EpisodeRequest request = new EpisodeRequest()
            {
                Name = "NEWHOPE"
            };
            #endregion

            #region Act
            var response = controller.AddEpisode(characterId, request) as NegotiatedContentResult<EpisodeResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Episode already exists", response.Content.Message);
            Assert.AreEqual(422, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void UpdateEpisode__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            EpisodeRequest request = new EpisodeRequest()
            {
                Id = 11,
                Name = "EMPIRE"
            };
            #endregion

            #region Act
            var response = controller.UpdateEpisode(request) as OkNegotiatedContentResult<EpisodeResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            #endregion
        }

        [TestMethod]
        public void UpdateEpisode__WithNullRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            EpisodeRequest request = null;
            #endregion

            #region Act
            var response = controller.UpdateEpisode(request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));
            #endregion
        }

        [TestMethod]
        public void UpdateEpisode__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            EpisodeRequest request = new EpisodeRequest()
            {
                Id = 1,
                Name = "EMPIRE"
            };
            #endregion

            #region Act
            var response = controller.UpdateEpisode(request) as NegotiatedContentResult<EpisodeResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual($"Episode {request.Name} not found", response.Content.Message);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void RemoveEpisode__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int episodeId = 6;
            int characterId = 4;
            #endregion

            #region Act
            var response = controller.RemoveEpisode(characterId, episodeId);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
            #endregion
        }

        [TestMethod]
        public void RemoveEpisode__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int episodeId = 6;
            int characterId = 4;
            #endregion

            #region Act
            var response = controller.RemoveEpisode(characterId, episodeId) as NegotiatedContentResult<EpisodeResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }

        #endregion

        #region TestFriend

        [TestMethod]
        public void AddFriend__WithNullRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int id = 3;
            FriendRequest request = null;
            #endregion

            #region Act
            var response = controller.AddFriend(id, request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));
            #endregion
        }

        [TestMethod]
        public void AddFriend__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int characterId = 4;
            FriendRequest request = new FriendRequest()
            {
                Name = "Leia Organa"
            };
            #endregion

            #region Act
            var response = controller.AddFriend(characterId, request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
            #endregion
        }

        [TestMethod]
        public void AddFriend__DuplicatedRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int characterId = 4;
            FriendRequest request = new FriendRequest()
            {
                Name = "Leia Organa"
            };
            #endregion

            #region Act
            var response = controller.AddFriend(characterId, request) as NegotiatedContentResult<FriendResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.AreEqual("Friend already exists", response.Content.Message);
            Assert.AreEqual(422, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void UpdateFriend__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            FriendRequest request = new FriendRequest()
            {
                Id = 5,
                Name = "Luke Skywalker"
            };
            #endregion

            #region Act
            var response = controller.UpdateFriend(request) as OkNegotiatedContentResult<FriendResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            #endregion
        }

        [TestMethod]
        public void UpdateFriend__WithNullRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            FriendRequest request = null;
            #endregion

            #region Act
            var response = controller.UpdateFriend(request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));
            #endregion
        }

        [TestMethod]
        public void UpdateFriend__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            FriendRequest request = new FriendRequest()
            {
                Id = 1,
                Name = "Luke Skywalker"
            };
            #endregion

            #region Act
            var response = controller.UpdateFriend(request) as NegotiatedContentResult<FriendResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual($"Friend {request.Name} not found", response.Content.Message);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void RemoveFriend__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int friendId = 3;
            int characterId = 3;
            #endregion

            #region Act
            var response = controller.RemoveFriend(characterId, friendId);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
            #endregion
        }

        [TestMethod]
        public void RemoveFriend__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsDetailsController(_starsWarsManager);
            int friendId = 3;
            int characterId = 3;
            #endregion

            #region Act
            var response = controller.RemoveFriend(characterId, friendId) as NegotiatedContentResult<FriendResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }
        #endregion
    }
}

