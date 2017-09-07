using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ERMSolution
{
    public class CsvHelper : List<string>
    {
        public CsvHelper(string csv, string separator = "\",\"")
        {
            var lines = Regex.Split(csv, Environment.NewLine).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            if (lines.Count() > 1) //exclude header in file
            {
                for (int i = 1; i < lines.Count(); i++)
                {
                    string value = Regex.Split(lines[i], separator)[0];

                    //Trim values
                    value = value.Trim('\"');

                    Add(value);
                }
            }
        }
    }
}
