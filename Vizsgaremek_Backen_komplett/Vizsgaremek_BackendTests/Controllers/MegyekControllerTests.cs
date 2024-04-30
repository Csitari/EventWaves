using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vizsgaremek_Backend.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Vizsgaremek_Backend.Models;

namespace Vizsgaremek_Backend.Controllers.Tests
{
    [TestClass()]
    public class MegyeControllerTests
    {
        [TestMethod()]
        public void GetAllTest()
        {
            // Arrange
            var controller = new MegyeController(); // Ha szükséges, itt lehetne mockolni a dependency-ket

            // Act
            var result = controller.GetAll() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result); // Ellenőrizzük, hogy nem null a válasz
            Assert.IsInstanceOfType(result.Value, typeof(List<Megyek>)); // Ellenőrizzük, hogy a válasz típusa List<Megyek>

            // Egyéb ellenőrzések, ha szükséges
            var megyek = result.Value as List<Megyek>;
            Assert.AreEqual(5, megyek.Count); // Például, ha tudjuk, hogy 5 megye van az adatbázisban


        }
    }
}
