using AutoMapper;

namespace CarDealer.Application.Utils.Mappings
{
    public interface IMapFrom<T>
    {   
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
