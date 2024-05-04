using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.Viewables;

namespace TimeTracker.DataAccess.DataContracts
{
    public interface IEntity
    {
        int Id { get; }
    }
}
