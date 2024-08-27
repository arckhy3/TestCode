using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace TestCode_BE.Models
{
    public class User
    {
        [Key]
        public Int64 user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public bool is_active { get; set; }
    }
}
