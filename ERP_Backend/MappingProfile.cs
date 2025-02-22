using AutoMapper;
using MvcQuotation.Models;
using Enterprise.API.QuotationDTO;
using Enterprise.API.PostQuotationDTO;
using Enterprise.API.Product;
using Enterprise.API.ProductDTO;
using Enterprise.API.DTOs;
using Enterprise.API.Society;
using Enterprise.API.SocietyDTO;
using Enterprise.API.PostSocietyDTO;
using Enterprise.API.Employee;
using Enterprise.API.EmployeeDTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Quotation, QuotationDTO>()
            .ForMember( dst => dst.ProductName, opt => opt.MapFrom( src => src.ProductObj.Name))
            .ForMember( dst => dst.EmployeeName, opt => opt.MapFrom(src => src.EmployeeObj.Name + " " + src.EmployeeObj.Surname))
            .ForMember( dst => dst.EmployeeId, opt => opt.MapFrom(src => src.EmployeeID))
            .ForMember( dst => dst.SocietyName, opt => opt.MapFrom( src => src.SocietyObj.Name))
            .ForMember( dst => dst.State, opt => opt.MapFrom( src => src.State.ToString()));
        CreateMap<PostQuotationDTO, Quotation>();

        CreateMap<Product, ProductDTO>();
        CreateMap<PostProductDTO, Product>();

        CreateMap<Society, SocietyDTO>();
        CreateMap<PostSocietyDTO, Society>();

        CreateMap<Employee, EmployeeDTO>();
        CreateMap<PostEmployeeDTO, Employee>();
    }
}