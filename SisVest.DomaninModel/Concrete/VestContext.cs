using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;
using System.Data.Entity;
using SisVest.DomaninModel.Entities;

namespace SisVest.DomaninModel.Concrete
{
    public class VestContext : DbContext
    {
        /// <summary>
        /// Referencia direta a chave
        /// </summary>
        public VestContext()
            : base("name=InstanciaLocal")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<VestContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Candidato> Candidatos { get; set; }

        public DbSet<Curso> Cursos { get; set; }

        public DbSet<Vestibular> Vestibulares { get; set; }

    }
}