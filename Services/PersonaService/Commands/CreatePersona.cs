using ApiPersonasDoc.Data;
using ApiPersonasDoc.DTOs;
using ApiPersonasDoc.Exceptions;
using ApiPersonasDoc.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ApiPersonasDoc.Services.PersonaService.Commands.CreatePersona;

namespace ApiPersonasDoc.Services.PersonaService.Commands
{
    public class CreatePersona
    {
        public class CreatePersonaCommand : IRequest<PersonaDTO>
        {
            public string Nombre { get; set; } = null!;

            public string Apellido { get; set; }

            public long TipoDocumentoId { get; set; }
        }
    }

    public class CreatePersonaCommandHandler : IRequestHandler<CreatePersonaCommand, PersonaDTO>
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CreatePersonaCommandHandler(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PersonaDTO> Handle(CreatePersonaCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePersonaCommandValidator(_context);

            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult);
            }

            var personaToCreate = _mapper.Map<Persona>(request);

            await _context.Personas.AddAsync(personaToCreate);
            await _context.SaveChangesAsync();

            var personaDTO = _mapper.Map<PersonaDTO>(personaToCreate);

            return personaDTO;
        }
    }

    public class CreatePersonaCommandValidator : AbstractValidator<CreatePersonaCommand>
    {
        private readonly ApplicationContext _context;

        public CreatePersonaCommandValidator(ApplicationContext context)
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .MinimumLength(4).WithMessage("{PropertyName} debe tener al menos 4 caracteres");

            RuleFor(p => p.Apellido)
                .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
                .NotNull().WithMessage("{PropertyName} no puede ser nulo")
                .MinimumLength(4).WithMessage("{PropertyName} debe tener al menos 4 caracteres");

            RuleFor(p => p.TipoDocumentoId)
            .NotEmpty().WithMessage("{PropertyName} no puede estar vacio")
            .NotNull().WithMessage("{PropertyName} no puede ser nulo")
            .Must(value => value == 1 || value == 2)
            .WithMessage("{PropertyName} solo puede ser 1 o 2");

            RuleFor(q => q)
                .MustAsync(PersonaUnique)
                .WithMessage("Esta persona ya existe");

            _context = context;
        }

        private async Task<bool> PersonaUnique(CreatePersonaCommand command, CancellationToken token)
        {
            bool isUnique = await _context.Personas.AnyAsync(p => p.Nombre == command.Nombre && p.Apellido == command.Apellido, token);

            return !isUnique;
        }
    }
}
