using Logger.Api.Enums;
using Logger.Api.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Logger.Api.Extensions.Validations;

public static class ValidationExtensions
{
    extension<T>(IValidator<T> validator)
    {
        public async Task<List<ResponseDetail>> ValidateRequestAsync(T instance, CancellationToken ct = default)
        {
            ValidationResult validation = await validator.ValidateAsync(instance, ct);

            if (validation.IsValid) return [];

            return [.. validation.Errors
                .Select(e => new ResponseDetail($"{e.PropertyName}: {e.ErrorMessage}", ResponseSeverity.Error))];
        }
    }
}
