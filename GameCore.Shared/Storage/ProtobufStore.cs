using System.IO;
using ProtoBuf;

namespace GameCore.Storage
{
    public class ProtobufStore
    {
        public static void SaveFile<T>(T protobufObject, string filePath)
        {
            using var file = File.Create(filePath);
            Serializer.Serialize(file, protobufObject);
        }

        public static T LoadFile<T>(string filePath)
        {
            using var file = File.OpenRead(filePath);
            return Serializer.Deserialize<T>(file);
        }
    }
}