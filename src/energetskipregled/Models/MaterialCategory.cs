namespace EnergetskiPregled.Models
{
    public class MaterialCategory
    {
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual bool IsArchived { get; set; }
	}
}
