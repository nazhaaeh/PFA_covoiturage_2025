namespace Covoiturage_PFA.Models
{
    public class TrajetPassager
    {
        public int Id { get; set; }
        public int TrajetId { get; set; }
        public Trajet Trajet { get; set; }

        public string UtilisateurId { get; set; }
        public ApplicationUser Utilisateur { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int UserRating {  get; set; }
        public string Commentrating {  get; set; }

    }
}
