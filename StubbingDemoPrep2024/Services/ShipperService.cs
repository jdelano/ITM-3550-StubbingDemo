using StubbingDemoPrep2024;
using StubbingDemoPrep2024.Database.Models;
using StubbingDemoPrep2024.Exceptions;
using StubbingDemoPrep2024.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

namespace StubbingDemoPrep2024.Services;

public class ShipperService
{
    private ShipperRepository _repository;

    public ShipperService(ShipperRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateShipperAsync(int shipperId, string companyName, string phone)
    {
        int attempts = 0;
        while (attempts < 3)
        {
            try
            {
                await _repository.CreateShipperAsync(shipperId, companyName, phone);
                return true;
            }
            catch (CouldNotAddToDatabaseException)
            {
                attempts++;
            }
        }
        return false;
    }

    public async Task<Shipper?> GetShipperByIdAsync(int shipperId)
    {
        var shipper = await _repository.GetShipperByIdAsync(shipperId);
        if (shipper is null)
        {
            throw new ShipperNotFoundException();
        }
        else
        {
            return shipper;
        }
    }

    public IEnumerable<Shipper> GetShippers()
    {
        return _repository.GetShippers();
        //foreach (var shipper in _repository.GetShippers())
        //{
        //    yield return shipper;
        //}
    }
}

