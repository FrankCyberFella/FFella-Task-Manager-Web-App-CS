using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/****************************************************************************************
 * Class for object return from data source
 ***************************************************************************************/
namespace FFella_TaskManager_CS.Models
{
    public partial class Task
    {
        public int      TaskId      { get; set; }

        [Required(ErrorMessage = "Due Date cannot be empty or blank")]
        [JsonPropertyName("duedate")]              // Relate JSON property to object property since names differ
        public DateTime DueDate     { get; set; }

        [Required]
        [JsonPropertyName("description")]           // Relate JSON property to object property since names differ
        public string?  Description { get; set; }

        [JsonPropertyName("iscomplete")]             // Relate JSON property to object property since names differ
        public bool?    Iscomplete  { get; set; }
    }
}
