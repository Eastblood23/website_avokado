using System.Collections.Generic;
using System.Linq;
using AvokadocuSezgin.Models.Dto;

namespace AvokadocuSezgin.Services
{
    public interface IAvokadoService
    {
        List<AvokadoDto> GetAllAvokados();
        AvokadoDto GetAvokadoById(int id);
    }

    public class AvokadoService : IAvokadoService
    {
        private readonly List<AvokadoDto> _avokados;

        public AvokadoService()
        {
            // Örnek veri oluşturuyoruz - gerçek uygulamada veritabanından gelecek
            _avokados = new List<AvokadoDto>
            {
                new AvokadoDto
                {
                    Id = 1,
                    Name = "Hass Avokado",
                    Description = "Küçük boyutlu, koyu yeşil-siyah kabuklu ve kremsi dokusu ile bilinen en popüler avokado türü.",
                    LongDescription = "Hass avokado, dünyada en çok tüketilen avokado çeşididir. Küçük-orta boyutlu olan bu avokado türü, olgunlaştıkça koyu yeşilden siyaha dönen bir kabuk rengine sahiptir. İçi kremsi ve yağlı bir dokuya sahip olup, lezzetli ve besleyicidir. Yüksek yağ içeriği sayesinde salatalarda, sandviçlerde ve guacamole yapımında tercih edilir.",
                    Price = "45 TL",
                    OriginalPrice = "55 TL",
                    Discount = "18%",
                    ImageUrl = "/images/hass-avocado.jpg",
                    Features = new List<string>
                    {
                        "Yüksek yağ içeriği",
                        "Kremsi doku",
                        "Koyu yeşil-siyah kabuk",
                        "Küçük-orta boy"
                    },
                    NutritionalInfo = new NutritionalInfoDto
                    {
                        Calories = "160 kcal",
                        Fat = "15g",
                        Carbs = "9g",
                        Protein = "2g",
                        Fiber = "7g"
                    },
                    Origin = "Meksika",
                    Season = "Yıl boyu",
                    Rating = 4.8,
                    Reviews = 124,
                    InStock = true
                },
                new AvokadoDto
                {
                    Id = 2,
                    Name = "Fuerte Avokado",
                    Description = "Daha büyük boyutlu, açık yeşil kabuklu ve daha az yağlı bir avokado türü.",
                    LongDescription = "Fuerte avokado, Hass avokadodan daha büyük boyutlu ve daha az yağlı bir türdür. Parlak yeşil kabuğu ve armut şekli ile tanınır. İçi açık yeşil renkli ve hafif sulu bir dokuya sahiptir.",
                    Price = "50 TL",
                    OriginalPrice = "60 TL",
                    Discount = "16%",
                    ImageUrl = "/images/fuerte-avocado.jpg",
                    Features = new List<string>
                    {
                        "Orta yağ içeriği",
                        "Hafif sulu doku",
                        "Parlak yeşil kabuk",
                        "Orta-büyük boy"
                    },
                    NutritionalInfo = new NutritionalInfoDto
                    {
                        Calories = "140 kcal",
                        Fat = "13g",
                        Carbs = "8g",
                        Protein = "2g",
                        Fiber = "6g"
                    },
                    Origin = "Kaliforniya",
                    Season = "Kasım-Mart",
                    Rating = 4.6,
                    Reviews = 98,
                    InStock = true
                },
                new AvokadoDto
                {
                    Id = 3,
                    Name = "Zutano Avokado",
                    Description = "Açık yeşil kabuklu, armut şeklinde ve hafif tadıyla bilinen bir avokado türü.",
                    LongDescription = "Zutano avokado, açık yeşil kabuklu ve armut şeklinde bir avokado türüdür. Kabuğu parlak ve incedir, soyması kolaydır. İçi açık sarı-yeşil renkli olup, diğer avokado türlerine göre daha düşük yağ içeriğine sahiptir.",
                    Price = "55 TL",
                    OriginalPrice = "65 TL",
                    Discount = "15%",
                    ImageUrl = "/images/zutano-avocado.jpg",
                    Features = new List<string>
                    {
                        "Düşük yağ içeriği",
                        "Sulu doku",
                        "Açık yeşil kabuk",
                        "Büyük boy"
                    },
                    NutritionalInfo = new NutritionalInfoDto
                    {
                        Calories = "120 kcal",
                        Fat = "10g",
                        Carbs = "8g",
                        Protein = "2g",
                        Fiber = "5g"
                    },
                    Origin = "Kaliforniya",
                    Season = "Eylül-Ocak",
                    Rating = 4.5,
                    Reviews = 76,
                    InStock = false
                }
            };
        }

        public List<AvokadoDto> GetAllAvokados()
        {
            return _avokados;
        }

        public AvokadoDto GetAvokadoById(int id)
        {
            return _avokados.FirstOrDefault(a => a.Id == id);
        }
    }
} 