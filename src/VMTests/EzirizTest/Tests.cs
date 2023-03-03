using System.Reflection;
using NUnit.Framework;

namespace EzirizTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.IsTrue(I32() == 32);
        }


        [Obfuscation(Feature = "virtualization", Exclude = false)]
        public static int I32()
        {
            return 32;
        }
    }
}