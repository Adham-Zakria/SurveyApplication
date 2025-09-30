using Domain.Contracts;
using Domain.Models;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BranchService(IGenericRepository<Branch> _branchRepository) : IBranchService
    {
        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _branchRepository.GetAllAsync();
        }
    }
}