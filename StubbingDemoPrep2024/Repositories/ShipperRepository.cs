using System;
using StubbingDemoPrep2024.Database.Models;

namespace StubbingDemoPrep2024.Repositories;

public class ShipperRepository
{
    private readonly NorthwindContext _context;

    public ShipperRepository(NorthwindContext context)
    {
        _context = context;
    }

    public void CreateShipper(int shipperId, string companyName, string phone)
    {
        var shipper = new Shipper()
        {
            ShipperId = shipperId,
            CompanyName = companyName,
            Phone = phone
        };

        try
        {
            _context.Shippers.Add(shipper);
            _context.SaveChanges();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception during database query: {exception.Message}");
            throw new CouldNotAddToDatabaseException(exception.Message, exception);
        }

    }
}

