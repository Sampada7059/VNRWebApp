using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VNRWEBAPI;
using VNRWEBAPI.Models;
using VNRWEBAPI.Repository;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly CollegeDatabaseContext _dbContext;//declaring private variable

        public TeacherRepository(CollegeDatabaseContext dbContext)//Constructor 
        {
            _dbContext = dbContext;
        }
        
        
        //Employee Id generation
        public void AddTeacher(TeacherVM teachervm)
        {
            string HashedPassword = "";
            User user = new User();
            Teacher teacher = new Teacher();
            Address address = new Address();
            var results = (from tt in _dbContext.Teachers
                           orderby tt.EmployeeId descending
                           let EmployeeId = tt.EmployeeId.Substring(3, 6)
                           select new
                           {
                               EmployeeId
                           }
                 ).FirstOrDefault();
            int count;
            if (results == null)
            {
                count = 1;
            }
            else
            {
                count = Int32.Parse(results.EmployeeId) + 1;
            }
            String EmployeeID = "VNR" + Convert.ToString(count).PadLeft(3, '0');
            user.Username = teachervm.Username;
            HashedPassword = HashingHelper.HashPassword(teachervm.Password);
            user.Password = HashedPassword;
            user.Status = true;  // Set the status to true by default
            user.UserType = 2;    // Set the user type to 2 by default
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            teacher.Name = teachervm.Name;
            teacher.Gender = teachervm.Gender;
            teacher.Department = teachervm.Department;
            teacher.EmployeeId = EmployeeID;
            teacher.Email = teachervm.Email;
            teacher.PhoneNumber = teachervm.Phone_Number;
            teacher.UserId = user.Id;
            _dbContext.Teachers.Add(teacher);
            _dbContext.SaveChanges();

            address.DoorNumber = teachervm.Door_Number;
            address.Street = teachervm.Street;
            address.Area = teachervm.Area;
            address.City = teachervm.City;
            address.Pincode = teachervm.Pincode;
            address.TeacherId = teacher.Id;
            _dbContext.Addresses.Add(address);
            _dbContext.SaveChanges();

        }
    }
}
