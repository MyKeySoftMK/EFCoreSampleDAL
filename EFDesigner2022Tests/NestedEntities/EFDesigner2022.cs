using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using EFCore.BulkExtensions;

namespace EFDesigner2022Tests
{
    [TestClass]
    public class EFDesigner2022
    {
        private DbContextOptionsBuilder<EFModelDatabase> DbOptionBuilder;

        private void Initialize(string ConnString = "")
        {
            try
            {

                // Einstellungen lesen
            DbOptionBuilder = new DbContextOptionsBuilder<EFModelDatabase>();

                // Austauschen der Datenbankverbindung
                if (ConnString.Length > 0)
                {
                    EFModelDatabase.ConnectionString = ConnString;

                }

                EFModelDatabase.ConfigureOptions(DbOptionBuilder);

            }
            catch (Exception ex)
            {

            }
        }

        private void CreateParent(EntityParent ParentEntity)
        {
            using (EFModelDatabase _DbContext = new EFModelDatabase(DbOptionBuilder.Options))
            {

                try
                {
                    _DbContext.Add(ParentEntity);
                    _DbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Exception _exception = ex;
                }
            }
        }


        public EntityParent GetEntityParent(long EntityParentId)
        {
            EntityParent serverEntityParent = null;

            using (EFModelDatabase _DbContext = new EFModelDatabase(DbOptionBuilder.Options))
            {

                try
                {
                    serverEntityParent = _DbContext.EntityParent.Single(EntityParent => EntityParent.Id == EntityParentId);
                }
                catch (Exception ex)
                {
                    Exception _exception = ex;
                }

                return serverEntityParent;
            }
        }

        public EntityParent ModifyEntityParent(EntityParent EntityParentEntity)
        {
            EntityParent _result = null;
            EntityParent serverEntityParent;
            List<EntityParent> EntityParentList = new List<EntityParent>();


            using (EFModelDatabase _DbContext = new EFModelDatabase(DbOptionBuilder.Options))
            {
                try
                {
                    EntityParentList.Add(EntityParentEntity);
                    serverEntityParent = _DbContext.EntityParent.
                        Include(parenteEntity => parenteEntity.Childs).
                        Single(EntityParent => EntityParent.Id == EntityParentEntity.Id);


                    // Anpassen der Hauptebene
                    _DbContext.Entry(serverEntityParent).CurrentValues.SetValues(EntityParentEntity);


                    // Temporäres Entfernen der Collections der Navigationseigenschaften
                    serverEntityParent.Childs.Clear();
                    _DbContext.SaveChanges();

                    // Die Collections erneut hinzufügen und speichern
                    foreach (var ChildEntity in EntityParentEntity.Childs)
                    {
                        serverEntityParent.Childs.Add(ChildEntity);
                        _DbContext.SaveChanges();
                    }


                }
                catch (Exception ex)
                {
                    Exception _exception = ex;
                }

            }

            return GetEntityParent(EntityParentEntity.Id);
        }

        [TestMethod]
        public void ModifyChildEntities()
        {
            this.Initialize();

            EntityParent serverParent = null;
 
            // Create Parent with two Child entities
            EntityParent entityParent = new EntityParent();
            EntityChild entityChild1 = new EntityChild();
            EntityChild entityChild2 = new EntityChild();
            EntityChild entityChild3 = new EntityChild();

            entityParent.NameParent = "ParentName";

            entityChild1.NameChild = "FirstChildName";
            entityChild2.NameChild = "SecondChildName";
            entityChild3.NameChild = "ThirdChildName";

            entityParent.Childs.Add(entityChild1);
            entityParent.Childs.Add(@entityChild2);

            // Check if Parent has 2 Childs
            Assert.AreEqual(2, entityParent.Childs.Count());

            // Write Parent to Database
            CreateParent(entityParent);

            // Read Parent fresh from Database
            serverParent = GetEntityParent(entityParent.Id);

            // Check if Parent on Database have 2 Childs
            Assert.AreEqual(2, serverParent.Childs.Count());

            // Add a new Child to Database Result
            serverParent.Childs.Add(entityChild3);
            serverParent.NameParent = "ParentName Changed";
            // Check if Parent now have 3 Childs
            Assert.AreEqual(3, serverParent.Childs.Count());

            // Write Changes to DB
            ModifyEntityParent(serverParent);

            // Read Parent fresh from Database
            serverParent = GetEntityParent(entityParent.Id);
            Assert.AreEqual("ParentName Changed", serverParent.NameParent);
            Assert.AreEqual(3,serverParent.Childs.Count());
        }

