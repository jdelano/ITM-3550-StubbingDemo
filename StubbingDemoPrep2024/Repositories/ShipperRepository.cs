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

    public async Task CreateShipper(int shipperId, string companyName, string phone)
    {
        var shipper = new Shipper()
        {
            ShipperId = shipperId,
            CompanyName = companyName,
            Phone = phone
        };

        try
        {
            await _context.Shippers.AddAsync(shipper);
            await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Exception during database query: {exception.Message}");
            throw new CouldNotAddToDatabaseException(exception.Message, exception);
        }

    }

    public async Task<Shipper> GetShipperByIdAsync(int shipperId)
    {
        var shipper = await _context.Shippers.FindAsync(shipperId);
        if (shipper is null)
        {
            throw new ArgumentException();
        }
        else
        {
            return shipper;
        }
    }
}

