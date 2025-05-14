// Import the necessary namespaces for AutoMapper and models
using AutoMapper;
using TodoAPi.Contract; // Import contract classes (DTOs)
using TodoAPi.Models; // Import the Todo model class

namespace TodoAPi.Mapping
{
    // AutoMapper profile to define object-to-object mappings
    public class AutoMapperProfile : Profile
    {
        // Constructor for defining mapping configurations
        public AutoMapperProfile()
        {
            // Map from CreateTodoRequest DTO to Todoitems Model
            CreateMap<CreateTodoRequest, Todoitems>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id field (it will be generated)
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore CreatedAt (automatically set in the model)
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore UpdatedAt (handled internally)

            // Map from UpdateToDoRequest DTO to Todoitems Model
            CreateMap<UpdateToDoRequest, Todoitems>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id field (should not be modified)
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore CreatedAt (should remain unchanged)
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore UpdatedAt (automatically updated)
        }
    }
}
