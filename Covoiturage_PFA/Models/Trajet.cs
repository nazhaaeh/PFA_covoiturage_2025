namespace Covoiturage_PFA.Models
{
    public class Trajet
    {
        public int Id { get; set; }
        public string PointDepart { get; set; }
        public string PointArrivee { get; set; }
        public DateTime DateDepart { get; set; }
        public int PlacesDisponibles { get; set; }
        public decimal Prix { get; set; }
        public string MatriculeCar { get; set; }

        public int deletedOrnotdispo {  get; set; }
        // Relations
        public string ConducteurId { get; set; }  
        public ApplicationUser Conducteur { get; set; }

        public ICollection<TrajetPassager> TrajetPassagers { get; set; }
    }
}
