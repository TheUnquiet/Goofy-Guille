namespace REST.Controller
{
    [ApiController]
    public abstract class GenericController<TModel, TEntity, TDto, TCreateDto, TUpdateDto> : ControllerBase
    {
        private readonly IGenericService<TModel, TEntity> _service;
        private readonly IMapper<TModel, TDto> _toDtoMapper;
        private readonly IMapper<TCreateDto, TModel> _createMapper;
        private readonly IMapper<TUpdateDto, TModel> _updateMapper;
        
        protected GenericController(IGenericService<TModel, TEntity> service, 
            IMapper<TModel, TDto> toDtoMapper, IMapper<TCreateDto, TModel> createMapper, IMapper<TUpdateDto, TModel> updateMapper)
        {
            _service = service;
            _toDtoMapper = toDtoMapper;
            _createMapper = createMapper;
            _updateMapper = updateMapper;          
        }

        protected Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId)) throw new UnauthorizedAcessException("User ID is missing or invalid");
            return userId;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TDto>> GetById(Guid id)
        {
            var model = await _service.GetByIdAsync(id);
            return Ok(_toDtoMapper.Map(model));
        }

        [HttpPost]
        public async Task<ActionResult<TDto>> Create(TCreateDto dto)
        {
            var model = _createMapper.Map(dto);
            var createModel = await _service.CreateAsync(model);
            return Ok(_toDtoMapper.Map(createdModel));
        }

        [HttpPut]
        public async Task<ActionResult<TDto>> Update(TUpdateDto dto)
        {
            var model = updateMapper.Map(dto);
            var updatedModel = await _service.UpdateAsync(model);
            return Ok(_toDtoMapper.Map(updatedModel));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}