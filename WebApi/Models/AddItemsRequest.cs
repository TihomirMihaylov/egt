using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace WebApi.Models
{
    [DataContract]
    public class AddItemsRequest
    {
        [DataMember]
        [Required]
        public IEnumerable<CustomItem> Items { get; set; }
    }
}
