using System.Runtime.Serialization;

namespace WebApi.Models
{
    [DataContract]
    public class BaseResponse
    {
        [DataMember]
        public bool IsSuccess { get; set; }
    }
}
