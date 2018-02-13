namespace EnergetskiPregled.Models
{
    public class TBE
    {
		public virtual int Id { get; set; }
		public virtual TBEMaterial TBEMaterial { get; set; }
		public virtual int? TBEMaterialId { get; set; }
		public virtual TBEFrame TBEFrame { get; set; }
		public virtual int? TBEFrameId { get; set; }
		public virtual TBEHeatCorrectionFactor TBEHeatCorrectionFactor { get; set; }
		public virtual int? TBEHeatCorrectionFactorId { get; set; }
		public virtual string POS { get; set; }
		public virtual float Width { get; set; }
		public virtual float Height { get; set; }
		public virtual float Ag { get; set; }
		public virtual float Af { get; set; }
		public virtual float Lg { get; set; }
	}
}
