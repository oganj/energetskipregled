namespace EnergetskiPregled.Models
{
    public class MaterialThickness
    {
		public virtual int Id { get; set; }
		public virtual NonTrasparentBuildingElemet BuildingElement { get; set; }
		public virtual int BuildingElementId { get; set; }
		public virtual Material Material { get; set; }
		public virtual int MaterialId { get; set; }
		public virtual float Thickness { get; set; }
	}
}
