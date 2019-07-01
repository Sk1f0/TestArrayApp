using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ArrayWepApi.Models
{
    public class ArrayOperations : IArrayOperations
    {
        public CustomArray Create()
        {
            var rand = new Random();
            var N = rand.Next(2, 10);

            var array = new int[N,N];

            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    array[i, j] = rand.Next(0, 100);

            return new CustomArray(array);
        }

        public CustomArray Rotate(CustomArray array)
        {
            if (array.Value.Length > 1 && array.Size > 1)
            {
                int temp;
                int size = array.Size;

                for (int i = 0; i < Math.Floor((double)size / 2); i++)
                    for (int j = 0; j < Math.Ceiling((double)size / 2); j++)
                    {
                        temp = array.Value[size - 1 - i, size - 1 - j];
                        array.Value[size - 1 - i, size - 1 - j] = array.Value[j, size - 1 - i];
                        array.Value[j, size - 1 - i] = array.Value[i, j];
                        array.Value[i, j] = array.Value[size - 1 - j, i];
                        array.Value[size - 1 - j, i] = temp;
                    }
            }
            return array;
        }

        public string Export(CustomArray array)
        {
            if (array.Size < 1)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < array.Size; i++)
            {
                sb.Append(array.Value[i, 0]);
                for (int j = 1; j < array.Size; j++)
                    sb.Append($",{array.Value[i, j]}");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        public CustomArray Import(string fileText)
        {
            CustomArray array = new CustomArray();
            try
            {
                var lines = fileText.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var col = lines[0].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Length;
                if (lines.Length != col)
                {
                    return null;
                }
                array.Value = new int[col, col];
                for (int i = 0; i < col; i++)
                {
                    var lineArray = lines[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < col; j++)
                    {
                        if (int.TryParse(lineArray[j], out int n))
                            array.Value[i, j] = n;
                        else
                            array.Value[i, j] = -1;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            { }

            return array;
        }
    }
}