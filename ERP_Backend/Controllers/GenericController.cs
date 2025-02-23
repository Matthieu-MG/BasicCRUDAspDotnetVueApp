using Enterprise.API.Requests;
using Enterprise.API.Services.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Enterprise.API.Controllers;

[Route("[controller]")]
public class GenericController<TEntity, TPost, TGet> : ControllerBase
    where TEntity : class
    where TPost : class, new()
    where TGet : class, new()
{
    protected readonly IPagedGenericRepository<TEntity, TPost, TGet> _repository;

    public GenericController(IPagedGenericRepository<TEntity, TPost, TGet> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        return Ok(await _repository.Read());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var modelData = await _repository.ReadById(id);
        if(modelData == null)
        {
            return NotFound();
        }
        return Ok(modelData);
    }

    [HttpGet("page")]
    public async Task<ActionResult> GetPage([FromQuery] GetQueryDTO request)
    {
        var page = await _repository.GetPage(request);
        if(page == null)
        {
            return NotFound(page);
        }

        return Ok(page);
    }

    [HttpPost]
    public async Task<ActionResult> Post(TPost postData,
                 [FromServices] IValidator<TPost> validator)
    {
        var results = await validator.ValidateAsync(postData);
        if(!results.IsValid)
        {
            return BadRequest(results.ToDictionary());
        }

        await _repository.Create(postData);
        return Created("{id}", postData);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, TPost putData)
    {
        await _repository.Update(id, putData);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repository.Delete(id);
        return NoContent();
    }

    [HttpGet("GetDTO")]
    public ActionResult GetDTO()
    {
        return Ok(new TGet());
    }

    [HttpGet("Count")]
    public ActionResult<int> GetTotalRecordsCount()
    {
        return Ok(_repository.GetTotalRecordsCount());
    }
}