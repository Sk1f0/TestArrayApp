using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArrayWepApi.Models
{
    public class CustomArray
    {
        public CustomArray()
        { }

        public CustomArray(int[,] value)
        {
            this.Value = value;
        }

        public int[,] Value { get; set; }
        public int Size
        {
            get
            {
                return Value.GetLength(0) != Value.GetLength(1) ? -1 : Value.GetLength(0);
            }
        }
    }
}