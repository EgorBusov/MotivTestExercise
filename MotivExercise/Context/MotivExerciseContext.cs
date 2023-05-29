using Microsoft.EntityFrameworkCore;
using MotivExercise.Models;
using MotivExercise.Models.OrdersModels;

namespace MotivExercise.Context
{
    public class MotivExerciseContext : DbContext
    {
        public MotivExerciseContext(DbContextOptions<MotivExerciseContext> options)
            : base(options)
        {

        }
        /// <summary>
        /// Заявки
        /// </summary>
        public DbSet<Order> Orders { get; set; }
        /// <summary>
        /// Направления приема заявок
        /// </summary>
        public DbSet<Department> Departments { get; set; }

    }
}
