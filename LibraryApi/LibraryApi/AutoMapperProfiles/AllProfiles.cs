using AutoMapper;
using LibraryApi.Data;

namespace LibraryApi.AutoMapperProfiles;

public class AllProfiles : Profile
{
    public AllProfiles()
    {
        CreateMap<Book, DTOs.Book>()
            .ForMember(dest => dest.IsCheckedOut, opt => opt.MapFrom(src => src.CheckedOutByUser != null));

        CreateMap<DTOs.Book, Book>();
    }
}
