namespace MotivExercise.Models.OrdersModels
{
    public class OrderAdd : Order
    {
        public new int? Id { get; set; }
        public new string? DateCreate { get; set; }

        public static Order Transform(OrderAdd orderAdd)
        {
            Order order = new Order()
            {
                Id = orderAdd.Id ?? 0, Locality = orderAdd.Locality, Telephone = orderAdd.Telephone,
                Cause = orderAdd.Cause, Department = orderAdd.Department, Region = orderAdd.Region,
                DateCreate = orderAdd.DateCreate ?? default, Country = orderAdd.Country
            };
            return order;
        }
    }
}
