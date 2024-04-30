using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Vizsgaremek_Backend.Controllers;
using Vizsgaremek_Backend.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Vizsgaremek_Backend.Controllers.Tests
{
    [TestClass()]
    public class MegyekControllerTests
    {
        [TestMethod()]
        public void GetById_ExistingId_ReturnsItem()
        {
            // Arrange
            var testId = 1;
            var testItem = new Megyek { MegyeId = testId, Megyenev = "Test Megye" };

            var mockContext = new Mock<EsemenytarContext>();
            mockContext.Setup(c => c.Megyeks.Find(testId)).Returns(testItem);

            var controller = new MegyekController(mockContext.Object);

            // Act
            var result = controller.GetById(testId);

            // Assert
            Assert.IsNotNull(result); // Ellenőrizzük, hogy a válasz nem null
            Assert.IsInstanceOfType(result.Value, typeof(Megyek)); // Ellenőrizzük, hogy a válasz egy Megyek objektum
            Assert.AreEqual(testItem, result.Value); // Ellenőrizzük, hogy a visszaadott elem azonos a tesztesetben megadott elemmel
        }

        [TestMethod()]
        public void GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var testId = 1;

            var mockContext = new Mock<EsemenytarContext>();
            mockContext.Setup(c => c.Megyeks.Find(testId)).Returns((Megyek)null);

            var controller = new MegyekController(mockContext.Object);

            // Act
            var result = controller.GetById(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult)); // Ellenőrizzük, hogy a válasz egy NotFoundResult objektum
        }
    }
}
