using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part2.Model
{
    public class AddEditProductResult
    {
        public Product ResultProduct { get; set; }
        public bool IsCreatingNew { get; set; }
        public bool IsCancel { get; set; }
    }
}
