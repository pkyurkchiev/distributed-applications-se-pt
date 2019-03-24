using MC.ApplicationServices.DTOs;
using MC.Data.Contexts;
using MC.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC.ApplicationServices.Implementations
{
    public class DirectorManagementService
    {
        #region Properties
        private readonly MovieCatalogDbContext _context = new MovieCatalogDbContext();
        #endregion

        #region public Methods
        public List<DirectorDto> Get()
        {
            List<Director> directors = _context.Directors.ToList();

            return directors.Select(x => new DirectorDto(x)).ToList();
        }

        public DirectorDto GetById(int id)
        {
            return new DirectorDto(_context.Directors.FirstOrDefault(x => x.Id == id));
        }

        public List<DirectorDto> GetByFirstName(string firstName)
        {
            List<Director> directors = _context.Directors.Where(x => x.FirstName == firstName).ToList();

            return directors.Select(x => new DirectorDto(x)).ToList();
        }

        public int Save(DirectorDto directorDto)
        {
            Director director = new Director
            {
                FirstName = directorDto.FirstName,
                LastName = directorDto.LastName,
                UserName = directorDto.UserName,
                IsActive = directorDto.IsActive
            };

            try
            {
                _context.Directors.Add(director);
                _context.SaveChanges();

                return director.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int Delete(int id)
        {
            try
            {
                Director director = _context.Directors.Find(id);
                if (director == null)
                    return -1;

                _context.Directors.Remove(director);
                _context.SaveChanges();

                return id;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        #endregion
    }
}
