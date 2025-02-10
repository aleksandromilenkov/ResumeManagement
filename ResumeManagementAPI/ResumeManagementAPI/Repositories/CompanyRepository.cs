using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ResumeManagementAPI.Data;
using ResumeManagementAPI.DTOs;
using ResumeManagementAPI.Interface;
using ResumeManagementAPI.Logs;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(Company entity)
        {
            try
            {
                if (await GetByAsync(company => company.Name == entity.Name) != null)
                {
                    return new Response()
                    {
                        Flag = false,
                        Message = "Cannot create company. A company with this name already exists."
                    };
                }

                await _context.Companies.AddAsync(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Company successfully created." : "Cannot create company. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while creating company.");
            }
        }

        public async Task<Response> DeleteAsync(Company entity)
        {
            try
            {
                _context.Companies.Remove(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Company successfully deleted." : "Cannot delete company. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while deleting company.");
            }
        }

        public async Task<Company?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Companies.Include(c=>c.Jobs).FirstOrDefaultAsync(j=> j.Id == id);
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while finding company by ID.");
            }
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            try
            {
                return await _context.Companies.Include(c => c.Jobs).ToListAsync();
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while retrieving all companies.");
            }
        }

        public async Task<Company?> GetByAsync(Expression<Func<Company, bool>> predicate)
        {
            try
            {
                return await _context.Companies.Include(c => c.Jobs).FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while retrieving company.");
            }
        }

        public async Task<Response> UpdateAsync(Company entity)
        {
            try
            {
                _context.Companies.Update(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Company successfully updated." : "Cannot update company. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while updating company.");
            }
        }
    }
}
