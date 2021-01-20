using System;
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

        public static string Serialize<T>(T protobufObject)
        {
            var memoryStream = new MemoryStream();
            Serializer.Serialize(memoryStream, protobufObject);
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public static T Deserialize<T>(string encodedObject)
        {
            return Serializer.Deserialize<T>(new MemoryStream(Convert.FromBase64String(encodedObject)));
        }
    }
}