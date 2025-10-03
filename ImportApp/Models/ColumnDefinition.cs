using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportApp.Models
{
    public class ColumnDefinition
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; } = "VARCHAR2";
        public bool IsRequired { get; set; } = false;
    }
}


