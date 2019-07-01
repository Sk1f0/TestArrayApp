using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArrayWepApi.Models
{
    public interface IArrayOperations
    {
        CustomArray Create();
        CustomArray Rotate(CustomArray array);
        CustomArray Import(string fileText);
        string Export(CustomArray array);
    }
}