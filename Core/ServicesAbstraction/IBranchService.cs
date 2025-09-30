using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IBranchService
    {
        Task<IEnumerable<Branch>> GetAllAsync();
    }
}
