using System;
using Microsoft.EntityFrameworkCore;
using StubbingDemoPrep2024.Database.Models;

namespace StubbingDemoPrep2024Tests.Stubs;

public class NorthwindContext_Stub : NorthwindContext
{
	public bool CauseError { get; set; } = false;

	public NorthwindContext_Stub(DbContextOptions<NorthwindContext> options)
		: base(options)
	{
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
}

