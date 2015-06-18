namespace Chat.Service.Contract
{
    using System.Runtime.Serialization;

    [DataContract]
    public class OperationResult
    {
        [DataMember]
        public bool Ok { get; set; }
    }
}