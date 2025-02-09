using MC.ApplicationServices;
using MC.ApplicationServices.DTOs;
using System.Collections.Generic;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Movies" in code, svc and config file together.
public class Movies : IMovies
{
    #region
    private readonly MoviesManagementService _service = new MoviesManagementService();
    #endregion

    public string GetVersion()
    {
        return "Movies version 1.0";
    }

    public IEnumerable<MovieDto> GetMovies()
    {
        return _service.GetAll();
    }

    public int Save(string title)
    {
        return _service.Save(new MovieDto { Title = title });
    }

    public int Delete(int id)
    {
        return _service.Delete(id);
    }
}
