using TodoBack.Domain;

namespace TodoBack.Repositories
{
    public interface ITodoRepository
    {
        List<Todo> GetTodos();
        Todo? Get( int id );
        void Create( Todo todo );
        void Delete( int id );
        int Update( Todo todo );
    }
}
