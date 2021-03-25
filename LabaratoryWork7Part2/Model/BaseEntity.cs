using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaratoryWork7Part2.Model
{
    public abstract class BaseModel
    {
        public BaseModel(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
