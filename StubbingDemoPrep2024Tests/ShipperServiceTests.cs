using System;
using Moq;
using StubbingDemoPrep2024.Repositories;

namespace StubbingDemoPrep2024Tests
{
	[TestClass]
	public class ShipperServiceTests
	{
		private Mock<ShipperRepository> _mockShipperRepository = default!;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockShipperRepository = new Mock<ShipperRepository>(new object[] { });
		}

		[TestMethod]
		public void ShipperService_Constructor()
		{
			var service = new ShipperService(_mockShipperRepository.Object);

		}

	}
}

