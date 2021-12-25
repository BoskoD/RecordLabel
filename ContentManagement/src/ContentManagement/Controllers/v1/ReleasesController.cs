namespace ContentManagement.Controllers.v1;

using ContentManagement.Domain.Releases.Features;
using ContentManagement.Dtos.Release;
using ContentManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/releases")]
[ApiVersion("1.0")]
public class ReleasesController: ControllerBase
{
    private readonly IMediator _mediator;

    public ReleasesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Release record.
    /// </summary>
    /// <response code="201">Release created.</response>
    /// <response code="400">Release has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Release.</response>
    [ProducesResponseType(typeof(ReleaseDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddRelease")]
    public async Task<ActionResult<ReleaseDto>> AddRelease([FromBody]ReleaseForCreationDto releaseForCreation)
    {
        var command = new AddRelease.AddReleaseCommand(releaseForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetRelease",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Release by ID.
    /// </summary>
    /// <response code="200">Release record returned successfully.</response>
    /// <response code="400">Release has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Release.</response>
    [ProducesResponseType(typeof(ReleaseDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id}", Name = "GetRelease")]
    public async Task<ActionResult<ReleaseDto>> GetRelease(Guid id)
    {
        var query = new GetRelease.ReleaseQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Releases.
    /// </summary>
    /// <response code="200">Release list returned successfully.</response>
    /// <response code="400">Release has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Release.</response>
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
    [ProducesResponseType(typeof(IEnumerable<ReleaseDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetReleases")]
    public async Task<IActionResult> GetReleases([FromQuery] ReleaseParametersDto releaseParametersDto)
    {
        var query = new GetReleaseList.ReleaseListQuery(releaseParametersDto);
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
    /// Updates an entire existing Release.
    /// </summary>
    /// <response code="204">Release updated.</response>
    /// <response code="400">Release has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Release.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id}", Name = "UpdateRelease")]
    public async Task<IActionResult> UpdateRelease(Guid id, ReleaseForUpdateDto release)
    {
        var command = new UpdateRelease.UpdateReleaseCommand(id, release);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Release record.
    /// </summary>
    /// <response code="204">Release deleted.</response>
    /// <response code="400">Release has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Release.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id}", Name = "DeleteRelease")]
    public async Task<ActionResult> DeleteRelease(Guid id)
    {
        var command = new DeleteRelease.DeleteReleaseCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
