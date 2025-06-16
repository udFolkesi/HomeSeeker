using Core.Entities;

namespace HomeSeeker.Models
{
    public class AccountsModel
    {
        public ICollection<User> Data { get; set; }
        public string Search { get; set; }
    }
}
