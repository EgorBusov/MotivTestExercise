using MotivExercise.Models.OrdersModels;
using System.ComponentModel.DataAnnotations;

namespace MotivExercise.Models
{
    /// <summary>
    /// Для поиска
    /// </summary>
    public class Search : Order
    {
        public new int? Id { get; set; }
        public new string? Country { get; set; }
        public new string? Region { get; set; }
        public new string? Locality { get; set; }
        [RegularExpression(@"^(\+7)[0-9]{10}$")]
        public new string? Telephone { get; set; }
        public new string? Cause { get; set; }
        public new string? Department { get; set; }
        public new string? DateCreate { get; set; }
    }
}
