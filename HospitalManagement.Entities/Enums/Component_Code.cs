using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Enums
{
   public enum Component_Code
    {
        // <summary>
        /// Determines the Component_Code.
        /// </summary>

        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a  Component_Code : Admission.
        /// </summary>
        [Display(Name = "Admission")]
        Admission = 1,

        /// <summary>
        /// Indicates a Component_Code : Diagnosis.
        /// </summary>        
        [Display(Name = "Diagnosis")]
        Diagnosis = 2,

        /// <summary>
        /// Indicates a  Component_Code : Medication.
        /// </summary>
        [Display(Name = "Medication")]
        Medication = 3,

        
    }
}
