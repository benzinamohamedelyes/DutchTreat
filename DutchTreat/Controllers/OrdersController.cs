using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public OrdersController(IDutchRepository repository, ILogger<ProductsController> logger, IMapper mapper)
        {
            this._repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get(bool includeAllItems = true)
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(_repository.GetAllOrders(includeAllItems)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bad Request: {ex}");
                return BadRequest("Bad Resquest");
            }

        }
        [HttpGet("{id:int}")]
        public ActionResult<Order> Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);
                if (order != null) return Ok(_mapper.Map<Order, OrderViewModel>(order));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bad Request: {ex}");
                return BadRequest("Bad Resquest");
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Order newOrder = _mapper.Map<OrderViewModel, Order>(model);
                    if (newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);

                    if (_repository.SaveAll())
                    {
                        OrderViewModel vm = _mapper.Map<Order, OrderViewModel>(newOrder);
                        return Created($"api/orders/{vm.OrderId}", vm);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"failed to save the new order: {ex}");
            }
            return BadRequest($"failed to save the new order");
        }
    }
}
