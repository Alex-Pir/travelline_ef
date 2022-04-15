using TodoBack.Domain;
using TodoBack.Infrastructure;

namespace TodoBack.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _dbContext;

        public TodoRepository (TodoDbContext todoDbContext)
        {
            _dbContext = todoDbContext;
        }

        public void Create(Todo todo)
        {
            _dbContext.Set<Todo>().Add(todo);
        }

        public void Delete(int id)
        {
            Todo? todo = Get(id);

            if (todo == null)
            {
                throw new Exception($"Todo with id {id} not found");
            }

            _dbContext.Set<Todo>().Remove(todo);
        }

        public Todo? Get(int id)
        {
            return _dbContext.Set<Todo>().FirstOrDefault(x => x.Id == id);
        }

        public List<Todo> GetTodos()
        {
            return _dbContext.Set<Todo>().ToList();
        }

        public int Update(Todo todo)
        {
            _dbContext.Set<Todo>().Update(todo);

            return todo.Id;
        }
    }
}
