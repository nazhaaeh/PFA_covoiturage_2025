using Microsoft.AspNetCore.Identity;

namespace Covoiturage_PFA.Models
{
    public class DemandeProfil 
    {
          public int id { get; set; }
        public string username { get; set; }

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public int contact { get; set; }


        public string Numdepermit { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
