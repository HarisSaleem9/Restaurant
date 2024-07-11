using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UploadRestaurantLogo;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Quries.GetAllRestaurant;
using Restaurants.Application.Restaurants.Quries.GetRestaurantById;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorizarion;

namespace Restaurant.Api.Controllers;

[ApiController]
[Route("api/restaurants")]
[Authorize]
public class RestaurantsController : ControllerBase 
{
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
       _mediator = mediator;
    }

    [HttpGet]
    //[Authorize(Policy=PolicyName.AtLeast2Restaurants)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantQuire Query)
    {
        var restaurants = await _mediator.Send(Query);
        return Ok(restaurants);
    }
    [HttpGet("{Id}")]
    //[Authorize(Policy = PolicyName.HasNationality)]
    public async Task<IActionResult> GetById(int Id)
    {
        var restaurtant = await _mediator.Send(new GetRestaurantByIdQury { Id=Id});
        return Ok(restaurtant);
    }
    [HttpPost]
    [Authorize(Policy=PolicyName.AtLeast20)]
    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand createRestaurantCommand)
    {
        int id = await _mediator.Send(createRestaurantCommand);
        return CreatedAtAction(nameof(GetById),new {id},null);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteRestaurant(int Id)
    {
        var isDeleted = await _mediator.Send(new DeleteRestaurantCommand { Id = Id });
        if (isDeleted)
        {
            return NoContent();
        }
        return NotFound();
    }

    [HttpPost("{id}/logo")]
    public async Task<IActionResult> UploadLogo([FromRoute]int id,IFormFile file)
    {
        using var stream = file.OpenReadStream();
        var command = new UploadRestaurantLogoCommand()
        {
            RestaurantId = id,
            FileName = file.FileName,
            File = stream
        };
        await _mediator.Send(command);
        return NoContent();
    }

}
