using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingAttire.Models;

namespace ShoppingAttire.DAL
{
    public interface ILogger
    {

        public void CreateLog(string logType, string details);
    }
}
