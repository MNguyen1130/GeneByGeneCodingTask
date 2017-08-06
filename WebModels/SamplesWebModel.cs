using GeneByGeneCodingTask.Models;
using System;

namespace GeneByGeneCodingTask.WebModels
{
    public class SamplesWebModel
    {
        public int SampleId { get; set; }
        public string Barcode { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int StatusId { get; set; }
        public string Status { get; set; }

        public SamplesWebModel(Samples sample)
        {
            SampleId = sample.SampleId;
            Barcode = sample.Barcode;
            CreatedAt = sample.CreatedAt.Value;

            UserId = sample.CreatedByNavigation.UserId;
            FirstName = sample.CreatedByNavigation.FirstName;
            LastName = sample.CreatedByNavigation.LastName;

            StatusId = sample.Status.StatusId;
            Status = sample.Status.Status;
        }
    }
}
