using System;
using Microsoft.EntityFrameworkCore;
using StubbingDemoPrep2024.Database.Models;

namespace StubbingDemoPrep2024Tests.Stubs;

public class NorthwindContext_Stub : NorthwindContext
{
	public bool CauseError { get; set; } = false;

    // Need to ensure that if we have configured the database for InMemory
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		optionsBuilder.UseInMemoryDatabase("Northwind");
    }

    public override int SaveChanges()
	{
		if (CauseError)
		{
			throw new Exception();
		}
		else
		{
			return base.SaveChanges();
		}
	}

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (CauseError)
        {
            throw new Exception();
        }
        else
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

