using System;
using Moq;
using StubbingDemoPrep2024;
using StubbingDemoPrep2024.Database.Models;
using StubbingDemoPrep2024.Exceptions;
using StubbingDemoPrep2024.Repositories;
using StubbingDemoPrep2024.Services;

namespace StubbingDemoPrep2024Tests;

[TestClass]
public class ShipperServiceTests
{
	private Mock<ShipperRepository> _mockShipperRepository = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		_mockShipperRepository = new Mock<ShipperRepository>(null!);
	}

	[TestMethod]
	public void ShipperService_Constructor()
	{
		var service = new ShipperService(_mockShipperRepository.Object);
		Assert.IsNotNull(service);
	}

	[TestMethod]
	public async Task GetShipperByIdAsync_ShouldReturnShipper_WhenShipperExists()
	{
		_mockShipperRepository
			.Setup(repo => repo.GetShipperByIdAsync(1))
			.ReturnsAsync(new Shipper
			{
				ShipperId = 1,
				CompanyName = "Microsoft",
				Phone = "111-2222"
			});
		var service = new ShipperService(_mockShipperRepository.Object);
		var shipper = await service.GetShipperByIdAsync(1);
		Assert.AreEqual(1, shipper?.ShipperId);
		Assert.AreEqual("Microsoft", shipper?.CompanyName);
		Assert.AreEqual("111-2222", shipper?.Phone);
	}

	[TestMethod]
	[ExpectedException(typeof(ShipperNotFoundException))]
	public async Task GetShipperByIdAsync_ShouldThrowException_WhenShipperNotFound()
	{
		_mockShipperRepository
			.Setup(repo => repo.GetShipperByIdAsync(1))
			.ReturnsAsync((Shipper)null!);
        var service = new ShipperService(_mockShipperRepository.Object);
        var shipper = await service.GetShipperByIdAsync(1);
    }

	[TestMethod]
	public async Task CreateShipperAsync_ShouldCallRepository()
	{
        var service = new ShipperService(_mockShipperRepository.Object);
		var result = await service.CreateShipperAsync(1, "Microsoft", "111-2222");
		Assert.IsTrue(result);
		_mockShipperRepository
			.Verify(repo => repo.CreateShipperAsync(1, "Microsoft", "111-2222"));
    }

	[TestMethod]
	public async Task CreateShipperAsync_ShouldRetryTwice()
	{
		_mockShipperRepository
			.SetupSequence(repo => repo.CreateShipperAsync(It.IsAny<int>(), "Microsoft", "111-2222"))
			.ThrowsAsync(new CouldNotAddToDatabaseException())
			.ThrowsAsync(new CouldNotAddToDatabaseException())
			.Returns(Task.CompletedTask);
        var service = new ShipperService(_mockShipperRepository.Object);
        var result = await service.CreateShipperAsync(1, "Microsoft", "111-2222");
		Assert.IsTrue(result);
		_mockShipperRepository
			.Verify(repo => repo.CreateShipperAsync(1, "Microsoft", "111-2222"), Times.Exactly(3));
    }

	[TestMethod]
	public void GetShippers_ShouldReturnShippers()
	{
		_mockShipperRepository
			.Setup(repo => repo.GetShippers())
			.Returns(new List<Shipper>
		{
			new Shipper { ShipperId = 1, CompanyName = "Microsoft", Phone = "111-2222" },
			new Shipper { ShipperId = 2, CompanyName = "Google", Phone = "111-3333" },
			new Shipper { ShipperId = 3, CompanyName = "Apple", Phone = "111-4444" },
			new Shipper { ShipperId = 4, CompanyName = "Dell", Phone = "111-5555" },
			new Shipper { ShipperId = 5, CompanyName = "HP", Phone = "111-6666" }
		});
        var service = new ShipperService(_mockShipperRepository.Object);
		var result = service.GetShippers();
		Assert.IsNotNull(result);
		var resultList = result.ToList();
		Assert.AreEqual(5, resultList.Count());
		Assert.AreEqual("Apple", resultList[2].CompanyName);

    }
}

