using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Collections
{
    /// <summary>
    /// Represents a collection of Patient_Bill_Items instances in the system.
    /// </summary>
  public class Patient_Bill_Items: CollectionBase<Patient_Bill_Item>
    {
      
      /// <summary>
      /// Initializes a new instance of the <see cref="Patients"/> class.
    /// </summary>
    public Patient_Bill_Items() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Patients"/> class.
    /// </summary>
    /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
    public Patient_Bill_Items(IList<Patient_Bill_Item> initialList) : base(initialList) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Patients"/> class.
    /// </summary>
    /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
    public Patient_Bill_Items(CollectionBase<Patient_Bill_Item> initialList) : base(initialList) { }

    /// <summary>
    /// Validates the current collection by validating each individual item in the collection.
    /// </summary>
    /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
    public IEnumerable<ValidationResult> Validate()
    {
      var errors = new List<ValidationResult>();
      foreach (var patient_bill_item in this)
      {
          errors.AddRange(patient_bill_item.Validate());
      }
      return errors;
    }
    }
}
