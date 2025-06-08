using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Avocado> Avocados { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data with static CreatedAt values
            var staticDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<Avocado>().HasData(
                new Avocado
                {
                    Id = 1,
                    Name = "Hass Avokado",
                    Description = "Yüksek yağ içeriği ve kremsi dokusu ile bilinen, en popüler avokado çeşidi.",
                    Price = 29.99M,
                    ImageUrl = "/images/hass.jpg",
                    Type = "Hass",
                    LongDescription = "Hass avokadosu, koyu yeşilden siyaha dönen pürüzlü kabuğu ile kolayca tanınır. Eti yüksek yağ içeriği nedeniyle son derece kremsi ve lezzetlidir. Olgunlaştıkça kabuk rengi koyulaşır ve ete hafif baskı uygulandığında yumuşaksa, yemeye hazır demektir. Hass, tüm dünyada en yaygın yetiştiriciliği yapılan avokado çeşididir ve özellikle guacamole yapmak için mükemmeldir.",
                    CreatedAt = staticDate,
                    IsAvailable = true
                },
                new Avocado
                {
                    Id = 2,
                    Name = "Fuerte Avokado",
                    Description = "Yumuşak dokusu ve hafif tatlı tadı ile bilinen orta boyutlu avokado çeşidi.",
                    Price = 34.99M,
                    ImageUrl = "/images/fuerte.jpg",
                    Type = "Fuerte",
                    LongDescription = "Fuerte avokadosu, Hass'tan daha büyük olan, parlak yeşil kabuğa sahip armut şeklinde bir meyvedir. Adını İspanyolca \"güçlü\" kelimesinden alır ve soğuk iklimlere dayanıklılığıyla bilinir. Daha az yağlı olmasına rağmen, zengin kremsi tadıyla sandviçler, salatalar ve smoothie'ler için mükemmel bir seçimdir. Olgunlaştığında ete hafifçe bastırıldığında yumuşak hissedilir, ancak rengi yeşil kalır.",
                    CreatedAt = staticDate,
                    IsAvailable = true
                },
                new Avocado
                {
                    Id = 3,
                    Name = "Zutano Avokado",
                    Description = "Daha açık yeşil rengi ve su gibi dokusu ile tanınan düşük yağlı avokado.",
                    Price = 27.99M,
                    ImageUrl = "/images/zutano.jpg",
                    Type = "Zutano",
                    LongDescription = "Zutano avokadosu, parlak, açık yeşil kabuğu ve armut şekli ile tanınır. Daha düşük yağ içeriği nedeniyle, eti daha az kremsi ve hafif sulu bir dokuya sahiptir. Hafif tatlı tadı vardır ve özellikle yemeklerde garnitür olarak veya hafif bir lezzet isteyenler için idealdir. Soğuğa dayanıklı olduğu için daha serin iklimlerde yetiştirilir ve genellikle kış aylarında bulunur.",
                    CreatedAt = staticDate,
                    IsAvailable = true
                },
                new Avocado
                {
                    Id = 4,
                    Name = "Bacon Avokado",
                    Description = "İnce, kolay soyulabilen kabuğu ve orta büyüklükteki çekirdeğiyle bilinen avokado türü.",
                    Price = 32.99M,
                    ImageUrl = "/images/bacon.jpg",
                    Type = "Bacon",
                    LongDescription = "Bacon avokadosu, adını avokado yetiştiricisi James Bacon'dan alan, orta boy meyveli bir türdür. İnce, kolay soyulabilen parlak yeşil kabuğa ve orta büyüklükte bir çekirdeğe sahiptir. Eti sarı-yeşil renkte, kremsi bir dokuya sahip olup, hafif tatlı bir tadı vardır. Hass'a göre daha az yağlı ancak Zutano'dan daha kremsidir. Soğuğa dayanıklı olduğundan, kış aylarında bulunabilir ve salatalarda, sandviçlerde veya doğrudan tüketim için idealdir.",
                    CreatedAt = staticDate,
                    IsAvailable = true
                }
            );
            
            // Sepet ilişkilerini ayarla
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Avocado)
                .WithMany()
                .HasForeignKey(ci => ci.AvocadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 