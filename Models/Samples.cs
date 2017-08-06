using System;

namespace GeneByGeneCodingTask.Models
{
    public partial class Samples
    {
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? CreatedBy { get; set; }
        public int? StatusId { get; set; }

        public virtual Users CreatedByNavigation { get; set; }
        public virtual Statuses Status { get; set; }
    }
}
