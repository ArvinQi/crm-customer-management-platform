﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRM.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CRMEntities : DbContext
    {
        public CRMEntities()
            : base("name=CRMEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tb_ClientCallback> tb_ClientCallback { get; set; }
        public DbSet<tb_ClientCare> tb_ClientCare { get; set; }
        public DbSet<tb_ClientComplaint> tb_ClientComplaint { get; set; }
        public DbSet<tb_ClientContactRecord> tb_ClientContactRecord { get; set; }
        public DbSet<tb_Clients> tb_Clients { get; set; }
        public DbSet<tb_ClientSurvey> tb_ClientSurvey { get; set; }
        public DbSet<tb_Demands> tb_Demands { get; set; }
        public DbSet<tb_Departs> tb_Departs { get; set; }
        public DbSet<tb_Log> tb_Log { get; set; }
        public DbSet<tb_Menus_Left> tb_Menus_Left { get; set; }
        public DbSet<tb_Products> tb_Products { get; set; }
        public DbSet<tb_SalesAgreement> tb_SalesAgreement { get; set; }
        public DbSet<tb_SalesChance> tb_SalesChance { get; set; }
        public DbSet<tb_SalesPlan> tb_SalesPlan { get; set; }
        public DbSet<tb_SalesPrice> tb_SalesPrice { get; set; }
        public DbSet<tb_SalesRecord> tb_SalesRecord { get; set; }
        public DbSet<tb_ServiceRecord> tb_ServiceRecord { get; set; }
        public DbSet<tb_Suppliers> tb_Suppliers { get; set; }
        public DbSet<tb_UserType> tb_UserType { get; set; }
        public DbSet<tb_Duties> tb_Duties { get; set; }
        public DbSet<tb_Users> tb_Users { get; set; }
        public DbSet<tb_Roles> tb_Roles { get; set; }
    }
}
