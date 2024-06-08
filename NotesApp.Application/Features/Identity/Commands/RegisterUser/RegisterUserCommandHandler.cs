using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NotesApp.Application.Contracts.Identity;
using NotesApp.Application.Exceptions;
using NotesApp.Common.Audits;
using NotesApp.Common.Hashing;
using NotesApp.Domain;

namespace NotesApp.Application.Features.Identity.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IValidator<RegisterUserCommand> _validator;
    private readonly IMapper _mapper;
    private readonly IAudit _audit;
    private readonly IHasher _hasher;
    private readonly IIdentityRepository _identityRepository;
    private readonly IToken _token;

    public RegisterUserCommandHandler(IValidator<RegisterUserCommand> validator, IMapper mapper,
        IAudit audit, IHasher hasher, IIdentityRepository identityRepository, IToken token)
    {
        _validator = validator;
        _mapper = mapper;
        _audit = audit;
        _hasher = hasher;
        _identityRepository = identityRepository;
        _token = token;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Validates the incoming request
        ValidationResult validationResult = await _validator.ValidateAsync(request);
        if (validationResult is null) throw new InvalidOperationException("Validation failed: result was null.");
        if (!validationResult.IsValid) throw new BadRequestException("Invalid user details", validationResult.ToDictionary());

        // Maps the request to a User object
        User user = _mapper.Map<User>(request);
        if (user is null) throw new InvalidOperationException("Mapping to User entity failed.");

        // Creates an instance of UserCredentials
        UserCredentials userCredentials = new();

        // Sets fields for the User object and UserCredentials object
        SetFields(user, userCredentials);

        // Inserts the user
        await _identityRepository.CreateUserAsync(user);
        if (IsNullEmptyOrWhiteSpace(userCredentials.UserId)) throw new InvalidOperationException("User registration failed.");

        // Generates a hash for the provided password and assign the returned hash to UserCredentials
        userCredentials.PasswordHash = _hasher.Hash(request.Password);
        if (IsNullEmptyOrWhiteSpace(userCredentials.PasswordHash)) throw new InvalidOperationException("Password hashing failed.");

        // Inserts user credential.
        await _identityRepository.InsertUserCredsAsync(userCredentials);

        // Generates a JWT token (logic to be implemented)
        string token = _token.Generate(user);
        if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Token generation failed.");

        return token;
    }

    private void SetFields(User user, UserCredentials userCredentials)
    {
        user.UserId = Guid.NewGuid().ToString();
        user.CreatedOn = _audit.CreatedOn;
        user.UpdatedOn = _audit.UpdatedOn;

        userCredentials.UserId = user.UserId;
        userCredentials.CreatedOn = _audit.CreatedOn;
        userCredentials.UpdatedOn = _audit.UpdatedOn;
    }

    private static bool IsNullEmptyOrWhiteSpace(string userId)
    {
        return string.IsNullOrEmpty(userId) || string.IsNullOrWhiteSpace(userId);
    }
}
