using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;

namespace Server.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly RegContext _context;
        public FileRepository(RegContext context)
        {
            _context = context;
            
        }
        public async Task<RequestFile> GetFile(Guid id)
        {

            return await _context.Files.FindAsync(id);
        }

        public async Task<List<RequestFile>> GetFilesByRequestId(Guid requestId)
        {
            return await _context.Files.Where(f => f.RequestAbonentId == requestId).ToListAsync(); 
        }

        public async Task SaveFile(RequestFile file)
        {
            if(file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            _context.Files.Add(file);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFile(RequestFile file)
        {
            if(file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            _context.Files.Remove(file);
            
            await _context.SaveChangesAsync();
        }
    }
}