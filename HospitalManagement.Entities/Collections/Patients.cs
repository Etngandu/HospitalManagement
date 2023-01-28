﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Entities.Collections
{
    /// <summary>
    /// Represents a collection of Patients instances in the system.
    /// </summary>
  public class Patients  : CollectionBase<Patient>
  {
      /// <summary>
      /// Initializes a new instance of the <see cref="Patients"/> class.
    /// </summary>
    public Patients() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Patients"/> class.
    /// </summary>
    /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
    public Patients(IList<Patient> initialList) : base(initialList) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Patients"/> class.
    /// </summary>
    /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
    public Patients(CollectionBase<Patient> initialList) : base(initialList) { }

    /// <summary>
    /// Validates the current collection by validating each individual item in the collection.
    /// </summary>
    /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
    public IEnumerable<ValidationResult> Validate()
    {
      var errors = new List<ValidationResult>();
      foreach (var patient in this)
      {
        errors.AddRange(patient.Validate());
      }
      return errors;
    }
  }
    
}
