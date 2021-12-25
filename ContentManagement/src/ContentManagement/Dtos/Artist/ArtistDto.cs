namespace ContentManagement.Dtos.Artist;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ArtistDto 
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
   public int? ArtistId { get; set; }
   public string ArtistName { get; set; }
   public string Country { get; set; }
   public string Biography { get; set; }
}