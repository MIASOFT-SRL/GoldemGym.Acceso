﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoldemGym.Acceso.Datos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbControlAccesoEntities : DbContext
    {
        public dbControlAccesoEntities()
            : base("name=dbControlAccesoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ACGroup> ACGroup { get; set; }
        public DbSet<ACTimeZones> ACTimeZones { get; set; }
        public DbSet<ACUnlockComb> ACUnlockComb { get; set; }
        public DbSet<AlarmLog> AlarmLog { get; set; }
        public DbSet<AttParam> AttParam { get; set; }
        public DbSet<AuditedExc> AuditedExc { get; set; }
        public DbSet<AUTHDEVICE> AUTHDEVICE { get; set; }
        public DbSet<CHECKEXACT> CHECKEXACT { get; set; }
        public DbSet<CHECKINOUT> CHECKINOUT { get; set; }
        public DbSet<DEPARTMENTS> DEPARTMENTS { get; set; }
        public DbSet<DeptUsedSchs> DeptUsedSchs { get; set; }
        public DbSet<EmOpLog> EmOpLog { get; set; }
        public DbSet<FaceTemp> FaceTemp { get; set; }
        public DbSet<HOLIDAYS> HOLIDAYS { get; set; }
        public DbSet<LeaveClass> LeaveClass { get; set; }
        public DbSet<LeaveClass1> LeaveClass1 { get; set; }
        public DbSet<Machines> Machines { get; set; }
        public DbSet<NUM_RUN> NUM_RUN { get; set; }
        public DbSet<NUM_RUN_DEIL> NUM_RUN_DEIL { get; set; }
        public DbSet<ReportItem> ReportItem { get; set; }
        public DbSet<SchClass> SchClass { get; set; }
        public DbSet<SECURITYDETAILS> SECURITYDETAILS { get; set; }
        public DbSet<ServerLog> ServerLog { get; set; }
        public DbSet<SHIFT> SHIFT { get; set; }
        public DbSet<SystemLog> SystemLog { get; set; }
        public DbSet<TBSMSALLOT> TBSMSALLOT { get; set; }
        public DbSet<TBSMSINFO> TBSMSINFO { get; set; }
        public DbSet<TEMPLATE> TEMPLATE { get; set; }
        public DbSet<USER_OF_RUN> USER_OF_RUN { get; set; }
        public DbSet<USER_SPEDAY> USER_SPEDAY { get; set; }
        public DbSet<USER_TEMP_SCH> USER_TEMP_SCH { get; set; }
        public DbSet<UserACMachines> UserACMachines { get; set; }
        public DbSet<UserACPrivilege> UserACPrivilege { get; set; }
        public DbSet<USERINFO> USERINFO { get; set; }
        public DbSet<UsersMachines> UsersMachines { get; set; }
        public DbSet<UserUpdates> UserUpdates { get; set; }
        public DbSet<UserUsedSClasses> UserUsedSClasses { get; set; }
    }
}
