using System;
using System.Web.Http.Results;
using log4net;
using Microsoft.Practices.Unity.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarsWars.Common.Managers;
using StarsWars.Services.Controllers;
using StarsWars.Services.Models;
using Unity;

namespace StarsWars.Services.Tests
{
    [TestClass]
    public class StarsWarsControllerTest
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

        [TestMethod]
        public void GetAllCharacters__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            #endregion

            #region Act
            var response = controller.GetAllCharacters() as OkNegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content.Data);
            Assert.IsNotNull(response.Content.PagingInfo);
            #endregion
        }

        [TestMethod]
        public void GetAllCharacters__CanPaginate()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            #endregion

            #region Act
            var response = controller.GetAllCharacters() as OkNegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content.Data);
            Assert.IsNotNull(response.Content.PagingInfo);
            Assert.IsTrue(response.Content.PagingInfo.ItemsPerPage == 4);
            Assert.IsTrue(response.Content.PagingInfo.TotalPages == 2);
            Assert.IsTrue(response.Content.PagingInfo.CurrentPage == 1);
            Assert.IsTrue(response.Content.PagingInfo.TotalItems == 7);
            #endregion
        }

        [TestMethod]
        public void GetCharacter__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            int id = 3;
            #endregion

            #region Act
            var response = controller.GetCharacter(id) as OkNegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content.Data);
            #endregion
        }

        [TestMethod]
        public void GetCharacter__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            int id = 1;
            #endregion

            #region Act
            var response = controller.GetCharacter(id) as NegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual("Character not found", response.Content.Message);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void CreateCharacter__WithNullRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            CharacterRequest request = null;
            #endregion

            #region Act
            var response = controller.CreateCharacter(request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));
            #endregion
        }

        [TestMethod]
        public void CreateCharacter__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            CharacterRequest request = new CharacterRequest()
            {
                Name = "Chewbacca"
            };
            #endregion

            #region Act
            var response = controller.CreateCharacter(request) as NegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(201, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void CreateCharacter__DuplicatedRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            CharacterRequest request = new CharacterRequest()
            {
                Name = "Chewbacca"
            };
            #endregion

            #region Act
            var response = controller.CreateCharacter(request) as NegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.AreEqual($"Character {request.Name} already exists", response.Content.Message);
            Assert.AreEqual(422, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void UpdateCharacter__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            CharacterRequest request = new CharacterRequest()
            {
                Id = 13,
                Name = "Beru Lars"
            };
            #endregion

            #region Act
            var response = controller.UpdateCharacter(request) as OkNegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            #endregion
        }

        [TestMethod]
        public void UpdateCharacter__WithNullRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            CharacterRequest request = null;
            #endregion

            #region Act
            var response = controller.UpdateCharacter(request);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestErrorMessageResult));
            #endregion
        }

        [TestMethod]
        public void UpdateCharacter__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            CharacterRequest request = new CharacterRequest()
            {
                Id = 1,
                Name = "Beru Lars"
            };
            #endregion

            #region Act
            var response = controller.UpdateCharacter(request) as NegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual($"Character {request.Name} not found", response.Content.Message);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }

        [TestMethod]
        public void RemoveCharacter__WithGoodRequest()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            int id = 13;
            #endregion

            #region Act
            var response = controller.RemoveCharacter(id);
            #endregion

            #region Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
            #endregion
        }

        [TestMethod]
        public void RemoveCharacter__WithNotFound()
        {
            #region Arrange
            var controller = new StarsWarsController(_starsWarsManager);
            int id = 1;
            #endregion

            #region Act
            var response = controller.RemoveCharacter(id) as NegotiatedContentResult<CharacterResponse>;
            #endregion

            #region Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(404, (int)response.StatusCode);
            #endregion
        }
    }
}
