using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Enums
{
    public enum Payment_Method_Code
    {
        // <summary>
        /// Determines the Payment_Method.
        /// </summary>

        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a  Payment_Method : Cash.
        /// </summary>
        [Display(Name = "Cash")]
        Cash = 1,

        /// <summary>
        /// Indicates a Payment_Method : Check.
        /// </summary>        
        [Display(Name = "Check")]
        Check = 2,

        /// <summary>
        /// Indicates a  Payment_Method : Credit Card.
        /// </summary>
        [Display(Name = "Credit Card")]
        Credit_Card = 3,

        /// <summary>
        /// Indicates a  Payment_Method : Mobile Payments.
        /// </summary>
        [Display(Name = "Mobile Payments")]
        Mobile_Payments = 4,
    }
}
