using System.ComponentModel.DataAnnotations;

namespace MotivExercise.Models.OrdersModels
{
    /// <summary>
    /// Обращение
    /// </summary>
    public class Order
    {
        public int Id { get; set; }
        /// <summary>
        /// Страна
        /// </summary>
        [Required]
        public string Country { get; set; }
        /// <summary>
        /// Регион
        /// </summary>
        [Required]
        public string Region { get; set; }
        /// <summary>
        /// Населенный пункт
        /// </summary>
        [Required]
        public string Locality { get; set; }
        /// <summary>
        /// Телефон обращения
        /// </summary>
        [Required]
        [RegularExpression(@"^(\+7)[0-9]{10}$")]
        public string Telephone { get; set; }
        /// <summary>
        /// Причина обращения
        /// </summary>
        [Required]
        public string Cause { get; set; }
        /// <summary>
        /// Направление, принявшее заявку
        /// </summary>
        [Required]
        public string Department { get; set; }
        /// <summary>
        /// Дата обращения
        /// </summary>
        public string DateCreate { get; set; }
    }
}
