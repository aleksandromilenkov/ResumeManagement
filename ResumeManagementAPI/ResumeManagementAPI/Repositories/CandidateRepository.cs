using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ResumeManagementAPI.Data;
using ResumeManagementAPI.DTOs;
using ResumeManagementAPI.Interface;
using ResumeManagementAPI.Logs;
using ResumeManagementAPI.Models;

namespace ResumeManagementAPI.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(Candidate entity)
        {
            try
            {
                // Check if a candidate with the same email already exists
                if (await GetByAsync(c => c.Email == entity.Email) != null)
                {
                    return new Response()
                    {
                        Flag = false,
                        Message = "Cannot create candidate. Email already exists."
                    };
                }

                await _context.Candidates.AddAsync(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Candidate successfully created." : "Cannot create candidate. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while creating candidate.");
            }
        }

        public async Task<Response> DeleteAsync(Candidate entity)
        {
            try
            {
                _context.Candidates.Remove(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Candidate successfully deleted." : "Cannot delete candidate. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while deleting candidate.");
            }
        }

        public async Task<Candidate?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Candidates.FindAsync(id);
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while finding candidate by ID.");
            }
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            try
            {
                return await _context.Candidates.ToListAsync();
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while retrieving all candidates.");
            }
        }

        public async Task<Candidate?> GetByAsync(Expression<Func<Candidate, bool>> predicate)
        {
            try
            {
                return await _context.Candidates.FirstOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while retrieving candidate.");
            }
        }

        public async Task<Response> UpdateAsync(Candidate entity)
        {
            try
            {
                _context.Candidates.Update(entity);
                var resp = await _context.SaveChangesAsync();

                return new Response()
                {
                    Flag = resp > 0,
                    Message = resp > 0 ? "Candidate successfully updated." : "Cannot update candidate. Something went wrong."
                };
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred while updating candidate.");
            }
        }
    }
}
