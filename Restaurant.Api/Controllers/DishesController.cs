using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Quries.GetDishesByIdForRestaurant;
using Restaurants.Application.Dishes.Quries.GetDishesForRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteDishesForRestaurant;

namespace Restaurant.Api.Controllers
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            await _mediator.Send(command);
            return Ok(command);
        }
        [HttpGet]
        public async Task<IEnumerable<DishDto>> GetAllForRestarant([FromRoute] int restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery { RestaurantId = restaurantId });
            return dishes;
        }
        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForRestarant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dishes = await _mediator.Send(new GetDishesByIdForRestaurantQuery(restaurantId, dishId));
            return dishes;
        }
        [HttpDelete]
        public async Task<ActionResult<string>> DeleteDishesForRestaurant([FromRoute] int restaurantId)
        {
            var res = await _mediator.Send(new DeleteDishesForRestaurantCommand { RestaurantId = restaurantId });
            return res;
        }
    }
}
