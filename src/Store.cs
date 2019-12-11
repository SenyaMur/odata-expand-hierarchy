using Microsoft.EntityFrameworkCore;

namespace OdataExample {

    public class Store : DbContext {
        public Store (DbContextOptions<Store> options) : base (options) { }
        public DbSet<InstrumentType> InstrumentTypes { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Precious> Precious { get; set; }
        public DbSet<Bullion> Bullions { get; set; }
        public DbSet<PaymentApiModel> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            builder.Entity<Instrument> (entity => {
                entity.HasDiscriminator<string> ("INSTRUMENTTYPE_CODE")
                    .HasValue<Precious> ("PREC")
                    .HasValue<Bullion> ("INGT");

                entity.HasOne (x => x.InstrumentType)
                    .WithMany ()
                    .HasForeignKey ("INSTRUMENTTYPE_CODE");
            });

            builder.Entity<InstrumentType> (entity => {
                entity.Property (x => x.InstrumentCode)
                    .HasColumnName ("INSTRUMENTTYPE_CODE")
                    .HasMaxLength (4)
                    .IsFixedLength ()
                    .IsRequired ();
                entity.HasKey (x => x.InstrumentCode);
            });
        }
    }
}