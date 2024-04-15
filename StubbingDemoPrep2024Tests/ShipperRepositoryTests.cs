using Microsoft.EntityFrameworkCore;
using StubbingDemoPrep2024;
using StubbingDemoPrep2024.Database.Models;
using StubbingDemoPrep2024.Repositories;
using StubbingDemoPrep2024Tests.Stubs;

namespace StubbingDemoPrep2024Tests;

[TestClass]
public class ShipperRepositoryTests
{
    private NorthwindContext_Stub _context = default!;
    private ShipperRepository _repository = default!;

    [TestInitialize]
    public void TestInitialize()
    {
        _context = new NorthwindContext_Stub();

        // Clear out the database before every test
        _context.Database.EnsureDeleted();

        // Instantiate repository
        _repository = new ShipperRepository(_context);
    }

    [TestMethod]
    public void ShipperRepository_Constructor_IsNotNull()
    {
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    [ExpectedException(typeof(CouldNotAddToDatabaseException))]
    public async Task ShipperRepository_CreateShipper_ShouldThrowCorrectExceptionOnDatabaseFailure()
    {
        _context.CauseError = true;
        await _repository.CreateShipperAsync(2, "Test", "111-2222");
    }

    [TestMethod]
    public async Task ShipperRepository_GetShipperByIdAsync_ShouldReturnShipper()
    {
        // Add data to the database
        var shipper = new Shipper() { ShipperId = 1, CompanyName = "Shipper", Phone = "111-2222" };
        _context.Shippers.Add(shipper);
        _context.SaveChanges();
        var retrievedShipper = await _repository.GetShipperByIdAsync(1);
        Assert.AreEqual(1, retrievedShipper?.ShipperId);
        Assert.AreEqual("Shipper", retrievedShipper?.CompanyName);
        Assert.AreEqual("111-2222", retrievedShipper?.Phone);
    }

    [TestMethod]
    public async Task ShipperRepository_GetShipperByIdAsync_ShouldReturnNullWhenShipperIdNotFound()
    {
        // Add data to the database
        var shipper = new Shipper() { ShipperId = 1, CompanyName = "Shipper", Phone = "111-2222" };
        _context.Shippers.Add(shipper);
        _context.SaveChanges();
        var retrievedShipper = await _repository.GetShipperByIdAsync(2);
        Assert.IsNull(retrievedShipper);
    }

    [TestMethod]
    public void GetShippers_ShouldReturnShipppers()
    {
        var shipperList = new List<Shipper>
        {
            new Shipper { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222" },
            new Shipper { ShipperId = 2, CompanyName = "Google", Phone = "111-3333" },
            new Shipper { ShipperId = 3, CompanyName = "Apple", Phone = "111-4444" },
            new Shipper { ShipperId = 4, CompanyName = "Dell", Phone = "111-5555" },
            new Shipper { ShipperId = 5, CompanyName = "HP", Phone = "111-6666" }
        };
        _context.Shippers.AddRange(shipperList);
        _context.SaveChanges();
        var retrievedShippers = _repository.GetShippers();
        Assert.IsNotNull(retrievedShippers);
        Assert.AreEqual(5, retrievedShippers.Count());
        var retrievedShippersList = retrievedShippers.ToList();
        Assert.AreEqual("Apple", retrievedShippersList[2].CompanyName);
    }
}


