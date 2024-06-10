using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FFella_TaskManager_CS.Models
{
    public partial class Task
    {
        public int      TaskId      { get; set; }

        [Required(ErrorMessage = "Due Date cannot be empty or blank")]
        [JsonPropertyName("duedate")]
        public DateTime DueDate     { get; set; }

        [Required]
        [JsonPropertyName("description")]
        public string?  Description { get; set; }

        [JsonPropertyName("iscomplete")]
        public bool?    Iscomplete  { get; set; }
    }
}
