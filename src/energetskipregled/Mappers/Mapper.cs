using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Contracts.Mappers;

namespace EnergetskiPregled.Mappers
{
	public class Mapper<TEntity, TDto> : IMapper<TEntity, TDto>
		where TEntity : class, new()
		where TDto : new()
	{
		public TDto MapToDto(TEntity entity)
		{
			var dto = new TDto();
			dto.InjectFrom(entity);

			return dto;
		}

		public TEntity MapToEntity(TDto dto, TEntity e)
		{
			e.InjectFrom(dto);
			return e;
		}
	}
}
