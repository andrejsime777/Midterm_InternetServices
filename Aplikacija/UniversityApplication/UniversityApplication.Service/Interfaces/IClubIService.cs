using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApplication.Data.Entities;

namespace UniversityApplication.Service.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetClubs();
        Task<Club> GetClubById(int id);

    }
}
