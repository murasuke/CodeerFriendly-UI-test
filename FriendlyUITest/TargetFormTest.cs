using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Codeer.Friendly.Windows;
using System.Diagnostics;
using Codeer.Friendly.Dynamic;

namespace FriendlyUITest
{
    [TestClass]
    public class TargetFormTest
    {

        System.Diagnostics.Process _process;

        [STAThread]
        public static void Main()
        {
            Console.WriteLine(System.Environment.CurrentDirectory);
            var pr = Process.Start(Path.Combine(System.Environment.CurrentDirectory, "../../../bin/TestTargetExeForm.exe"));
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _process = Process.Start(Path.Combine(System.Environment.CurrentDirectory, "../../../bin/TestTargetExeForm.exe"));
            while (_process.MainWindowHandle == IntPtr.Zero)
            {
                _process.Refresh();
                Thread.Sleep(10);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _process.CloseMainWindow();
            _process.WaitForExit();
        }

        [TestMethod]
        public void TestUIAdd()
        {
            ////--- プロセスにアタッチ
            using (var targetApp = new WindowsAppFriend(_process))
            {
                dynamic form = targetApp.Type<Application>().OpenForms[0];

                form.textBox1.Text = "1";
                form.textBox2.Text = "2";

                form.button1.PerformClick();

                Assert.AreEqual((string)form.textBox3.Text, "3");
            }
        }


        [TestMethod]
        public void TestUISub()
        {
            ////--- プロセスにアタッチ
            using (var targetApp = new WindowsAppFriend(_process))
            {
                dynamic form = targetApp.Type<Application>().OpenForms[0];
                form.rdoSub.Checked = true;
                form.textBox1.Text = "1";
                form.textBox2.Text = "2";


                form.button1.PerformClick();

                Assert.AreEqual((string)form.textBox3.Text, "-1");
            }
        }

        [TestMethod]
        public void TestUIMul()
        {
            ////--- プロセスにアタッチ
            using (var targetApp = new WindowsAppFriend(_process))
            {
                dynamic form = targetApp.Type<Application>().OpenForms[0];
                form.rdoMul.Checked = true;
                form.textBox1.Text = "1";
                form.textBox2.Text = "2";


                form.button1.PerformClick();

                Assert.AreEqual((string)form.textBox3.Text, "2");
            }
        }

        [TestMethod]
        public void TestUIDiv()
        {
            ////--- プロセスにアタッチ
            using (var targetApp = new WindowsAppFriend(_process))
            {
                dynamic form = targetApp.Type<Application>().OpenForms[0];
                form.rdoDiv.Checked = true;
                form.textBox1.Text = "1";
                form.textBox2.Text = "2";


                form.button1.PerformClick();

                Assert.AreEqual((string)form.textBox3.Text, "0.5");
            }
        }

        [TestMethod]
        public void TestUIDiv0()
        {
            ////--- プロセスにアタッチ
            using (var targetApp = new WindowsAppFriend(_process))
            {
                dynamic form = targetApp.Type<Application>().OpenForms[0];
                form.rdoDiv.Checked = true;
                form.textBox1.Text = "1";
                form.textBox2.Text = "0";


                form.button1.PerformClick();

                Assert.AreEqual((string)form.textBox3.Text, "0");
            }
        }
    }
}
