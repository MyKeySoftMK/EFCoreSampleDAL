using EFDesigner2022Tests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;



public static class DbContextExtensions
{
    public static void UpdateEntity<TEntity>(this DbContext context, TEntity existingEntity, TEntity updatedEntity, string IdColumnName = "Id") where TEntity : class
    {
        // Aktualisiere einfache Eigenschaften
        context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);

        // Überprüfe Navigationseigenschaften und aktualisiere sie
        foreach (var navigationExistingEntry in context.Entry(existingEntity).Navigations)
        {
            if (navigationExistingEntry.IsLoaded)
            {

                foreach (var navigationUpdatedEntry in context.Entry(updatedEntity).Navigations)
                {
                    if (navigationUpdatedEntry.Metadata.Name == navigationExistingEntry.Metadata.Name)
                    { 
                        
                        var updatedNavigation = navigationUpdatedEntry.CurrentValue;
                        if (updatedNavigation == null)
                        {
                            // Wenn die aktualisierte Navigationseigenschaft null ist, setzen Sie die vorhandene auf null
                            navigationExistingEntry.CurrentValue = null;
                        }
                        else
                        {
                            // Überprüfe, ob die Navigationseigenschaft eine Sammlung ist
                            if (navigationExistingEntry.Metadata.IsCollection)
                            {
                                Type collectionType = navigationExistingEntry.CurrentValue.GetType().GetInterfaces()
                                        .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(ICollection<>));

                                // Erhalte den generischen Typargument
                                Type genericType = collectionType.GetGenericArguments().FirstOrDefault();

                                // Konstruiere die entsprechende ICollection mit dem generischen Typargument
                                Type constructedCollectionType = typeof(ICollection<>).MakeGenericType(genericType);


                                var existingCollection = navigationExistingEntry.CurrentValue as ICollection<GeneralEntity>;
                                var updatedCollection = updatedNavigation as ICollection<GeneralEntity>;

                                if (existingCollection != null && updatedCollection != null)
                                {
                                    // Entferne Untereinträge, die nicht mehr vorhanden sind
                                    foreach (var existingItem in existingCollection.ToList())
                                    {
                                        if (!updatedCollection.Any(u => context.Entry(u).Property(IdColumnName).CurrentValue.Equals(context.Entry(existingItem).Property(IdColumnName).CurrentValue)))
                                        {
                                            existingCollection.Remove(existingItem);
                                        }
                                    }

                                    // Füge neue Untereinträge hinzu
                                    foreach (var updatedItem in updatedCollection)
                                    {
                                        var existingItem = existingCollection.SingleOrDefault(e => context.Entry(e).Property(IdColumnName).CurrentValue.Equals(context.Entry(updatedItem).Property(IdColumnName).CurrentValue));
                                        if (existingItem != null)
                                        {
                                            // Wenn vorhanden, aktualisiere den bestehenden Eintrag
                                            context.Entry(existingItem).CurrentValues.SetValues(updatedItem);
                                        }
                                        else
                                        {
                                            // Wenn nicht vorhanden, füge den neuen Eintrag hinzu
                                            existingCollection.Add(updatedItem);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Wenn es keine Sammlung ist, aktualisiere die Navigationseigenschaft direkt
                                context.UpdateEntity(navigationExistingEntry.CurrentValue, updatedNavigation);
                            }
                        }

                       
                    }

                }


            }
        }
    }

}
