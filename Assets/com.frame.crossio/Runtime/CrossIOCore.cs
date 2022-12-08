using System.Threading.Tasks;
using UnityEngine;

namespace JackFrame.CrossIO {

    public static class CrossIOCore {

        public static void WriteToPersistent<T>(CrossIODataType dataType, string dir, string filename, T obj) {
            var bytes = ToBytes(dataType, obj);
            UnityFileHelper.WriteFileBytesToPersistentDataPath(dir, filename, bytes);
        }

        public async static Task<T> ReadFromPersistent<T>(CrossIODataType dataType, string dir, string filename) {
            var bytes = await UnityFileHelper.ReadFileBytesFromPersistentDataPathAsync(dir, filename);
            return FromBytes<T>(dataType, bytes);
        }

        public async static Task<T> ReadFromStreamingAssets<T>(CrossIODataType dataType, string dir, string filename) {
            var bytes = await UnityFileHelper.ReadFileBytesFromStreamingAssetsAsync(dir, filename);
            return FromBytes<T>(dataType, bytes);
        }

        static byte[] ToBytes<T>(CrossIODataType dataType, T obj) {
            switch (dataType) {
                case CrossIODataType.JsonUTF8:
                    return JsonConvertHelper.Serialize(obj);
                case CrossIODataType.ReflectionBinary:
                    return ReflectionBinaryConvertHelper.Serialize(obj);
                default:
                    return null;
            }
        }

        static T FromBytes<T>(CrossIODataType dataType, byte[] bytes) {
            switch (dataType) {
                case CrossIODataType.JsonUTF8:
                    return JsonConvertHelper.Deserialize<T>(bytes);
                case CrossIODataType.ReflectionBinary:
                    return ReflectionBinaryConvertHelper.Deserialize<T>(bytes);
                default:
                    return default(T);
            }
        }

    }

}