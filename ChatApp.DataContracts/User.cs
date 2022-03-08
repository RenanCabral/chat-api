using System.Runtime.Serialization;

namespace ChatApp.DataContracts
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}