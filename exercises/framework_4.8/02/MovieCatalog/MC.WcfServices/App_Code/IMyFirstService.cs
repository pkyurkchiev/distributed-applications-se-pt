using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyFirstService" in both code and config file together.
[ServiceContract]
public interface IMyFirstService
{
    [OperationContract]
    string DoWork(bool isWorking);

    [OperationContract]
    Results Calculator(Params inputParams);
}

[DataContract]
public class Params
{ 
    [DataMember]
    public double A { get; set; }
    [DataMember]
    public double B { get; set; }
}


[DataContract]
public class Results
{
    [DataMember]
    public double Perimeter { get; set; }
    [DataMember]
    public double Aria { get; set; }
}