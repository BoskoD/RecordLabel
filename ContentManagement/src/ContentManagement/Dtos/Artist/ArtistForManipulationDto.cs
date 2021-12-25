namespace ContentManagement.Dtos.Artist;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class ArtistForManipulationDto 
{
   public int? ArtistId { get; set; }
   public string ArtistName { get; set; }
   public string Country { get; set; }
   public string Biography { get; set; }
}