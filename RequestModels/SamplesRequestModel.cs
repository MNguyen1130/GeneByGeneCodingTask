using Newtonsoft.Json;

namespace GeneByGeneCodingTask.RequestModels
{
    public class SamplesRequestModel
    {
        public int? StatusId { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public SamplesRequestModel(int? statusId, string name)
        {
            StatusId = statusId;
            Name = name;
        }
    }
}
