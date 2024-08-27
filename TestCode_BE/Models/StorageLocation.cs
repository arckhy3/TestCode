using System.ComponentModel.DataAnnotations;

namespace TestCode_BE.Models
{
    public class StorageLocation
    {
        [Key]
        public string location_id { get; set; }

        public string location_name { get; set; }
    }
}
