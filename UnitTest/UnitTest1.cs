using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestTargetExeForm;


namespace UnitTest
{
    [TestClass]
    public class TestCalcLogic
    {
        [TestMethod]
        public void TestAdd()
        {
            Assert.AreEqual(CalcLogic.Add(1, 2), 3);
        }

        [TestMethod]
        public void TestSub()
        {
            Assert.AreEqual(CalcLogic.Sub(1, 2), -1);
        }

        [TestMethod]
        public void TestMul()
        {
            Assert.AreEqual(CalcLogic.Mul(1, 2), 2);
        }

        [TestMethod]
        public void TestDiv()
        {
            Assert.AreEqual(CalcLogic.Div(1, 2), 0.5M);
            Assert.AreEqual(CalcLogic.Div(1, 0), 0M); // 分岐を100%にしないためにコメントアウト
        }
    }
}
