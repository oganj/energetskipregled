using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Mappers
{
	public interface ICustomMapper
	{
		string[] TypeKeys { get; }

		IMapper<TEntity, TDto> GetMapper<TEntity, TDto>()
			where TEntity : class, new()
			where TDto : new();
	}
}
