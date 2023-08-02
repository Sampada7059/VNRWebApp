using VNRWEBAPI.ViewModels;
namespace VNRWEBAPI.Repository
{
    public interface IUsersRepository
    {
        public List<string>? Login(LoginVM loginVm);//Method declaration
    }
}
