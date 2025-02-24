using AutoMapper;
using Enterprise.Models;
using Enterprise.Models.Responses;
using Enterprise.Models.Requests;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quotation, QuotationQueryableDTO>()
            .ForMember( dst => dst.ProductName, opt => opt.MapFrom( src => src.ProductObj.Name))
            .ForMember( dst => dst.EmployeeName, opt => opt.MapFrom(src => src.EmployeeObj.Name + " " + src.EmployeeObj.Surname))
            .ForMember( dst => dst.EmployeeId, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember( dst => dst.SocietyName, opt => opt.MapFrom( src => src.SocietyObj.Name))
            .ForMember( dst => dst.State, opt => opt.MapFrom( src => src.State.ToString()));
        CreateMap<QuotationQueryableDTO, QuotationDTO>()
            .ForMember( dst => dst.Price, opt => opt.MapFrom(src => src.Price.ToString("C2")));
        CreateMap<PostQuotationDTO, Quotation>();

        CreateMap<Order, OrderQueryableDTO>()
            .ForMember( dst => dst.ProductName, opt => opt.MapFrom( src => src.ProductObj.Name))
            .ForMember( dst => dst.EmployeeName, opt => opt.MapFrom(src => src.EmployeeObj.Name + " " + src.EmployeeObj.Surname))
            .ForMember( dst => dst.EmployeeId, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember( dst => dst.SocietyName, opt => opt.MapFrom( src => src.SocietyObj.Name))
            .ForMember( dst => dst.State, opt => opt.MapFrom( src => src.State.ToString()));
        CreateMap<OrderQueryableDTO, OrderDTO>()
            .ForMember( dst => dst.Price, opt => opt.MapFrom(src => src.Price.ToString("C2")));
        CreateMap<PostOrderDTO, Order>();

        CreateMap<Product, ProductDTO>()
            .ForMember( dst => dst.StandardPrice, opt => opt.MapFrom(src => src.StandardPrice.ToString("C2")));
        CreateMap<PostProductDTO, Product>();

        CreateMap<Society, SocietyDTO>();
        CreateMap<PostSocietyDTO, Society>();

        CreateMap<Employee, EmployeeDTO>();
        CreateMap<PostEmployeeDTO, Employee>();
    }
}