﻿using AppV7.Server.Models;
using AppV7.Shared;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AppV7.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        public DbSet<Z100_Org> Organizaciones { get; set; }
        public DbSet<Z110_Usuarios> Usuarios { get; set; }
        public DbSet<Z190_Bitacora> Bitacoras { get; set; }
        public DbSet<Z195_MailUs> MailUs { get; set; }
        public DbSet<Z202_Contacto> Contactos { get; set; }
        public DbSet<Z204_ContDet> ContactoDet { get; set; }
        public DbSet<Z205_DatosTipo> DatosTipo { get; set; }
        public DbSet<Z800_WebSite> WebSite { get; set; }
        public DbSet<Z840_Contactanos> Contactanos { get; set; }


    }
}