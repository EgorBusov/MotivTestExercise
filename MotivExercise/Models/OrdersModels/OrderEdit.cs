using System.ComponentModel.DataAnnotations;

namespace MotivExercise.Models.OrdersModels
{
    public class OrderEdit : Order
    {
        [Required]
        public new int Id { get; set; }
        [Required]
        public new string DateCreate { get; set; }
    }
}
