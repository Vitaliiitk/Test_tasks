using AutoMapper;
using Dogs.Host.Data.Entities;
using Dogs.Host.Models.Dtos;

namespace Dogs.Host.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile() 
		{
			CreateMap<EntityDog, DogsDto>();
		}
	}
}
