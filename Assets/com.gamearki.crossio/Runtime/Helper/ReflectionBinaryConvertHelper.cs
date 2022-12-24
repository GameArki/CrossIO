using System;
using System.Linq;
using System.Reflection;

namespace GameArki.CrossIO {

    public static class ReflectionBinaryConvertHelper {

        public static byte[] Serialize<T>(T obj) {
            return ReflectionSerializeUtil.Serialize(obj);
        }

        public static T Deserialize<T>(byte[] bytes) {
            return ReflectionSerializeUtil.Deserialize<T>(bytes);
        }

    }

}