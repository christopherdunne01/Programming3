using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace COM377CATMOUSEGAME
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            CatController cat = new CatController();
            cat.Paused = false;

            //Act
            cat.Pause();

            //Assert
            Assert.IsTrue(cat.Paused == true);
        }



        [TestMethod]
        public void TestMethod2()
        {
            //Arrange
            CatController cat = new CatController();
            cat.Paused = true;

            //Act
            cat.UnPause();

            //Assert
            Assert.IsTrue(cat.Paused == false);
        }
    }
}
