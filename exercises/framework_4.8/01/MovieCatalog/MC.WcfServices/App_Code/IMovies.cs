using MC.ApplicationServices.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMovies" in both code and config file together.
[ServiceContract]
public interface IMovies
{
    [OperationContract]
    string GetVersion();

    [OperationContract]
    IEnumerable<MovieDto> GetMovies();

    [OperationContract]
    int Save(string title);

    [OperationContract]
    int Delete(int id);
}
