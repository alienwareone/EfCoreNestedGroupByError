using EfCoreNestedGroupByError;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

var dbContextOptionsBuilder = new DbContextOptionsBuilder();
dbContextOptionsBuilder.UseSqlServer("Data Source = .\\SQLEXPRESS; Database = EfCoreNestedGroupByError; Integrated Security = True;");

var applicationDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
applicationDbContext.Database.EnsureCreated();

if (!applicationDbContext.PageImpressions.Any())
{
    for (var counter = 1; counter <= 10; counter++)
    {
        applicationDbContext.PageImpressions.Add(new()
        {
            IpAddress = $"192.168.1.{counter}",
            UserAgent = counter <= 3 ? "Firefox" : "Chrome",
            CreationDateTime = DateTime.Now.AddDays(-counter)
        });
    }
    applicationDbContext.SaveChanges();
}

var result = applicationDbContext.PageImpressions
    .GroupBy(x => x.CreationDateTime.Date)
    .Select(x => new
    {
        TotalCount = x.Count(),
        UniqueIpAddressCount = x.GroupBy(x => x.IpAddress).Count(),
        UniqueUserAgentCount = x.GroupBy(x => x.UserAgent).Count()
    })
    .ToList();
