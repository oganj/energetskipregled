using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnergetskiPregled.Common.Helpers;
using EnergetskiPregled.Contracts.Mappers;

namespace EnergetskiPregled.Mappers
{
	public class MapperProvider : IMapperProvider
	{
		private readonly Dictionary<string, ICustomMapper> _customMappersDictionary = new Dictionary<string, ICustomMapper>();
		private readonly IEnumerable<ICustomMapper> _customMappers;

		public MapperProvider(IEnumerable<ICustomMapper> customMappers)
		{
			_customMappers = customMappers;
			PrefillMappers(customMappers);
		}

		private void PrefillMappers(IEnumerable<ICustomMapper> customMappers)
		{
			if (customMappers == null)
			{
				return;
			}
			foreach (var customMapper in customMappers)
			{
				foreach (var typeKey in customMapper.TypeKeys)
				{
					if (!_customMappersDictionary.ContainsKey(typeKey))
					{
						_customMappersDictionary.Add(typeKey, customMapper);
					}
				}
			}
		}

		public IMapper<TEntity, TDto> GetMapper<TEntity, TDto>()
			where TEntity : class, new()
			where TDto : new()
		{
			string typeKey = TypeHelper.GetTypesKey<TEntity, TDto>();

			ICustomMapper customMapper;
			if (_customMappersDictionary.TryGetValue(typeKey, out customMapper))
			{
				var mapper = customMapper.GetMapper<TEntity, TDto>();
				if (mapper != null)
				{
					return mapper;
				}
			}

			return new Mapper<TEntity, TDto>();//Later we can extend this implementation and provide any custom mapping
		}
	}
}
