﻿    //------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaterialUI.DateBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GymDBEntities2 : DbContext
    {
        public GymDBEntities2()
            : base("name=GymDBEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Абонемент> Абонемент { get; set; }
        public virtual DbSet<К_Карта> К_Карта { get; set; }
        public virtual DbSet<Клиент> Клиент { get; set; }
        public virtual DbSet<Пол> Пол { get; set; }
        public virtual DbSet<Тренер> Тренер { get; set; }
        public virtual DbSet<Услуга> Услуга { get; set; }
    }
}
