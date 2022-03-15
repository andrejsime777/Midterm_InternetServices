using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityApplication.Data;
using UniversityApplication.Data.Entities;
using UniversityApplication.Models.DTOs;
using UniversityApplication.Service.Interfaces;
using AutoMapper;

namespace UniversityApplication.Service.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly UniversityDataContext _dataContext;
        private readonly IMapper _mapper;

        public PlayerService(UniversityDataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public IEnumerable<PlayerDTO> GetPlayers()
        {
            //1. Entities
            //return await _dataContext.Players.ToListAsync();

            //2. DTOs
            //return _dataContext.Players.Select(x => new PlayerDTO
            //{
            //    Id = x.Id,
            //    FirstName = x.FirstName,
            //    LastName = x.LastName,
            //    DOB = x.DOB,
            //    EnrollmentDate = x.EnrollmentDate,
            //    PlayerIndex = x.PlayerIndex,
            //    GPA = x.GPA,
            //    Mail = x.Mail,
            //    ClubId = x.ClubId,
            //    Club = new ClubDTO
            //    {
            //        Id = x.Club.Id,
            //        Street = x.Club.Street,
            //        City = x.Club.City,
            //        Country = x.Club.Country
            //    }
            //}).ToList();

            //3. AutoMapper DTOs
            var Players = _dataContext.Players.Include(s => s.Club);
            return _mapper.Map<IEnumerable<PlayerDTO>>(Players);
        }

        public async Task<PlayerDTO> GetPlayerById(int id)
        {
            //Would cause circular dependency error, so we need DTOs
            //return await _dataContext.Players.Include(s => s.Club).FirstOrDefaultAsync(x => x.Id == id);

            var Player = await _dataContext.Players.Include(s => s.Club).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<PlayerDTO>(Player);
        }

        public PlayerDTO AddPlayer(PlayerDTO Player)
        {
            Player newPlayer = _mapper.Map<Player>(Player);

            if (_dataContext.Players.FirstOrDefault(s => s.Id == Player.Id) == null)
            {
                _dataContext.Players.Add(newPlayer);
                _dataContext.SaveChanges();
            }
            return _mapper.Map<PlayerDTO>(newPlayer);
        }

        public PlayerDTO UpdatePlayer(PlayerDTO Player)
        {
            Player newPlayer = _mapper.Map<Player>(Player);
            Player oldPlayer = _dataContext.Players.FirstOrDefault(s => s.Id == newPlayer.Id);

            if (oldPlayer != null)
            {
                _dataContext.Entry(oldPlayer).CurrentValues.SetValues(newPlayer);
                _dataContext.SaveChanges();
            }
            return _mapper.Map<PlayerDTO>(newPlayer);
        }

        public async Task<bool> DeletePlayer(int id)
        {
            var PlayerEntity = await _dataContext.Players.FindAsync(id);

            /* If we want to delete the Club associated with the current Player (in 1-to-1 relations) we should:
            //1. First retrieve the Club associated with the current Player ClubId
            var ClubEntity = await _dataContext.Clubs.FindAsync(PlayerEntity.ClubId);
            
            //2. Then delete all Players associated with the current ClubId
            var PlayersWithClub = await _dataContext.Players.Where(s => s.Id == PlayerEntity.Club.Id).ToListAsync();
            foreach(var Player in PlayersWithClub)
            {
                _dataContext.Players.Remove(Player);
            }

            //2. Then delete the Club associated with those Players
            _dataContext.Clubs.Remove(ClubEntity);
            */

            //If we want to remove only the Player without the associated Club (for 1-to-m relations) as in our case 1 Club - many Players, we should remove the Player only
            _dataContext.Players.Remove(PlayerEntity);
            return await SaveAsync() > 0;
        }

        public async Task<int> SaveAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
