using ApiPersonasDoc.Data;
using ApiPersonasDoc.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApiPersonasDoc.Services.PersonaService.Queries
{
    public record GetPersonasQuery : IRequest<List<PersonaDTO>>;

    public class GetPersonasQueryHandler : IRequestHandler<GetPersonasQuery, List<PersonaDTO>>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public GetPersonasQueryHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PersonaDTO>> Handle(GetPersonasQuery request, CancellationToken cancellationToken)
        {
            var personas = await _context.Personas.ToListAsync();

            var data = _mapper.Map<List<PersonaDTO>>(personas);

            return data;
        }
    }
}
