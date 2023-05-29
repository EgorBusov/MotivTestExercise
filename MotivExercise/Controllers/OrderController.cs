using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotivExercise.Context;
using MotivExercise.Models;
using MotivExercise.Models.OrdersModels;

namespace MotivExercise.Controllers
{
    public class OrderController : Controller
    {
        private readonly MotivExerciseContext _context;

        public OrderController(MotivExerciseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Поиск заявок
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private async Task<IEnumerable<Order>> SearchOrders(Search search)
        {
            var query = _context.Orders.AsQueryable();

            if (search.Id > 0) { query = query.Where(o => o.Id == search.Id.Value); }
            if (search.Country != null) { query = query.Where(o => o.Country.Contains(search.Country)); }
            if (search.Region != null) {query = query.Where(o => o.Region.Contains(search.Region)); }
            if (search.Locality != null) { query = query.Where(o => o.Locality.Contains(search.Locality));}
            if (search.Telephone != null) { query = query.Where(o => o.Telephone == search.Telephone);}
            if (search.Cause != null) { query = query.Where(o => o.Cause.Contains(search.Cause));}
            if (search.Department != null) { query = query.Where(o => o.Department == search.Department);}
            if (search.DateCreate != null) { query = query.Where(o => o.DateCreate.Contains(search.DateCreate));}

            var results = await query.ToListAsync();
            return results;
        }

        /// <summary>
        /// Отображение всех заявок
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOrders(Search? search)
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                string errorMessage = (string)TempData["ErrorMessage"];
                ModelState.AddModelError("", errorMessage);
            }

            ViewBag.Departments = await _context.Departments.ToListAsync();

            if(search == null) { ViewBag.Orders = await _context.Orders.ToListAsync(); }
            else { ViewBag.Orders = await SearchOrders(search); }

            return View();
        }        

        /// <summary>
        /// Добавление заказа
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderAdd orderAdd)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderAdd.DateCreate = DateTime.Now.ToString();
                    Order order = OrderAdd.Transform(orderAdd);
                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();
                    return Redirect("/Order/GetOrders");
                }
                else
                {
                    TempData["ErrorMessage"] = "Заполните все поля правильно";
                    return Redirect("/Order/GetOrders");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Redirect("/Order/GetOrders");
            }
        }

        /// <summary>
        /// Редактирование заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> EditOrder(int id)
        {
            try
            {
                ViewBag.Departments = await _context.Departments.ToListAsync();
                Order order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == id) ?? throw new Exception("Заявка не найдена");
                OrderEdit orderEdit = new OrderEdit()
                {
                    Id = order.Id, Locality = order.Locality, Telephone = order.Telephone, Cause = order.Cause,
                    Department = order.Department, Country = order.Country, DateCreate = order.DateCreate, Region = order.Region
                };
                return View(orderEdit);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Redirect("/Order/GetOrders");
            }


        }

        /// <summary>
        /// Изменение заказа
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditOrder(Order o)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Order order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == o.Id) ??
                                  throw new Exception("Заявка не найдена");
                    order.Cause = o.Cause;
                    order.Country = o.Country;
                    order.Department = o.Department;
                    order.Locality = o.Locality;
                    order.Region = o.Region;
                    order.Telephone = o.Telephone;
                    await _context.SaveChangesAsync();

                    return Redirect("/Order/GetOrders");
                }
                else
                {
                    TempData["ErrorMessage"] = "Заполните все поля правильно";
                    return Redirect("/Order/GetOrders");
                }
                
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Redirect("/Order/GetOrders");
            }
        }

        /// <summary>
        /// Удаление заявки
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                Order order = await _context.Orders.FirstOrDefaultAsync(a => a.Id == id) ??
                              throw new Exception("Заявка не найдена");
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return Redirect("/Order/GetOrders");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Redirect("/Order/GetOrders");
            }
        }

    }
}
