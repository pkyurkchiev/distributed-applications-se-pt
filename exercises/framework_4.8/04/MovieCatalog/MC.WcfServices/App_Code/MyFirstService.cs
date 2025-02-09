using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyFirstService" in code, svc and config file together.
public class MyFirstService : IMyFirstService
{
    public string DoWork(bool isWorking)
    {
        if (isWorking)
            return "I`m working";
        else
            return "I`m not working";
    }

    // NOTE: Create method Calculator. Return Perimeter & Aria.
    public Results Calculator(Params inputParams)
    {
        return new Results {
            Perimeter = 2 * inputParams.A + 2 * inputParams.B,
            Aria = inputParams.A * inputParams.B
        };
    }
}
