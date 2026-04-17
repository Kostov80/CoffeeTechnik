using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using CoffeeTechnik.Services;
using CoffeeTechnik.ViewModels;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ServiceRequestServiceTests
{
    private ApplicationDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public void GetAll_ShouldReturnEmptyList_WhenNoData()
    {
        var context = GetDbContext();
        var service = new ServiceRequestService(context);

        var result = service.GetAll(null, null, 1);

        Assert.Empty(result.Items);
    }

    [Fact]
    public void CreateMontage_ShouldAddServiceRequest()
    {
        var context = GetDbContext();
        var service = new ServiceRequestService(context);

        var model = new MontageViewModel
        {
            ObjectName = "Test Object",
            City = "Plovdiv",
            Address = "Center",
            BULSTAT = "123456789",
            ContactPerson = "Ivan",
            Phone = "0888888888",
            MachineModel = "Test Machine",
            MachineConnection = "SN123",
            Requester = "Tester"
        };

        service.CreateMontage(model);

        Assert.Equal(1, context.ServiceRequests.Count());
    }

    [Fact]
    public void GetById_ShouldReturnCorrectRequest()
    {
        var context = GetDbContext();

        context.ServiceRequests.Add(new ServiceRequest
        {
            Id = 1,
            RequestType = "Тест",
            Description = "Описание",
            CreatedOn = DateTime.Now,
            Requester = "Tester"
        });

        context.SaveChanges();

        var service = new ServiceRequestService(context);

        var result = service.GetById(1);

        Assert.NotNull(result);
        Assert.Equal("Тест", result.RequestType);
    }

    [Fact]
    public void Delete_ShouldRemoveRequest()
    {
        var context = GetDbContext();

        context.ServiceRequests.Add(new ServiceRequest
        {
            Id = 1,
            RequestType = "Test",
            Description = "Test",
            CreatedOn = DateTime.Now,
            Requester = "Tester"
        });

        context.SaveChanges();

        var service = new ServiceRequestService(context);

        var result = service.Delete(1);

        Assert.True(result);
        Assert.Empty(context.ServiceRequests);
    }
}