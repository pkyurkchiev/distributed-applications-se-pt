using MC.ApplicationServices;
using MC.ApplicationServices.DTOs;
using System.Collections.Generic;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Genres" in code, svc and config file together.
public class Genres : IGenres
{
    #region
    private readonly GenresManagementService _service = new GenresManagementService();
    #endregion

    public string GetVersion()
    {
        return "Genres version 1.0";
    }

    public IEnumerable<GenreDto> GetGenres()
    {
        return _service.GetAll();
    }

    public GenreDto GetById(int id)
    {
        return _service.GetById(id);
    }

    public int Save(string name)
    {
        return _service.Save(new GenreDto { Name = name });
    }

    public int Delete(int id)
    {
        return _service.Delete(id);
    }
}
