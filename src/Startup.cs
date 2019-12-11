using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace OdataExample {

  public class Startup {

    public void ConfigureServices (IServiceCollection services) {
      services.AddMvcCore (x=>x.EnableEndpointRouting= false)
        .AddControllersAsServices ()
        .AddJsonFormatters ()
        .SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

      services.AddDbContext<Store> (options => {
        // var connection = new Microsoft.Data.Sqlite.SqliteConnection ("DataSource=:memory:");
        // options.UseSqlite (connection);
        options.UseInMemoryDatabase("Example");
        options.EnableSensitiveDataLogging ();
      });

services.AddApiVersioning(options => {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = false;
      });
      services.AddOData ()
        .EnableApiVersioning ();

      services.AddODataApiExplorer (
        options => {
          options.GroupNameFormat = "'v'VVV";
          options.SubstituteApiVersionInUrl = true;

        });
    }

    public void Configure (IApplicationBuilder app, IHostingEnvironment env, VersionedODataModelBuilder modelBuilder) {
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory> ().CreateScope ()) {
        using (var context = serviceScope.ServiceProvider.GetRequiredService<Store> ()) {
          context.Database.EnsureCreated ();
          SeedDb (context);
        }
      }
      app.UseMvc (x => {
        var model = modelBuilder.GetEdmModels ();
        x.MapVersionedODataRoutes ("odata", "odata", model);
        x.MaxTop (500).Expand ().Select ().Count ().OrderBy ().Filter ();
      });
    }
    public void SeedDb (Store context) {
      var precType = new InstrumentType {
        Id = 1,
        Name = "Precious",
        InstrumentCode = "PREC"
      };
      context.InstrumentTypes.Add (precType);
      var ingtType = new InstrumentType {
        Id = 2,
        Name = "Bullion",
        InstrumentCode = "INGT"
      };
      context.InstrumentTypes.Add (ingtType);
      
      var precInstr = new Precious {
        Id = 1,
        InstrumentType = precType,
        Name = "XAU"
      };
      context.Precious.Add (precInstr);
      var bulInstr = new Bullion {
        Id = 2,
          InstrumentType = ingtType,
          Name = "XAU 51572-2000",
          LigatureMass = 1000,
          BaseInstrument = precInstr
      };
      context.Bullions.Add (bulInstr);
      context.Payments.Add(new PaymentApiModel{
        Id = 1,
        Amount = 5000,
        Date = DateTime.Now,
        Instrument = precInstr
      });
      context.Payments.Add(new PaymentApiModel{
        Id = 2,
        Amount = 300,
        Date = DateTime.Now,
        Instrument = bulInstr
      });
      context.SaveChanges ();
    }
  }
}