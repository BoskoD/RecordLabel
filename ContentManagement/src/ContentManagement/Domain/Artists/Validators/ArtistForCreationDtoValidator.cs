namespace ContentManagement.Domain.Artists.Validators;

using ContentManagement.Dtos.Artist;
using FluentValidation;

public class ArtistForCreationDtoValidator: ArtistForManipulationDtoValidator<ArtistForCreationDto>
{
    public ArtistForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}