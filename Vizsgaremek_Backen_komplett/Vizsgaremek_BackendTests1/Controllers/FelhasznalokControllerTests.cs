using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Vizsgaremek_Backend.Controllers;
using Vizsgaremek_Backend.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Vizsgaremek_Backend.Controllers.Tests
{
    [TestClass()]
    public class FelhasznaloControllerTests
    {
        [TestMethod()]
        public void GetFelhasznaloPlus_ExistingId_ReturnsItem()
        {
            // Arrange
            var testId = Guid.NewGuid(); // Random GUID generálása
            var testFelhasznalo = new Felhasznalok
            {
                FelhasznaloId = testId,
                Felhasznalonev = "TestUser",
                Email = "test@example.com",
                /* További tulajdonságok beállítása */
            };

            var mockContext = new Mock<EsemenytarContext>();
            mockContext.Setup(c => c.Felhasznaloks).Returns((DbSet<Felhasznalok>)GetQueryableMockDbSet(Enumerable.Empty<Felhasznalok>())); // Mockoljuk a Felhasznaloks DbSet-et

            var controller = new FelhasznalokController(mockContext.Object);

            // Act
            var result = controller.GetFelhasznaloPlus(testId);

            // Assert
            Assert.IsNotNull(result); // Ellenőrizzük, hogy a válasz nem null
            Assert.IsInstanceOfType(result.Value, typeof(Felhasznalok)); // Ellenőrizzük, hogy a válasz egy Felhasznalok objektum
            Assert.AreEqual(testFelhasznalo, result.Value); // Ellenőrizzük, hogy a visszaadott felhasználó azonos a tesztesetben megadott felhasználóval
        }

        [TestMethod()]
        public void GetFelhasznaloPlus_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var testId = Guid.NewGuid(); // Random GUID generálása

            var mockContext = new Mock<EsemenytarContext>();
            mockContext.Setup(c => c.Felhasznaloks).Returns((DbSet<Felhasznalok>)GetQueryableMockDbSet(Enumerable.Empty<Felhasznalok>())); // Mockoljuk a Felhasznaloks DbSet-et

            var controller = new FelhasznalokController(mockContext.Object);

            // Act
            var result = controller.GetFelhasznaloPlus(testId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult)); // Ellenőrizzük, hogy a válasz egy NotFoundResult objektum
        }

        // Segédfüggvény az IQueryable interfész mockolásához
        private static IQueryable<T> GetQueryableMockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.AsQueryable().Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }
    }
}


