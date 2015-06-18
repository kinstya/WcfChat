using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service
{
    using Chat.DAL.Info;
    using Chat.Service.Contract;

    using Microsoft.Practices.Unity.Utility;

    public static class ContractExtensions
    {
        public static ClientInfo ToInfo(this Client client)
        {
            Guard.ArgumentNotNull(client, "client");
            return new ClientInfo
                       {
                           Name = client.Name,
                           Time = client.Time
                       };
        }

        public static MessageInfo ToInfo(this Message message)
        {
            Guard.ArgumentNotNull(message, "message");
            return new MessageInfo
            {
                Content = message.Content,
                Sender = message.Sender,
                Time = message.Time
            };
        }
    }
}
