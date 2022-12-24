using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace GameArki.CrossIO.Tests {

    public class CrossIOTests {

        [UnityTest]
        public IEnumerator CrossIOTestsWithEnumeratorPasses() {
            var obj1 = TestClass.GenRandom();
            CrossIOCore.WriteToPersistent(CrossIODataType.ReflectionBinary, "CrossIOTest", "myobj", obj1);
            bool isEnd = false;
            Action action = async () => {
                TestClass obj2 = await CrossIOCore.ReadFromPersistentAsync<TestClass>(CrossIODataType.ReflectionBinary, "CrossIOTest", "myobj");
                Assert.That(obj2.boolValue, Is.EqualTo(obj1.boolValue));
                Assert.That(obj2.boolArray, Is.EqualTo(obj1.boolArray));
                Assert.That(obj2.byteValue, Is.EqualTo(obj1.byteValue));
                Assert.That(obj2.byteArray, Is.EqualTo(obj1.byteArray));
                Assert.That(obj2.charValue, Is.EqualTo(obj1.charValue));
                Assert.That(obj2.doubleValue, Is.EqualTo(obj1.doubleValue));
                Assert.That(obj2.doubleArray, Is.EqualTo(obj1.doubleArray));
                Assert.That(obj2.floatValue, Is.EqualTo(obj1.floatValue));
                Assert.That(obj2.floatArray, Is.EqualTo(obj1.floatArray));
                Assert.That(obj2.intValue, Is.EqualTo(obj1.intValue));
                Assert.That(obj2.intArray, Is.EqualTo(obj1.intArray));
                Assert.That(obj2.longValue, Is.EqualTo(obj1.longValue));
                Assert.That(obj2.longArray, Is.EqualTo(obj1.longArray));
                Assert.That(obj2.sbyteValue, Is.EqualTo(obj1.sbyteValue));
                Assert.That(obj2.sbyteArray, Is.EqualTo(obj1.sbyteArray));
                Assert.That(obj2.shortValue, Is.EqualTo(obj1.shortValue));
                Assert.That(obj2.shortArray, Is.EqualTo(obj1.shortArray));
                Assert.That(obj2.stringValue, Is.EqualTo(obj1.stringValue));
                Assert.That(obj2.stringArray, Is.EqualTo(obj1.stringArray));
                Assert.That(obj2.uintValue, Is.EqualTo(obj1.uintValue));
                Assert.That(obj2.uintArray, Is.EqualTo(obj1.uintArray));
                Assert.That(obj2.ulongValue, Is.EqualTo(obj1.ulongValue));
                Assert.That(obj2.ulongArray, Is.EqualTo(obj1.ulongArray));
                Assert.That(obj2.ushortValue, Is.EqualTo(obj1.ushortValue));
                Assert.That(obj2.ushortArray, Is.EqualTo(obj1.ushortArray));
                isEnd = true;
            };
            action.Invoke();
            while (!isEnd) {
                yield return null;
            }
        }

    }
}