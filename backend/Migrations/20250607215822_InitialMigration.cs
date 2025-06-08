using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avocados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LongDescription = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    IsAvailable = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avocados", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Avocados",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageUrl", "IsAvailable", "LongDescription", "Name", "Price", "Type" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yüksek yağ içeriği ve kremsi dokusu ile bilinen, en popüler avokado çeşidi.", "/images/hass.jpg", true, "Hass avokadosu, koyu yeşilden siyaha dönen pürüzlü kabuğu ile kolayca tanınır. Eti yüksek yağ içeriği nedeniyle son derece kremsi ve lezzetlidir. Olgunlaştıkça kabuk rengi koyulaşır ve ete hafif baskı uygulandığında yumuşaksa, yemeye hazır demektir. Hass, tüm dünyada en yaygın yetiştiriciliği yapılan avokado çeşididir ve özellikle guacamole yapmak için mükemmeldir.", "Hass Avokado", 29.99m, "Hass" },
                    { 2, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yumuşak dokusu ve hafif tatlı tadı ile bilinen orta boyutlu avokado çeşidi.", "/images/fuerte.jpg", true, "Fuerte avokadosu, Hass'tan daha büyük olan, parlak yeşil kabuğa sahip armut şeklinde bir meyvedir. Adını İspanyolca \"güçlü\" kelimesinden alır ve soğuk iklimlere dayanıklılığıyla bilinir. Daha az yağlı olmasına rağmen, zengin kremsi tadıyla sandviçler, salatalar ve smoothie'ler için mükemmel bir seçimdir. Olgunlaştığında ete hafifçe bastırıldığında yumuşak hissedilir, ancak rengi yeşil kalır.", "Fuerte Avokado", 34.99m, "Fuerte" },
                    { 3, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Daha açık yeşil rengi ve su gibi dokusu ile tanınan düşük yağlı avokado.", "/images/zutano.jpg", true, "Zutano avokadosu, parlak, açık yeşil kabuğu ve armut şekli ile tanınır. Daha düşük yağ içeriği nedeniyle, eti daha az kremsi ve hafif sulu bir dokuya sahiptir. Hafif tatlı tadı vardır ve özellikle yemeklerde garnitür olarak veya hafif bir lezzet isteyenler için idealdir. Soğuğa dayanıklı olduğu için daha serin iklimlerde yetiştirilir ve genellikle kış aylarında bulunur.", "Zutano Avokado", 27.99m, "Zutano" },
                    { 4, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "İnce, kolay soyulabilen kabuğu ve orta büyüklükteki çekirdeğiyle bilinen avokado türü.", "/images/bacon.jpg", true, "Bacon avokadosu, adını avokado yetiştiricisi James Bacon'dan alan, orta boy meyveli bir türdür. İnce, kolay soyulabilen parlak yeşil kabuğa ve orta büyüklükte bir çekirdeğe sahiptir. Eti sarı-yeşil renkte, kremsi bir dokuya sahip olup, hafif tatlı bir tadı vardır. Hass'a göre daha az yağlı ancak Zutano'dan daha kremsidir. Soğuğa dayanıklı olduğundan, kış aylarında bulunabilir ve salatalarda, sandviçlerde veya doğrudan tüketim için idealdir.", "Bacon Avokado", 32.99m, "Bacon" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avocados");
        }
    }
}
