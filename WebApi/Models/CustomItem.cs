using System.Runtime.Serialization;

namespace WebApi.Models
{
    [DataContract]
    public class CustomItem
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
