using PTSL.Ovidhan.Common.Entity.CommonEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PTSL.Ovidhan.Common.Entity
{
    public static class EntityExtension
    {
        public static bool PersistAuditProperties<TEntity>(this TEntity receivedEntity, ref TEntity retrievedEntity)
            where TEntity : BaseEntity
        {
            bool success = true;

            try
            {
                // TODO: debug the running code to determine if we should call Trim on the received entity and on the retrieved entity (to guarantee we do not update a field due to trailing spaces)
                TEntity receivedTrimmedEntity = receivedEntity.Trim<TEntity>();
                TEntity retrievedTrimmedEntity = retrievedEntity.Trim<TEntity>();

                ////List<string> tenantServiceEntityFields = typeof (MultiTenantServiceEntity<TPk, TTenant, TCustomer, TService>)
                ////    .GetProperties (BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                ////        .Select (p => p.Name)
                ////            .ToList ();

                ////PropertyInfo[] tenantServiceEntityFields =
                ////    typeof (MultiTenantServiceEntity<TPk, TTenant, TCustomer, TService>)
                ////        .GetProperties (BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

                // Take all the properties present in MultiTenantServiceEntity, and ignore these when iterating over fie properties of the given Entity
                PropertyInfo[] tenantServiceEntityProperties =
                    typeof(BaseEntity)
                        .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);


                PropertyInfo[] receivedEntityProperties =
                    typeof(TEntity)
                        .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);

                // Iterate over all properties of the Entity
                foreach (PropertyInfo property in receivedEntityProperties)
                {
                    // Ignore all the fields present in MultiTenantEntity, because these ones we will not match and update
                    ////if (! tenantServiceEntityFields.Contains (property.Name))
                    if (tenantServiceEntityProperties.All(p => p.Name != property.Name))
                    {
                        object? receivedEntityValue = property.GetValue(receivedTrimmedEntity);
                        object? retrievedEntityValue = property.GetValue(retrievedTrimmedEntity);

                        // Only update the value of the field of retrievedEntity if the value of receivedEntity has been updated
                        if (receivedEntityValue != null && !receivedEntityValue.Equals(retrievedEntityValue))
                        {
                            //TODO: verify if this is correct: we must assign the value of receivedEntity to retrievedTrimmedEntity
                            property.SetValue(retrievedTrimmedEntity, receivedEntityValue);
                        }
                        //else
                        //{
                        //    success = false;
                        //    break;
                        //}
                    }
                }

                foreach (PropertyInfo property in tenantServiceEntityProperties)
                {


                    // Ignore all the fields present in MultiTenantEntity, because these ones we will not match and update
                    ////if (! tenantServiceEntityFields.Contains (property.Name))
                    //if (tenantServiceEntityProperties.All(p => p.Name == "IsDeleted"))
                    //{
                    object? receivedEntityValue = property.GetValue(receivedTrimmedEntity); //Updated value form viewmodel
                    object? retrievedEntityValue = property.GetValue(retrievedTrimmedEntity); //Existing records from database

                    // Only update the value of the field of retrievedEntity if the value of receivedEntity has been updated
                    //if ((receivedEntityValue != null && !receivedEntityValue.Equals(0)) && !receivedEntityValue.Equals(retrievedEntityValue))
                    if (receivedEntityValue != null && !receivedEntityValue.Equals(retrievedEntityValue))
                    {
                        string columnValue = receivedEntityValue.ToString();
                        if (columnValue != "0")
                        {
                            //TODO: verify if this is correct: we must assign the value of receivedEntity to retrievedTrimmedEntity
                            property.SetValue(retrievedTrimmedEntity, receivedEntityValue);
                        }
                    }
                    //else
                    //{
                    //    success = false;
                    //    break;
                    //}
                    //}
                }

                //foreach (PropertyInfo property in tenantServiceEntityProperties.Where(x=>x.Name == "IsDeleted"))
                //{


                //    // Ignore all the fields present in MultiTenantEntity, because these ones we will not match and update
                //    ////if (! tenantServiceEntityFields.Contains (property.Name))
                //    //if (tenantServiceEntityProperties.All(p => p.Name == "IsDeleted"))
                //    //{
                //        object? receivedEntityValue = property.GetValue(receivedTrimmedEntity);
                //        object? retrievedEntityValue = property.GetValue(retrievedTrimmedEntity);

                //        // Only update the value of the field of retrievedEntity if the value of receivedEntity has been updated
                //        if (receivedEntityValue != null && !receivedEntityValue.Equals(retrievedEntityValue))
                //        {
                //            //TODO: verify if this is correct: we must assign the value of receivedEntity to retrievedTrimmedEntity
                //            property.SetValue(retrievedTrimmedEntity, receivedEntityValue);
                //        }
                //        //else
                //        //{
                //        //    success = false;
                //        //    break;
                //        //}
                //    //}
                //}

            }
            catch (System.Exception ex)
            {
                success = false;
            }

            return success;
        }

        public static TEntity Trim<TEntity>(this TEntity entity)
            where TEntity : BaseEntity
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string) && property.CanWrite && property.CanRead)
                {
                    string value = (string)typeof(TEntity).InvokeMember(property.Name, BindingFlags.GetProperty, null, entity, null, CultureInfo.CurrentCulture);

                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        string trimmedValue = value.Trim();

                        property.SetValue(entity, trimmedValue.Length == 0 ? null : trimmedValue);
                    }
                    else
                    {
                        property.SetValue(entity, "");
                    }
                }
            }

            return entity;
        }

        public static IList<TEntity> Trim<TEntity>(this IList<TEntity> entities)
            where TEntity : BaseEntity
        {
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                foreach (TEntity entity in entities)
                {
                    if (property.PropertyType == typeof(string) && property.CanWrite && property.CanRead)
                    {
                        string value = (string)typeof(TEntity).InvokeMember(property.Name, BindingFlags.GetProperty, null, entity, null, CultureInfo.CurrentCulture);

                        if (!string.IsNullOrWhiteSpace(value))
                        {
                            string trimmedValue = value.Trim();

                            property.SetValue(entity, trimmedValue.Length == 0 ? null : trimmedValue);
                        }
                        else
                        {
                            property.SetValue(entity, "");
                        }
                    }
                }
            }

            return entities;
        }
    }
}
