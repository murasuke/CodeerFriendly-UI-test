using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ManualTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ManualTest()
        {
            // Show form, and test manualy
            TestTargetExeForm.Program.Main();
            
        }
    }
}
