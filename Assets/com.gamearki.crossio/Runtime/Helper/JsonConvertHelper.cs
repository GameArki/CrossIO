using System.Text;
using UnityEngine;

namespace GameArki.CrossIO {

    public static class JsonConvertHelper {

        public static byte[] Serialize<T>(T obj) {
            var str = JsonUtility.ToJson(obj);
            return Encoding.UTF8.GetBytes(str);
        }

        public static T Deserialize<T>(byte[] utf8Bytes) {
            var str = Encoding.UTF8.GetString(utf8Bytes);
            return JsonUtility.FromJson<T>(str);
        }

    }

}