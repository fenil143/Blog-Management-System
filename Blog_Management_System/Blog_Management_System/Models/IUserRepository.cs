namespace BlogManagement.Models
{
    public interface IUserRepository
    {
        public Customer GetById(int id);
        public Customer GetByEmail(string email);
        public void DeleteById(int id);
        public List<Customer> GetAll();
        public Customer Create(Customer user);
        public Customer Update(Customer user);
        public void SeedDummyData();
        public void SetBlog(int id, Blog blog);
        public List<Blog> GetBlogs(int id);


    }
}
