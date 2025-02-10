using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ResumeManagementAPI.Data;
using ResumeManagementAPI.DTOs;
using ResumeManagementAPI.Interface;
using ResumeManagementAPI.Logs;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(Job entity)
        {
            try
            {
                await _context.Jobs.AddAsync(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Job successfully created." : "Cannot create job. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while creating job.");
            }
        }

        public async Task<Response> DeleteAsync(Job entity)
        {
            try
            {
                _context.Jobs.Remove(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Job successfully deleted." : "Cannot delete job. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while deleting job.");
            }
        }

        public async Task<Job?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Jobs.Include(j => j.Company).Include(j => j.Candidates).FirstOrDefaultAsync(j=> j.Id == id);
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while finding job by ID.");
            }
        }

        public async Task<IEnumerable<Job>> GetAllAsync()
        {
            try
            {
                return await _context.Jobs.Include(j => j.Company).Include(j => j.Candidates).ToListAsync();
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while retrieving all jobs.");
            }
        }

        public async Task<Job?> GetByAsync(Expression<Func<Job, bool>> predicate)
        {
            try
            {
                return await _context.Jobs.Include(j=> j.Company).Include(j=>j.Candidates).FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while retrieving job.");
            }
        }

        public async Task<Response> UpdateAsync(Job entity)
        {
            try
            {
                _context.Jobs.Update(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Job successfully updated." : "Cannot update job. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while updating job.");
            }
        }
    }
}
