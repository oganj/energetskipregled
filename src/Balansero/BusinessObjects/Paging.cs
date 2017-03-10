namespace EnergetskiPregled.BusinessObjects
{
	public class Paging
	{
		public int? Page { get; set; }
		public int? PageSize { get; set; }

		public int PageValue
		{
			get
			{
				return Page ?? 1;
			}
		}

		public int PageSizeValue
		{
			get
			{
				return PageSize ?? 25;
			}
		}

		public int SkipCount
		{
			get
			{
				return (PageValue - 1) * PageSizeValue;
			}
		}

		public void FixPageForCount(int count)
		{
			if (count <= SkipCount)
			{
				Page = 1;
			}
		}
	}
}
