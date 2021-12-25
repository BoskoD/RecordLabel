namespace ContentManagement.Domain.Artists;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;


public class Artist : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public int? ArtistId { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string ArtistName { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Country { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Biography { get; set; }
}