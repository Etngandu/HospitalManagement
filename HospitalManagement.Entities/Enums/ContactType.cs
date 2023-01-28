using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Enums
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum ContactType
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Business = 1,

        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Personal = 2
    }
}
