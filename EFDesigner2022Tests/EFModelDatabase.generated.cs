//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
//
//     Produced by Entity Framework Visual Editor v4.2.8.1
//     Source:                    https://github.com/msawczyn/EFDesigner
//     Visual Studio Marketplace: https://marketplace.visualstudio.com/items?itemName=michaelsawczyn.EFDesigner
//     Documentation:             https://msawczyn.github.io/EFDesigner/
//     License (MIT):             https://github.com/msawczyn/EFDesigner/blob/master/LICENSE
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFDesigner2022Tests
{
   /// <inheritdoc/>
   public partial class EFModelDatabase : DbContext
   {
      #region DbSets
      public virtual Microsoft.EntityFrameworkCore.DbSet<global::EFDesigner2022Tests.EntityChild> EntityChild { get; set; }
      public virtual Microsoft.EntityFrameworkCore.DbSet<global::EFDesigner2022Tests.EntityParent> EntityParent { get; set; }
      public virtual Microsoft.EntityFrameworkCore.DbSet<global::EFDesigner2022Tests.EntityReference> EntityReference { get; set; }

      #endregion DbSets

      /// <summary>
      /// Default connection string
      /// </summary>
      public static string ConnectionString { get; set; } = @"Data Source=192.168.2.251;Initial Catalog=DEVDatabase;Persist Security Info=True;User ID=sa;Password=SuperSQL2019;TrustServerCertificate=True";

      /// <inheritdoc />
      public EFModelDatabase() : base()
      {
      }

      /// <summary>
      ///     <para>
      ///         Initializes a new instance of the <see cref="T:Microsoft.EntityFrameworkCore.DbContext" /> class using the specified options.
      ///         The <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" /> method will still be called to allow further
      ///         configuration of the options.
      ///     </para>
      /// </summary>
      /// <param name="options">The options for this context.</param>
      public EFModelDatabase(DbContextOptions<EFModelDatabase> options) : base(options)
      {
      }

      partial void CustomInit(DbContextOptionsBuilder optionsBuilder);

      partial void OnModelCreatingImpl(ModelBuilder modelBuilder);
      partial void OnModelCreatedImpl(ModelBuilder modelBuilder);

      /// <summary>
      ///     Override this method to further configure the model that was discovered by convention from the entity types
      ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
      ///     and re-used for subsequent instances of your derived context.
      /// </summary>
      /// <remarks>
      ///     If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
      ///     then this method will not be run.
      /// </remarks>
      /// <param name="modelBuilder">
      ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
      ///     define extension methods on this object that allow you to configure aspects of the model that are specific
      ///     to a given database.
      /// </param>
      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         base.OnModelCreating(modelBuilder);
         OnModelCreatingImpl(modelBuilder);


         modelBuilder.Entity<global::EFDesigner2022Tests.EntityChild>()
                     .ToTable("EntityChild", "dbo")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityChild>()
                     .Property(t => t.Id)
                     .ValueGeneratedOnAdd()
                     .IsRequired();

         modelBuilder.Entity<global::EFDesigner2022Tests.EntityParent>()
                     .ToTable("EntityParent", "dbo")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityParent>()
                     .Property(t => t.Id)
                     .ValueGeneratedOnAdd()
                     .IsRequired();
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityParent>()
                     .HasMany<global::EFDesigner2022Tests.EntityChild>(p => p.Childs)
                     .WithMany(p => p.ParentChild)
                     .UsingEntity<Dictionary<string, object>>(right => right.HasOne<global::EFDesigner2022Tests.EntityChild>().WithMany().HasForeignKey("EntityChild_Id").OnDelete(DeleteBehavior.Cascade),left => left.HasOne<global::EFDesigner2022Tests.EntityParent>().WithMany().HasForeignKey("EntityParent_Id").OnDelete(DeleteBehavior.Cascade),join => join.ToTable("ParentToChilds"));
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityParent>().Navigation(e => e.Childs).AutoInclude();

         modelBuilder.Entity<global::EFDesigner2022Tests.EntityReference>()
                     .ToTable("EntityReference", "dbo")
                     .HasKey(t => t.Id);
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityReference>()
                     .Property(t => t.Id)
                     .ValueGeneratedOnAdd()
                     .IsRequired();
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityReference>()
                     .HasMany<global::EFDesigner2022Tests.EntityParent>(p => p.ParentRef)
                     .WithMany(p => p.References)
                     .UsingEntity<Dictionary<string, object>>(right => right.HasOne<global::EFDesigner2022Tests.EntityParent>().WithMany().HasForeignKey("EntityParent_Id").OnDelete(DeleteBehavior.Cascade),left => left.HasOne<global::EFDesigner2022Tests.EntityReference>().WithMany().HasForeignKey("EntityReference_Id").OnDelete(DeleteBehavior.Cascade),join => join.ToTable("RefToParent"));
         modelBuilder.Entity<global::EFDesigner2022Tests.EntityReference>().Navigation(e => e.ParentRef).AutoInclude();

         OnModelCreatedImpl(modelBuilder);
      }
   }
}
