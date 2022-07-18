using AutoMapper;
/// <summary>
/// Register the DTO and Source class mapping 
/// </summary>
internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<PostCode, PostCodeDto>();
    }
}