        [TestMethod]
        public void ModifyReferenceEntities()
        {
            this.Initialize();

            EntityParent serverParent = null;

            // Create Parent with two Child entities
            EntityParent entityParent = new EntityParent();
            EntityReference entityReference1 = new EntityReference();
            EntityReference entityReference2 = new EntityReference();
            EntityReference entityReference3 = new EntityReference();

            entityParent.NameParent = "ParentName";

            entityReference1.NameReference = "FirstReferenceName";
            entityReference2.NameReference = "SecondReferenceName";
            entityReference3.NameReference = "ThirdReferenceName";

            entityParent.References.Add(entityReference1);
            entityParent.References.Add(@entityReference2);

            // Check if Parent has 2 Childs
            Assert.AreEqual(entityParent.References.Count(), 2);

            // Write Parent to Database
            CreateParent(entityParent);

            // Read Parent fresh from Database
            serverParent = GetEntityParent(entityParent.Id);

            // Check if Parent on Database have 2 Childs
            Assert.AreEqual(serverParent.References.Count(), 2);

            // Add a new Child to Database Result
            serverParent.References.Add(entityReference3);
            serverParent.NameParent = "ParentName Changed";
            // Check if Parent now have 3 Childs
            Assert.AreEqual(serverParent.References.Count(), 3);

            // Write Changes to DB
            ModifyEntityParent(serverParent);

            // Read Parent fresh from Database
            serverParent = GetEntityParent(entityParent.Id);
            Assert.AreEqual(serverParent.NameParent, "ParentName Changed");
            Assert.AreEqual(serverParent.References.Count(), 3);
        }

        [TestMethod]
        public void ModifyChildReferecneEntities()
        {
            this.Initialize();

            EntityParent serverParent = null;

            // Create Parent with two Child entities
            EntityParent entityParent = new EntityParent();
            EntityChild entityChild1 = new EntityChild();
            EntityChild entityChild2 = new EntityChild();
            EntityChild entityChild3 = new EntityChild();
            EntityReference entityReference1 = new EntityReference();
            EntityReference entityReference2 = new EntityReference();
            EntityReference entityReference3 = new EntityReference();

            entityParent.NameParent = "ParentName";

            entityChild1.NameChild = "FirstChildName";
            entityChild2.NameChild = "SecondChildName";
            entityChild3.NameChild = "ThirdChildName";

            entityReference1.NameReference = "FirstReferenceName";
            entityReference2.NameReference = "SecondReferenceName";
            entityReference3.NameReference = "ThirdReferenceName";

            entityParent.Childs.Add(entityChild1);
            entityParent.Childs.Add(@entityChild2);

            entityParent.References.Add(entityReference1);
            entityParent.References.Add(@entityReference2);

            // Check if Parent has 2 Childs
            Assert.AreEqual(entityParent.Childs.Count(), 2);
            Assert.AreEqual(entityParent.References.Count(), 2);

            // Write Parent to Database
            CreateParent(entityParent);

            // Read Parent fresh from Database
            serverParent = GetEntityParent(entityParent.Id);

            // Check if Parent on Database have 2 Childs
            Assert.AreEqual(serverParent.Childs.Count(), 2);
            Assert.AreEqual(serverParent.References.Count(), 2);

            // Add a new Child to Database Result
            serverParent.NameParent = "ParentName Changed";
            serverParent.Childs.Add(entityChild3);
            serverParent.References.Add(entityReference3);
            // Check if Parent now have 3 Childs
            Assert.AreEqual(serverParent.Childs.Count(), 3);
            Assert.AreEqual(serverParent.References.Count(), 3);

            // Write Changes to DB
            ModifyEntityParent(serverParent);

            // Read Parent fresh from Database
            serverParent = GetEntityParent(entityParent.Id);
            Assert.AreEqual(serverParent.NameParent, "ParentName Changed");
            Assert.AreEqual(serverParent.Childs.Count(), 3);
            Assert.AreEqual(serverParent.References.Count(), 3);
        }
    }

}