using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using VNRWEBAPI.Models;
using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Repository
{
    public class LatestUpdateRepository : ILatestUpdateRepository
    {
        private readonly CollegeDatabaseContext _context;
        public LatestUpdateRepository(CollegeDatabaseContext context)//Constructor
        {
            _context = context;
        }
        public IEnumerable<LatestUpdateVM> GetAllUpdates()
        {
            /*Query is for return Information(Message) followed by Name of User, if Update is created by Today's Date*/
               var query = (
                    from u in _context.LatestUpdates
                    where u.Createdby == 10 && u.CreatedDate.Date == DateTime.Today
                    select new LatestUpdateVM
                    {
                        Information = u.Information,
                        Name = "Admin"
                    }
                ).ToList()
                 .Union(
                     from u in _context.LatestUpdates
                     join t in _context.Teachers on u.Createdby equals t.UserId into teacherGroup
                     from t in teacherGroup.DefaultIfEmpty()
                     join s in _context.Students on u.Createdby equals s.UserId into studentGroup
                     from s in studentGroup.DefaultIfEmpty()
                     where u.Createdby != 10 && u.CreatedDate.Date == DateTime.Today
                     select new LatestUpdateVM
                     {
                         Information = u.Information,
                         Name = t.Name ?? s.Name
                     }
                 ).ToList();
            return query;
        }
        public void AddLatestUpdate(LatestUpdateVM latestUpdateVM)
        {
            LatestUpdate latest = new LatestUpdate();
                    latest.Information = latestUpdateVM.Information;
                    latestUpdateVM.Created_Date = DateTime.Now;
                    latest.CreatedDate = latestUpdateVM.Created_Date;
                    latest.Createdby = latestUpdateVM.CreatedBy;
                    _context.LatestUpdates.Add(latest);
                    _context.SaveChanges();
        }
    }
}

