﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain.Entitites
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyBudget : DbContext
    {
        public MyBudget()
            : base("name=MyBudget")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<expenseDestination> expenseDestinations { get; set; }
        public DbSet<paymentSource> paymentSources { get; set; }
        public DbSet<recurrence> recurrences { get; set; }
        public DbSet<activity> activities { get; set; }
        public DbSet<bill> bills { get; set; }
    }
}
