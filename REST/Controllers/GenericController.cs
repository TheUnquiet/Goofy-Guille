using Domain.Mappers;
using Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace REST.Controllers
{
    [ApiController]
    public abstract class GenericController<TModel, TEntity, TDto, TCreateDto, TUpdateDto>
        : ControllerBase
        where TModel : class
        where TEntity : class
        where TDto : class
        where TCreateDto : class
        where TUpdateDto : class
    {
        private readonly IGenericService<TModel, TEntity> _service;
        private readonly IMapper<TModel, TDto> _toDtoMapper;
        private readonly IMapper<TCreateDto, TModel> _createMapper;
        private readonly IMapper<TUpdateDto, TModel> _updateMapper;

        protected GenericController(
            IGenericService<TModel, TEntity> service,
            IMapper<TModel, TDto> toDtoMapper,
            IMapper<TCreateDto, TModel> createMapper,
            IMapper<TUpdateDto, TModel> updateMapper)
        {
            _service = service;
            _toDtoMapper = toDtoMapper;
            _createMapper = createMapper;
            _updateMapper = updateMapper;
        }

        protected Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out var userId))
                throw new Domain.Exceptions.UnauthorizedException(
                    "User ID is missing or invalid.");

            return userId;
        }

        protected virtual void OnCreating(TModel model)
        {
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<ActionResult<TDto>> GetById(Guid id)
        {
            var model = await _service.GetByIdAsync(id);
            return Ok(_toDtoMapper.Map(model));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TDto>> Create(TCreateDto dto)
        {
            var model = _createMapper.Map(dto);
            OnCreating(model);
            var createdModel = await _service.CreateAsync(model);

            return Ok(_toDtoMapper.Map(createdModel));
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TDto>> Update(TUpdateDto dto)
        {
            var model = _updateMapper.Map(dto);
            var updatedModel = await _service.UpdateAsync(model);

            return Ok(_toDtoMapper.Map(updatedModel));
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}