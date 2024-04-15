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

    public virtual async Task CreateShipperAsync(int shipperId, string companyName, string phone)
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

    public virtual async Task<Shipper?> GetShipperByIdAsync(int shipperId)
    {
        return await _context.Shippers.FindAsync(shipperId);
    }

    public virtual IEnumerable<Shipper> GetShippers()
    {
        foreach (var shipper in _context.Shippers)
        {
            yield return shipper;
        }
    }
}

