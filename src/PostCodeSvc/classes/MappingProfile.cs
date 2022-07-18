using AutoMapper;
/// <summary>
/// Register the DTO and Source class mapping 
/// </summary>
internal class MappingProfile : Profile
{
    /// <summary>
    /// Put all the source class and destination DTO class mapping here
    /// </summary>
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<PostCodeDetail, PostCodeDto>();
    }
}

