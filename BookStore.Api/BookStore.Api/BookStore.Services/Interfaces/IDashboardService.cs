using BookStore.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboard();
    }
}
