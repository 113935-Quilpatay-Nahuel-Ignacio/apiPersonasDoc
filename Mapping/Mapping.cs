using ApiPersonasDoc.DTOs;
using ApiPersonasDoc.Models;
using AutoMapper;
using static ApiPersonasDoc.Services.PersonaService.Commands.CreatePersona;

namespace ApiPersonasDoc.Mapping
{
    public class Mapping : Profile
    {
        public Mapping() 
        {
            CreateMap<PersonaDTO, Persona>().ReverseMap();
            CreateMap<CreatePersonaCommand, Persona>().ReverseMap();
        }
    }
}
