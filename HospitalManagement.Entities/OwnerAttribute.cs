using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
     public class OwnerAttribute: Attribute
    {
        public OwnerAttribute(string link)
        {
            Link = link;
        }
        public string Link { get; }
    }
}
