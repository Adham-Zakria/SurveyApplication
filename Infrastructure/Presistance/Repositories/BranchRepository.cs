using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Repositories
{
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        private readonly SurveyAppContext _context;

        public BranchRepository(SurveyAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Branch>> GetBranchesByIdsAsync(List<int> branchIds)
        {
            return await _context.Branches
                .Where(b => branchIds.Contains(b.BranchId))
                .ToListAsync();
        }
    }

}
