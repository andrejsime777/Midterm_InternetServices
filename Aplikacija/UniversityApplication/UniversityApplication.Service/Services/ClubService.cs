using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApplication.Data;
using UniversityApplication.Data.Entities;
using UniversityApplication.Service.Interfaces;

namespace UniversityApplication.Service.Services
{
    public class ClubService : IClubService
    {
        private readonly UniversityDataContext _dataContext;

        public ClubService(UniversityDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Club> GetClubById(int id)
        {
            return await _dataContext.Clubs.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubs()
        {
            return await _dataContext.Clubs.ToListAsync();
        }
    }
}
