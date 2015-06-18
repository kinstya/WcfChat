namespace WCF.CustomSerialization
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;
    using System.ServiceModel.Dispatcher;
    using System.Xml;

    public class JsonSerializerContractBehaviorAttribute : Attribute, IContractBehavior
    {
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            this.ReplaceSerializerOperationBehavior(contractDescription);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            this.ReplaceSerializerOperationBehavior(contractDescription);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
            foreach (OperationDescription operation in contractDescription.Operations)
            {
                foreach (MessageDescription message in operation.Messages)
                {
                    this.ValidateMessagePartDescription(message.Body.ReturnValue);
                    foreach (MessagePartDescription part in message.Body.Parts)
                    {
                        this.ValidateMessagePartDescription(part);
                    }

                    foreach (MessageHeaderDescription header in message.Headers)
                    {
                        this.ValidateCustomSerializableType(header.Type);
                    }
                }
            }
        }

        private void ValidateMessagePartDescription(MessagePartDescription part)
        {
            if (part != null)
            {
                this.ValidateCustomSerializableType(part.Type);
            }
        }

        private void ValidateCustomSerializableType(Type type)
        {
            if (!type.IsPublic)
            {
                throw new InvalidOperationException("Json serialization is supported in public types only");
            }

            ConstructorInfo defaultConstructor = type.GetConstructor(new Type[0]);
            if (defaultConstructor == null && !type.IsPrimitive)
            {
                throw new InvalidOperationException("Json serializable types must have a public, parameterless constructor");
            }
        }

        private void ReplaceSerializerOperationBehavior(ContractDescription contract)
        {
            foreach (OperationDescription od in contract.Operations)
            {
                for (int i = 0; i < od.Behaviors.Count; i++)
                {
                    DataContractSerializerOperationBehavior dcsob = od.Behaviors[i] as DataContractSerializerOperationBehavior;
                    if (dcsob != null)
                    {
                        od.Behaviors[i] = new JsonSerializerOperationBehavior(od);
                    }
                }
            }
        }

        private class JsonSerializerOperationBehavior : DataContractSerializerOperationBehavior
        {
            public JsonSerializerOperationBehavior(OperationDescription operation) : base(operation) { }
            public override XmlObjectSerializer CreateSerializer(Type type, string name, string ns, IList<Type> knownTypes)
            {
                return new DataContractJsonSerializer(type, knownTypes);
            }

            public override XmlObjectSerializer CreateSerializer(Type type, XmlDictionaryString name, XmlDictionaryString ns, IList<Type> knownTypes)
            {
                return new DataContractJsonSerializer(type, knownTypes);
            }
        }
    }
}
