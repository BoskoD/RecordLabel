namespace ContentManagement.Controllers.v1;

using ContentManagement.Domain.Artists.Features;
using ContentManagement.Dtos.Artist;
using ContentManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/artists")]
[ApiVersion("1.0")]
public class ArtistsController: ControllerBase
{
    private readonly IMediator _mediator;

    public ArtistsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Artist record.
    /// </summary>
    /// <response code="201">Artist created.</response>
    /// <response code="400">Artist has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Artist.</response>
    [ProducesResponseType(typeof(ArtistDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddArtist")]
    public async Task<ActionResult<ArtistDto>> AddArtist([FromBody]ArtistForCreationDto artistForCreation)
    {
        var command = new AddArtist.AddArtistCommand(artistForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetArtist",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Artist by ID.
    /// </summary>
    /// <response code="200">Artist record returned successfully.</response>
    /// <response code="400">Artist has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Artist.</response>
    [ProducesResponseType(typeof(ArtistDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id}", Name = "GetArtist")]
    public async Task<ActionResult<ArtistDto>> GetArtist(Guid id)
    {
        var query = new GetArtist.ArtistQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Artists.
    /// </summary>
    /// <response code="200">Artist list returned successfully.</response>
    /// <response code="400">Artist has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Artist.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<ArtistDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetArtists")]
    public async Task<IActionResult> GetArtists([FromQuery] ArtistParametersDto artistParametersDto)
    {
        var query = new GetArtistList.ArtistListQuery(artistParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Updates an entire existing Artist.
    /// </summary>
    /// <response code="204">Artist updated.</response>
    /// <response code="400">Artist has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Artist.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id}", Name = "UpdateArtist")]
    public async Task<IActionResult> UpdateArtist(Guid id, ArtistForUpdateDto artist)
    {
        var command = new UpdateArtist.UpdateArtistCommand(id, artist);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Artist record.
    /// </summary>
    /// <response code="204">Artist deleted.</response>
    /// <response code="400">Artist has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Artist.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id}", Name = "DeleteArtist")]
    public async Task<ActionResult> DeleteArtist(Guid id)
    {
        var command = new DeleteArtist.DeleteArtistCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
