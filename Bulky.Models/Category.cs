using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [DisplayName("Category Name")]
    [Required]
    [MaxLength(30)]
    public string Name { get; set; }
    [DisplayName("Category Order")]
    [Range(1, 100, ErrorMessage = "Order should be between 1 and 100.")]
    public int DisplayOrder { get; set; }
}
