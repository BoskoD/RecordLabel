namespace ContentManagement.Domain.Artists.Validators;

using ContentManagement.Dtos.Artist;
using FluentValidation;

public class ArtistForUpdateDtoValidator: ArtistForManipulationDtoValidator<ArtistForUpdateDto>
{
    public ArtistForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}