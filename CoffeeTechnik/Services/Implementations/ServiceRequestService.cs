using CoffeeTechnik.Data;
using CoffeeTechnik.Models;
using CoffeeTechnik.ViewModels;
using CoffeeTechnik.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTechnik.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly ApplicationDbContext _context;

        public ServiceRequestService(ApplicationDbContext context)
        {
            _context = context;
        }
                
        public ServiceRequestListResult GetAll(string requestType, string searchString, int page)
        {
            int pageSize = 5;

            var query = _context.ServiceRequests
                .Include(s => s.Machine)
                    .ThenInclude(m => m.ObjectEntity)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(requestType))
                query = query.Where(s => s.RequestType == requestType);

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(s =>
                    s.Description.Contains(searchString) ||
                    s.RequestType.Contains(searchString) ||
                    (s.Machine != null && s.Machine.Model != null &&
                        s.Machine.Model.Contains(searchString)) ||
                    (s.Machine != null && s.Machine.ObjectEntity != null &&
                        s.Machine.ObjectEntity.Name.Contains(searchString)) ||
                    (s.Machine != null && s.Machine.ObjectEntity != null &&
                        s.Machine.ObjectEntity.City.Contains(searchString))
                );
            }

            int totalItems = query.Count();

            var items = query
                .OrderByDescending(s => s.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new ServiceRequestListResult
            {
                Items = items,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize)
            };
        }
                
        public ServiceRequest GetById(int id)
        {
            return _context.ServiceRequests
                .Include(r => r.Machine)
                    .ThenInclude(m => m.ObjectEntity)
                .FirstOrDefault(r => r.Id == id);
        }
                
        public void CreateMontage(MontageViewModel model)
        {
            if (model == null) return;

            var obj = new ObjectEntity
            {
                Name = model.ObjectName,
                City = model.City,
                Address = model.Address,
                Bulstat = model.BULSTAT,
                ContactPerson = model.ContactPerson,
                PhoneNumber = model.Phone,
                Type = "Монтаж",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = model.MachineModel,
                SerialNumber = model.MachineConnection,
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Монтаж",
                MachineId = machine.Id,
                Description = $"Монтаж на {machine.Model} за {obj.Name}",
                CreatedOn = DateTime.Now,
                Requester = model.Requester
            });

            _context.SaveChanges();
        }
                
        public void CreateDemontage(string objectName, string requester, string reason)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(reason))
                return;

            var obj = new ObjectEntity
            {
                Name = objectName,
                Type = "Демонтаж",
                Bulstat = "N/A",
                City = "N/A",
                Address = "N/A",
                ContactPerson = "N/A",
                PhoneNumber = "N/A",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = "N/A",
                SerialNumber = "N/A",
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Демонтаж",
                MachineId = machine.Id,
                Description = reason,
                CreatedOn = DateTime.Now,
                Requester = requester
            });

            _context.SaveChanges();
        }
              
        public void CreateEmergency(string objectName, string requester, string details)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(details))
                return;

            var obj = new ObjectEntity
            {
                Name = objectName,
                Type = "Авария",
                Bulstat = "N/A",
                City = "N/A",
                Address = "N/A",
                ContactPerson = "N/A",
                PhoneNumber = "N/A",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = "N/A",
                SerialNumber = "N/A",
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Авария",
                MachineId = machine.Id,
                Description = details,
                CreatedOn = DateTime.Now,
                Requester = requester
            });

            _context.SaveChanges();
        }
                
        public void CreateMaintenance(string objectName, string requester, string details)
        {
            if (string.IsNullOrWhiteSpace(objectName) ||
                string.IsNullOrWhiteSpace(requester) ||
                string.IsNullOrWhiteSpace(details))
                return;

            var obj = new ObjectEntity
            {
                Name = objectName,
                Type = "Профилактика",
                Bulstat = "N/A",
                City = "N/A",
                Address = "N/A",
                ContactPerson = "N/A",
                PhoneNumber = "N/A",
                Firma = "Неизвестна"
            };

            _context.Objects.Add(obj);
            _context.SaveChanges();

            var machine = new Machine
            {
                Model = "N/A",
                SerialNumber = "N/A",
                ObjectEntityId = obj.Id
            };

            _context.Machines.Add(machine);
            _context.SaveChanges();

            _context.ServiceRequests.Add(new ServiceRequest
            {
                RequestType = "Профилактика",
                MachineId = machine.Id,
                Description = details,
                CreatedOn = DateTime.Now,
                Requester = requester
            });

            _context.SaveChanges();
        }
                
        public bool Delete(int id)
        {
            var request = _context.ServiceRequests.Find(id);

            if (request == null)
                return false;

            _context.ServiceRequests.Remove(request);
            _context.SaveChanges();

            return true;
        }
    }
}