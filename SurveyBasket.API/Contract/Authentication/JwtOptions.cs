using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.API.Contract.Authentication
{
    public class JwtOptions
    {
        public static string sectionName = "Jwt";
        [Required]
        public string Key { get; set; } = string.Empty;
        [Required]
        public string Issuer { get; set; } = string.Empty;
        [Required]
        public string Audience { get; set; } = string.Empty;
        [Range(0, int.MaxValue)]
        public int ExpireyMinutes { get; set; }
    }

}
