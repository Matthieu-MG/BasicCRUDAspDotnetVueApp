using System.Linq.Expressions;
using AutoMapper;
using Enterprise.API.Requests;
using Enterprise.API.Responses;
using Enterprise.API.Services.Repositories;
using Enterprise.Data;
using Enterprise.Models;
using Enterprise.Models.Requests;
using Enterprise.Models.Responses;
using Microsoft.EntityFrameworkCore;

public class EmployeeRepository :
 GenericRepository<Employee, PostEmployeeDTO, EmployeeDTO>,
 IPagedGenericRepository<Employee, PostEmployeeDTO, EmployeeDTO>
{
    public EmployeeRepository(EnterpriseDbContext context, IMapper mapper) : base(context, mapper)
    {}

    public async Task<Pagination<EmployeeDTO>> GetPage(GetQueryDTO request)
    {
        return await Pagination<EmployeeDTO>.GetPage<Employee>(
            request, _context.Employee,
            (q) => q.Where(em => em.Name.Contains(request.SearchTerm ?? "") || em.Surname.Contains(request.SearchTerm ?? "")),
            GetSortProperty,
            _mapper.ConfigurationProvider  
        );
    }

    public async Task<List<EmployeeDTO>> GetEmployeesByName(string name)
    {
        return _mapper.Map<List<EmployeeDTO>>( 
            await _context.Employee
            .Where( em => em.Name.Contains(name) || em.Surname.Contains(name) ).ToListAsync()
        );
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _context.Employee.AnyAsync(em => em.Email == email);
    }

    private Expression<Func<Employee, object>> GetSortProperty(GetQueryDTO request)
    {
        return request.SortBy?.ToLower() switch
        {
            "name" => em => em.Name,
            "surname" => em => em.Surname,
            "dob" => em => em.DateOfBirth,
            _ => em => em.ID
        };
    }
}