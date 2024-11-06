using FluentValidation;
using Sample.MediatR.Dto;

namespace Sample.MediatR.Application.Client.Create;

public class CreateClientValidator : AbstractValidator<CreateClientRequestDto>
{
    public CreateClientValidator()
    {
        //RuleFor(x => x.Name)
        //    .NotEmpty()
        //    .MaximumLength(50);

        //RuleFor(x => x.Email)
        //    .EmailAddress().WithMessage("BROOOOOO");
    }
}
