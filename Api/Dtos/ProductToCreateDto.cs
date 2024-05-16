using System.ComponentModel.DataAnnotations;

namespace Api.Dtos;

public class ProductToCreateDto
{
    [Required]
    public string  Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string PictureUrl { get; set; }
    [Required]
    public int ProductTypeId { get; set; }
    [Required]
    public int ProductBrandId { get; set; }
}
