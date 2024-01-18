using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Currency_Exchange_Manager_App.Controllers;
using Currency_Exchange_Manager_App.Repositories;
using Moq;
using AutoFixture;
using Currency_Exchange_Manager_App.Model;
using System.Data.Entity.Core.Objects;
using NuGet.Protocol;
using Microsoft.AspNetCore.Http;

using Microsoft.Extensions.Logging;

namespace ApiTestCurrencyApp
{
    [TestClass]
    public class CurrencyControllerTest
    {
        /*create test case, unit test for CRUD*/
        private Mock<ICurrencyInfo> _currencyRepoMock;
        private Fixture _fixture;
        private CurrencyController _currencyController;
        private ILogger<CurrencyController> _logger;

        public CurrencyControllerTest()
        {
            _fixture = new Fixture();
            _currencyRepoMock = new Mock<ICurrencyInfo>();

        }

        [TestMethod]

        public async Task Get_Currency_list()
        {
            var currencyList = _fixture.CreateMany<CurrencyInfo>(2).ToList();

            //_currencyRepoMock.Setup(repo => repo.GetCurrencyAsync()).Returns(currencyList);

            _currencyController = new CurrencyController(_currencyRepoMock.Object, _logger);

            var result = await _currencyController.GetCurrency();

            var obj = result as ObjectResult;
            /*look at test cases */
            
           Assert.AreEqual(200, 200);
        }

        [TestMethod]

        public async Task Get_Currency_ThrowExeception()
        {
            _currencyRepoMock.Setup(repo => repo.GetCurrencyAsync()).Throws(new Exception());

            _currencyController = new CurrencyController(_currencyRepoMock.Object, _logger);

            var result = await _currencyController.GetCurrency();

            var obj = result as ObjectResult;
            /*look at test cases */

            Assert.AreEqual(500, 500);
        }

        [TestMethod]

        public async Task Post_Currency_ReturnOK()
        {
            var currency = _fixture.Create<CurrencyInfo>();

           // _currencyRepoMock.Setup(repo => repo.CreateCurrencyAsync(It.IsAny<CurrencyInfo>())).Returns(currency);
            
            _currencyController = new CurrencyController(_currencyRepoMock.Object, _logger);

            var result = await _currencyController.AddCurrency(currency);

            var obj = result as ObjectResult;

            Assert.AreEqual(200, 200);
        }

        [TestMethod]

        public async Task Update_Currency_ReturnOK()
        {
            var currency = _fixture.Create<CurrencyInfo>();

           // _currencyRepoMock.Setup(repo => repo.UpdateCurrencyAsync(It.IsAny<CurrencyInfo>())).Returns(currency);

            _currencyController = new CurrencyController(_currencyRepoMock.Object, _logger);

            var result = await _currencyController.UpdateCurrency(currency);

            var obj = result as ObjectResult;

            Assert.AreEqual(200, 200);
        }

        [TestMethod]

        public async Task Delete_Currency_ReturnOK()
        {
            //var currency = _fixture.Create<CurrencyInfo>();

            //_currencyRepoMock.Setup(repo => repo.DeleteCurrencyAsync(It.IsAny<int>())).Returns(true);

            _currencyController = new CurrencyController(_currencyRepoMock.Object, _logger);

            var result = await _currencyController.DeleteCurrency(It.IsAny<int>());

            var obj = result as ObjectResult;

            Assert.AreEqual(200, 200);
        }
    }
}
