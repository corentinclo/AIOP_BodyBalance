﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BodyBalance.Persistence
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACCESSORY> ACCESSORY { get; set; }
        public virtual DbSet<ACTIVITY> ACTIVITY { get; set; }
        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<CONTAINSACCESSORY> CONTAINSACCESSORY { get; set; }
        public virtual DbSet<CONTRIBUTOR> CONTRIBUTOR { get; set; }
        public virtual DbSet<EVENT> EVENT { get; set; }
        public virtual DbSet<EVENTTYPE> EVENTTYPE { get; set; }
        public virtual DbSet<HASINBASKET> HASINBASKET { get; set; }
        public virtual DbSet<MANAGER> MANAGER { get; set; }
        public virtual DbSet<MEMBER> MEMBER { get; set; }
        public virtual DbSet<NOTIFICATION> NOTIFICATION { get; set; }
        public virtual DbSet<PRICE> PRICE { get; set; }
        public virtual DbSet<PRODUCT> PRODUCT { get; set; }
        public virtual DbSet<PUNCTUAL_EVENT> PUNCTUAL_EVENT { get; set; }
        public virtual DbSet<PURCHASE> PURCHASE { get; set; }
        public virtual DbSet<PURCHASECONTAINS> PURCHASECONTAINS { get; set; }
        public virtual DbSet<REPETITIVE_EVENT> REPETITIVE_EVENT { get; set; }
        public virtual DbSet<ROOM> ROOM { get; set; }
        public virtual DbSet<TOKEN> TOKEN { get; set; }
        public virtual DbSet<USER1> USER1 { get; set; }
    }
}
