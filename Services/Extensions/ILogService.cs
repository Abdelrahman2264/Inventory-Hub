namespace InventorySystem.Services.Extensions
{
    public interface ILogService
    {
        Task CreateLogAsync(int assetId, string changeDescription,string Logtype,string DeviceType , int userid = 0);
        public  Task<int> ReturnCurrentUserID();

    }

}
