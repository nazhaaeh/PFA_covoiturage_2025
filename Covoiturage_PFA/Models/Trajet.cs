using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Covoiturage_PFA.Models
{
    public class Trajet
    {
        public int Id { get; set; }
        public string PointDepart { get; set; } = string.Empty;
        public string PointArrivee { get; set; } = string.Empty;
        public DateTime DateDepart { get; set; }
        public bool IsRecurring { get; set; }
        public int PlacesDisponibles { get; set; }
        public decimal Prix { get; set; }
        public string MatriculeCar { get; set; } = string.Empty;

        public int deletedOrnotdispo { get; set; } = 0;

        // on garde l'ID du conducteur (clé étrangère)
        public string ConducteurId { get; set; } = string.Empty;

        // ⬇⬇⬇ IGNORER ces navigations pour la sérialisation/validation
        [JsonIgnore, ValidateNever]
        public ApplicationUser? Conducteur { get; set; }

        [JsonIgnore, ValidateNever]
        public ICollection<TrajetPassager>? TrajetPassagers { get; set; }
    }
}