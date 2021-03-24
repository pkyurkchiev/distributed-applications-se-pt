using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Helpers;
using System;
using System.Linq;

namespace MC.ApplicationServices
{
    public class DirectorsManagementService : BaseManagementService
    {
        public DirectorDto GetByFirstName(string firstName)
        {
            return _context.Directors.AsNoTracking().SingleOrDefault(x => x.FirstName == firstName).ToDirectorDto();
        }

        public int Save(DirectorDto directorDto)
        {
            try
            {
                _context.Directors.Add(directorDto.ToDirectorEntity());
                _context.SaveChanges();
                return 1;
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
                var director = _context.Directors.Find(id);
                if (director == null)
                    return -1;

                _context.Directors.Remove(director);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
