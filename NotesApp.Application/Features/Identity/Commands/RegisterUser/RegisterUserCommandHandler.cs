using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NotesApp.Application.Contracts.Identity;
using NotesApp.Application.Exceptions;
using NotesApp.Common.Audits;
using NotesApp.Domain;

namespace NotesApp.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IValidator<RegisterUserCommand> _validator;
    private readonly IMapper _mapper;
    private readonly IAudit _audit;
    private readonly IIdentityRepository _identityRepository;

    public RegisterUserCommandHandler(IValidator<RegisterUserCommand> validator, IMapper mapper,
        IAudit audit, IIdentityRepository identityRepository)
    {
        _validator = validator;
        _mapper = mapper;
        _audit = audit;
        _identityRepository = identityRepository;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);
        if(validationResult is null) throw new InvalidOperationException("Failed to validate the request, as the validation result was null");
        if(!validationResult.IsValid) throw new BadRequestException("Invalid User Details", validationResult.ToDictionary());

        User user = _mapper.Map<User>(request);
        if(user is null) throw new InvalidOperationException("Failed to map the request to User entity");

        SetAuditFields(user);

        string token = await _identityRepository.RegisterUserAsync(user);
        if(string.IsNullOrEmpty(token)) throw new InvalidOperationException("Failed to generate token");
        
        return token;
    }

    private void SetAuditFields(User user)
    {
        user.CreatedOn = _audit.CreatedOn;
        user.UpdatedOn = _audit.UpdatedOn;
    }
}
