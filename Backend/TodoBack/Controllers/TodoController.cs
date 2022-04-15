using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoBack.Domain;
using TodoBack.Dto;
using TodoBack.Infrastructure;
using TodoBack.Repositories;

namespace TodoBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(ITodoRepository todoRepository, IUnitOfWork unitOfWork)
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public List<TodoDto> GetAll()
        {
            List<Todo> todos = _todoRepository.GetTodos();

            return todos.Select(todo => new TodoDto()
            {
                Id = todo.Id,
                Title = todo.Title,
                IsDone = todo.IsDone,
            }).ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoDto todoDto)
        {
            try
            {
                Todo todo = new Todo()
                {
                    Id = 0,
                    Title = todoDto.Title,
                    IsDone = todoDto.IsDone,
                };

                _todoRepository.Create(todo);

                _unitOfWork.Commit();

                return Ok(todo.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] TodoDto todoDto)
        {
            try
            {
                _todoRepository.Update(new Todo()
                {
                    Id = todoDto.Id,
                    Title = todoDto.Title,
                    IsDone = true
                });

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Id can not be negative or 0");
                }

                _todoRepository.Delete(id);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
    }
}
