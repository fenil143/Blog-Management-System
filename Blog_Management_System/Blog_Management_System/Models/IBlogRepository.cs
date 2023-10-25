namespace BlogManagement.Models
{
    public interface IBlogRepository
    {
        public Blog GetById(int id);
        public void Create(Blog blog);
        public void Update(Blog blog);
        public Blog Delete(int id);
        public bool Count(int id);
        public List<Blog> GetAll();
    }
}
