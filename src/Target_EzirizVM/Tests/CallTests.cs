using System.Reflection;
using NUnit.Framework;

namespace Target_EzirizVM.Tests
{
    [TestFixture]
    public class EzirizCallTests
    {
        [Test]
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        public void Test1()
        {
            Test1_Called();
            var instanceClass = new EzirizVMCall_Test1_InstanceClass();

            Assert.AreEqual(instanceClass.Test1_Called_OtherClass(1234), "1234");
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private void Test1_Called()
        {
        }

        [Obfuscation(Feature = "virtualization", Exclude = false)]
        private static void Test1_Called_Static()
        {
        }
    }

    internal class EzirizVMCall_Test1_InstanceClass
    {
        [Obfuscation(Feature = "virtualization", Exclude = false)]
        public string Test1_Called_OtherClass(int someParam)
        {
            return someParam.ToString();
        }
    }
}