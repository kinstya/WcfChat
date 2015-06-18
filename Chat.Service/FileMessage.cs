using System;
using System.Runtime.Serialization;

namespace ServiceAssembly
{
    [DataContract]
    public class FileMessage
    {
        [DataMember]
        public string Sender { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] Data { get; set; }

        [DataMember]
        public DateTime Time { get; set; }
    }
}