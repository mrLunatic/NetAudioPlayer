using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetAudioPlayer.Common.Attribute;
using NetAudioPlayer.Common.Message;
using Newtonsoft.Json;

namespace NetAudioPlayer.Common.Components
{
    public static class MessageParser
    {
        private static readonly IDictionary<string, Type> MesageTypes = new Dictionary<string, Type>();

        static MessageParser()
        {
            var types = typeof (MessageParser)
                .GetTypeInfo()
                .Assembly
                .DefinedTypes
                .ToArray();

            foreach (var typeInfo in types)
            {
                var attribute = typeInfo.GetCustomAttribute<MessageAttribute>();

                if (attribute != null)
                {      
                    MesageTypes.Add(typeInfo.Name, typeInfo.AsType());
                }
            }
        }  

        public static IMessage Parse(string message)
        {
            if (string.IsNullOrEmpty(message)) return null;

            IMessage msg = JsonConvert.DeserializeObject<MessageBase>(message);

            var type = MesageTypes[msg.Type];

            if (type != null)
            {
                return JsonConvert.DeserializeObject(message, type) as IMessage;
            }
            return null;

        }

        public static string Serialize(IMessage message)
        {
            if (message != null)
            {
                return JsonConvert.SerializeObject(message, Formatting.Indented);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
