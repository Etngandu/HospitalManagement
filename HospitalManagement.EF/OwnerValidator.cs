using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalManagement.Entities;

namespace HospitalManagement.EF
{
  public  class OwnerValidator
    {
        private static readonly Dictionary<Type, string> _parentAttributes = new Dictionary<Type, string>();

        public static void ValidateEntity(DbContext context, DbEntityEntry entity, Type type)
        {
            if (entity.State == EntityState.Modified)
            {
                if (!_parentAttributes.ContainsKey(type))
                {
                    var properties = from attributedProperty in type.GetProperties()
                                     select new
                                     {
                                         attributedProperty,
                                         attributes = attributedProperty.GetCustomAttributes(true)
                                             .Where(attribute => attribute is OwnerAttribute)
                                     };
                    properties = properties.Where(p => p.attributes.Any());
                    _parentAttributes.Add(type,
                                          properties.Any()
                                              ? properties.First().attributedProperty.Name
                                              : string.Empty);
                }

                if (!string.IsNullOrEmpty(_parentAttributes[type]))
                {
                    if (entity.Reference(_parentAttributes[type]).CurrentValue == null)
                    {
                        context.Set(type).Remove(entity.Entity);
                    }
                }
            }
        }
    }
}
