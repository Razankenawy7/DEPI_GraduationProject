// File: Models/GlassFixationCategory.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace DEPI_GraduationProject.Models
{
    public class GlassFixationCategory
    {
        public int Id { get; set; } // Primary Key

        public string Name { get; set; }

        public decimal fixation_cost { get; set; } // ✅ Add this
    }
}
