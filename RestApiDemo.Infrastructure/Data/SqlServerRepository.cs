using Microsoft.EntityFrameworkCore;
using RestApiDemo.DomainCore.Models.Resource;
using RestApiDemo.Kernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiDemo.Infrastructure.Data
{
    class SqlServerRepository : IRepository<Resource>
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public SqlServerRepository(AppDbContext ctx, IEmailSender sender)
        {
            _context = ctx;
            _emailSender = sender;
        }

        public async Task<Resource> AddAsync(Resource entity)
        {
            Resource result = (await _context.Resources.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();

            string from = "testSender@test.com";
            string to = "testRecepient@test.com";
            string subject = "Added new resource!";
            string body = String.Format("A new resource has been added!\n\tResourceId:\t{0}\n\tResourceName:\t{1}\n\tResourceElementsCount:\t{2}", result.Id, result.Name, result.ResourceContents.Count);
            await _emailSender.SendEmailAsync(to, from, subject, body);

            return result;
        }

        public async Task DeleteAsync(Resource entity)
        {
            Resource toDelete = (await _context.Resources.Where(x => x.Id == entity.Id).FirstOrDefaultAsync());

            if (toDelete != null)
            {
                _context.Resources.Remove(toDelete);
                await _context.SaveChangesAsync();

                string from = "testSender@test.com";
                string to = "testRecepient@test.com";
                string subject = "Deleted a resource!";
                string body = String.Format(
                    "A resource has been deleted!\n\tResourceId:\t{0}\n\tResourceName:\t{1}\n\tResourceElementsCount:\t{2}",
                    toDelete.Id,
                    toDelete.Name,
                    toDelete.ResourceContents.Count
                );
                await _emailSender.SendEmailAsync(to, from, subject, body);
            }
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            List<Resource> result = (await _context.Resources
                                                    .Include(x=>x.ResourceContents)
                                                    .ToListAsync());
            return result;
        }

        public async Task<Resource> GetByIdAsync(int id)
        {
            Resource result = (await _context.Resources
                                                .Include(x => x.ResourceContents)
                                                .Where(x => x.Id == id)
                                                .FirstOrDefaultAsync());
            return result;
        }

        public async Task UpdateAsync(Resource entity)
        {
            Resource toUpdate = (await _context.Resources.Where(x => x.Id == entity.Id).FirstOrDefaultAsync());

            if (toUpdate != null)
            {
                toUpdate.Name = entity.Name;
                toUpdate.ResourceContents = entity.ResourceContents;
                _context.Resources.Update(toUpdate);
                await _context.SaveChangesAsync();

                string from = "testSender@test.com";
                string to = "testRecepient@test.com";
                string subject = "Deleted a resource!";
                string body = String.Format(
                    "A resource has been updated!\n\tResourceId:\t{0}\n\tResourceName:\t{1}\n\tResourceElementsCount:\t{2}",
                    toUpdate.Id,
                    toUpdate.Name,
                    toUpdate.ResourceContents.Count
                );
                await _emailSender.SendEmailAsync(to, from, subject, body);
            }
        }
    }
}
