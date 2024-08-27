using System.Security.Cryptography.X509Certificates;

namespace TestCode_FE.Models
{
    public class StorageLocationModel
    {
        public string location_id { get; set; }
        public string location_name { get; set; }
    }

    public class LoginResponse
    {
        public string message { get; set; }
        public List<StorageLocationModel> storageLocations { get; set; }
    }
}
