﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BodyBalance
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntitiesAIOP : DbContext
    {
        public EntitiesAIOP()
            : base("name=EntitiesAIOP")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACCESSORY> ACCESSORY { get; set; }
        public virtual DbSet<ACTIVITY> ACTIVITY { get; set; }
        public virtual DbSet<ADMIN> ADMIN { get; set; }
        public virtual DbSet<BIN_9MwBG5eHTXbgRAADuhzxdw___0> BIN_9MwBG5eHTXbgRAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_9PgaA_DDNVDgRAADuhzxdw___0> BIN_9PgaA_DDNVDgRAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_9PgaA_DGNVDgRAADuhzxdw___0> BIN_9PgaA_DGNVDgRAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_9PgaA_DJNVDgRAADuhzxdw___0> BIN_9PgaA_DJNVDgRAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_CCD2eL7dchngVAADuhzxdw___0> BIN_CCD2eL7dchngVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_CCD2eL7gchngVAADuhzxdw___0> BIN_CCD2eL7gchngVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_v3U2XgVAADuhzxdw___0> BIN_D6_pY_v3U2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_v6U2XgVAADuhzxdw___0> BIN_D6_pY_v6U2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_w_U2XgVAADuhzxdw___0> BIN_D6_pY_w_U2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_w4U2XgVAADuhzxdw___0> BIN_D6_pY_w4U2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_w7U2XgVAADuhzxdw___0> BIN_D6_pY_w7U2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wbU2XgVAADuhzxdw___0> BIN_D6_pY_wbU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wCU2XgVAADuhzxdw___0> BIN_D6_pY_wCU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_weU2XgVAADuhzxdw___0> BIN_D6_pY_weU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_whU2XgVAADuhzxdw___0> BIN_D6_pY_whU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wHU2XgVAADuhzxdw___01> BIN_D6_pY_wHU2XgVAADuhzxdw___01 { get; set; }
        public virtual DbSet<BIN_D6_pY_wlU2XgVAADuhzxdw___0> BIN_D6_pY_wlU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wMU2XgVAADuhzxdw___0> BIN_D6_pY_wMU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wpU2XgVAADuhzxdw___0> BIN_D6_pY_wpU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wUU2XgVAADuhzxdw___0> BIN_D6_pY_wUU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D6_pY_wYU2XgVAADuhzxdw___0> BIN_D6_pY_wYU2XgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_D7BSGM51VaHgVAADuhzxdw___0> BIN_D7BSGM51VaHgVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_EaaDNAXOUs7gVAADuhzxdw___0> BIN_EaaDNAXOUs7gVAADuhzxdw___0 { get; set; }
        public virtual DbSet<BIN_Eiu5KedaBhXgVAADuhzxdw___0> BIN_Eiu5KedaBhXgVAADuhzxdw___0 { get; set; }
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
        public virtual DbSet<USER1> USER1 { get; set; }
    }
}