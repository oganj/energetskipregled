using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergetskiPregled.Contracts.Mappers
{
	public interface IMapper<TEntity, TDto>
		where TEntity : class, new()
		where TDto : new()
	{
		TDto MapToDto(TEntity entity);

		TEntity MapToEntity(TDto dto, TEntity e);
	}
}
