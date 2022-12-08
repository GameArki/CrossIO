# CrossIO
Unity 文件存取，适用于 Win/iOS/Android

# 依赖
JackFrame.BufferIO: https://github.com/chenwansal/BufferIO

# 功能
1. 将任意对象存为: 二进制文件、Json字符串文件  
2. 从二进制文件、Json字符串文件转化为原对象  

# 如何使用
``` C#
YourClass obj1 = new YourClass();
CrossIOCore.WriteToNormalDir(CrossIODataType.ReflectionBinary, "dir", "filename", obj1);
YourClass obj2 = CrossIOCore.ReadFromNormalDir<YourClass>(CrossIODataType.ReflectionBinary, "dir", "filename");

// or
CrossIOCore.WriteToPersistent(CrossIODataType.ReflectionBinary, "dir", "filename", obj1);
YourClass obj3 = await CrossIOCore.ReadFromPersistentAsync<YourClass>(CrossIODataType.ReflectionBinary, "dir", "filename");

```

# 说明
二进制转换时, 使用的是反射, 因此重视性能的项目请斟酌使用  

# 使用场景
1. (游戏)存档  
2. 配置文件  
