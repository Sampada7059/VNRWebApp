using VNRWEBAPI.Models;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDatabaseContext _context;
        public StudentRepository(CollegeDatabaseContext context)//Constructor
        {
            _context = context;
        }

        //Roll Number Generation
        public void AddStudent(StudentVM studentvm)
        {
            
                    User user = new User();
                    Address address = new Address();
                    Student student = new Student();
                    String hashedPassword = "";
                    var results = (from r in _context.Students
                                   where r.Department == studentvm.Department
                                   orderby r.RollNumber descending
                                   let Roll_Number = r.RollNumber.Substring(8, 10)
                                   select new
                                   {
                                       Roll_Number
                                   }
                                ).FirstOrDefault();
                    int count;

                    if (results == null || results.Roll_Number == "")
                    {
                        count = 1;
                    }
                    else
                    {
                        count = Int32.Parse(results.Roll_Number) + 1;
                    }
                    String RollNumber = "4VN23" + studentvm.Department + Convert.ToString(count).PadLeft(3, '0');

                    user.Username = studentvm.Username;
                    hashedPassword = HashingHelper.HashPassword(studentvm.Password);
                    user.Password = hashedPassword;
                    user.Status = true;  // Set the status to true by default
                    user.UserType = 3;    // Set the user type to 2 by default
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    student.Name = studentvm.Name;
                    student.Gender = studentvm.Gender;
                    student.Department = studentvm.Department;
                    student.RollNumber = RollNumber;
                    student.Section = studentvm.Section;
                    student.Email = studentvm.Email;
                    student.PhoneNumber = studentvm.Phone_Number;
                    student.StudentFee = studentvm.Student_Fee;
                    student.UserId = user.Id;
                    _context.Students.Add(student);
                    _context.SaveChanges();

                    address.DoorNumber = studentvm.Door_Number;
                    address.Area = studentvm.Area;
                    address.City = studentvm.City;
                    address.Pincode = studentvm.Pincode;
                    address.StudentId = student.Id;
                    _context.Addresses.Add(address);
                    _context.SaveChanges();
                    
        }
            
    }
}
