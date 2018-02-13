namespace EnergetskiPregled.Contracts.Data
{
    public class TBEDto
    {
		public virtual int Id { get; set; }
		public virtual TBEMaterialDto TBEMaterial { get; set; }
		public virtual int? TBEMaterialId { get; set; }
		public virtual TBEFrameDto TBEFrame { get; set; }
		public virtual int? TBEFrameId { get; set; }
		public virtual TBEHeatCorrectionFactorDto TBEHeatCorrectionFactor { get; set; }
		public virtual int? TBEHeatCorrectionFactorId { get; set; }
		public virtual string POS { get; set; }
		public virtual float Width { get; set; }
		public virtual float Height { get; set; }
		public virtual float Ag { get; set; }
		public virtual float Af { get; set; }
		public virtual float Lg { get; set; }
	}
}
