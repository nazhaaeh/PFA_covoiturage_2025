using Microsoft.AspNetCore.Identity;

namespace Covoiturage_PFA.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Numpermit { get; set; }

        public int deleted { get; set; }

        // Pour relation avec Trajet et Réservations
        public ICollection<Trajet> TrajetsConducteur { get; set; }
        public ICollection<TrajetPassager> ReservationsPassager { get; set; }
        public string Role { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }


    }

}
