using System.Runtime.Serialization;

namespace ChatApp.DataContracts
{
    [DataContract]
    public class CommandMessage
    {
        [DataMember]
        public string Command { get; set; }

        [DataMember]
        public string Parameter { get; set; }
    }
}