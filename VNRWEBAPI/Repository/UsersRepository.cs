using VNRWEBAPI.Models;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Repository
{
    public class UsersRepository: IUsersRepository
    {
        
        
		private readonly CollegeDatabaseContext _context;//declaring private variable
        public UsersRepository(CollegeDatabaseContext dbContext)//Constructor 
        {
            _context = dbContext;
        }
        public List<string>? Login(LoginVM loginVm)
        {
            string hashedPassword = HashingHelper.HashPassword(loginVm.Password);//Password hashing
            var user = _context.Users.FirstOrDefault(u => u.Username == loginVm.UserName && u.Password== hashedPassword);//Comparing login credential
            /*Retrieving the Name of particular user*/
            List<string> LoginValue = new List<string>();
            if (user != null)
                {
                    var adminName = "Admin";
                    var teacherName = "";
                    var studentName = "";
                    teacherName =
                    (from teacher in _context.Teachers
                     where teacher.UserId == user.Id
                     select teacher.Name
                    ).FirstOrDefault();
                    studentName = (
                    from student in _context.Students
                    where student.UserId == user.Id
                    select student.Name
                    ).FirstOrDefault();

                    var NameOfUser = teacherName ?? studentName ?? adminName;
                    //if (user.Username == loginVm.UserName && user.Password == hashedPassword)
                    //{
                        LoginValue.Add(NameOfUser);
                        LoginValue.Add(user.Username);
                        LoginValue.Add(user.UserType.ToString());
                        LoginValue.Add(user.Id.ToString());
                    //}
                return LoginValue;
            }
            return null;
        }   
    }
}
