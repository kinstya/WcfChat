namespace Chat.Service
{
    using System.ServiceModel;

    using Chat.Service.Contract;

    using WCF.CustomSerialization;

    [ServiceContract(CallbackContract = typeof (IChatCallback), SessionMode = SessionMode.Required)]
    [JsonSerializerContractBehavior]
    public interface IChat
    {
        [OperationContract(IsInitiating = true)]
        OperationResult Connect(Client client);

        [OperationContract(IsOneWay = true)]
        void Say(Message msg);

        [OperationContract(IsOneWay = true)]
        void Whisper(Message msg, Client receiver);

        [OperationContract(IsOneWay = true)]
        void IsWriting(Client client);

        [OperationContract(IsOneWay = true, IsTerminating = true)]
        void Disconnect(Client client);
    }
}