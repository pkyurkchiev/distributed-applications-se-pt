using MC.ApplicationServices.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGenres" in both code and config file together.
[ServiceContract]
public interface IGenres
{
    [OperationContract]
    string GetVersion();

    [OperationContract]
    IEnumerable<GenreDto> GetGenres();

    [OperationContract]
    GenreDto GetById(int id);

    [OperationContract]
    int Save(string title);

    [OperationContract]
    int Delete(int id);
}
