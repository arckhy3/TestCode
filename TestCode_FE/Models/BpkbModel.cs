using Microsoft.AspNetCore.Mvc.Rendering;

namespace TestCode_FE.Models
{
    public class BpkbModel
    {
        public string agreement_number { get; set; }
        public string bpkb_no { get; set; }
        public string branch_id { get; set; }
        public DateTime bpkb_date { get; set; } = DateTime.Today;
        public string faktur_no { get; set; }
        public DateTime faktur_date { get; set; } = DateTime.Today;
        public string location_id { get; set; }
        public string police_no { get; set; }
        public DateTime bpkb_date_in { get; set; } = DateTime.Today;
        public string created_by { get; set; }
        public DateTime created_on { get; set; } = DateTime.Today;
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; } = DateTime.Today;

        public List<SelectListItem> Locations { get; set; }
    }
}
