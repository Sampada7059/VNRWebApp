using VNRWEBAPI.ViewModels;

namespace VNRWEBAPI.Repository
{
    public interface ILatestUpdateRepository
    {
        IEnumerable<LatestUpdateVM> GetAllUpdates();//Method declaration
        void AddLatestUpdate(LatestUpdateVM latestupdate);//Method declaration
    }
}
