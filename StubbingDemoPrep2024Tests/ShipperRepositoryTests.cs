using Microsoft.EntityFrameworkCore;
using StubbingDemoPrep2024;
using StubbingDemoPrep2024.Database.Models;
using StubbingDemoPrep2024.Repositories;
using StubbingDemoPrep2024Tests.Stubs;

namespace StubbingDemoPrep2024Tests;

[TestClass]
public class ShipperRepositoryTests
{
    private NorthwindContext_Stub _context;
    private ShipperRepository _repository;

    [TestInitialize]
    public void TestInitialize()
    {
        var dBContextOptions = new DbContextOptionsBuilder<NorthwindContext>()
            .UseInMemoryDatabase("Northwind").Options;
        _context = new NorthwindContext_Stub(dBContextOptions);
        _repository = new ShipperRepository(_context);
    }

    [TestMethod]
    public void ShipperRepository_Constructor_IsNotNull()
    {
        Assert.IsNotNull(_repository);
    }

    [TestMethod]
    [ExpectedException(typeof(CouldNotAddToDatabaseException))]
    public void ShipperRepository_CreateShipper_ShouldThrowCorrectExceptionOnDatabaseFailure()
    {
        _context.CauseError = true;
        _repository.CreateShipper(2, "Test", "111-2222");
    }
}


