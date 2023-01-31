
using System.ComponentModel.DataAnnotations;

namespace AsetQrApi.DTO
{
  public class BarangDto
  {
    [Required]
    public string Id { get; set; }
  }

  public class PrintDto
  {
    [Required]
    public string UnitKey { get; set; }
    [Required]
    public string AsetKey { get; set; }
  }
}