using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Infrastructure
{
     public interface IHasOwner<T> where T: class
    {
        // <summary>
        /// The object instance this object belongs to.
        /// </summary>
        T Owner { get; set; }

    }
}
