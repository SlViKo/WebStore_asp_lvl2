﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Interfaces.Api;

using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class WebAPITestTests
    {

        [TestMethod]
        public void Index_Returns_View_with_Values()
        {
            var expected_result = new[] { "1", "2", "3" };

            var value_service_mock = new Mock<IValueServices>();
            value_service_mock
                .Setup(service => service.Get())
                .Returns(expected_result);

            var controller = new WebAPITestController(value_service_mock.Object);

            var result = controller.Index();

            var view_result = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(view_result.Model);

            Assert.Equal(expected_result.Length, model.Count());

            value_service_mock.Verify(service => service.Get());
            value_service_mock.VerifyNoOtherCalls();
        }
    }
}