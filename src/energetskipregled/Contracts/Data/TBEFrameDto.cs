namespace EnergetskiPregled.Contracts.Data
{
    public class TBEFrameDto
    {
		public virtual int Id { get; set; }
		public virtual TBEFrameCategoryDto Category { get; set; }
		public virtual int CategoryId { get; set; }
		public virtual string Name { get; set; }
		public virtual float Uf { get; set; }
		public virtual float g { get; set; }
	}
}
