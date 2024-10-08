using K8S.DriverAPI.DTOs.Requests;
using K8S.DriverAPI.DTOs.Responses;
using K8S.DriverAPI.Models;
using Mapster;

namespace K8S.DriverAPI.Profiles
{
    public class MapsterProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // request to domain
            config.NewConfig<CreateDriverRequest, Driver>()
                .Map(dest => dest.Status, src => 1)
                .Map(dest => dest.AddedDate, src => DateTime.UtcNow)
                .Map(dest => dest.UpdatedDate, src => DateTime.UtcNow);


            config.NewConfig<UpdateDriverRequest, Driver>()
                .Map(dest => dest.UpdatedDate, src => DateTime.UtcNow);



            // domain to response
            // source to destination
            config.NewConfig<Driver, GetDriverResponse>()
                .Map(dest => dest.DriverId, src => src.Id)
                .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");
                
        }
    }
}
