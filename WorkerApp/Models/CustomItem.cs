using System.Runtime.Serialization;

namespace WorkerApp.Models
{
    [DataContract]
    public class CustomItem //This should be imported from a shared contracts package both in WebApi and WorkerApp projects
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
