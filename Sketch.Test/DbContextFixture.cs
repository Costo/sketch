using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketch.Core.Database;

namespace Sketch.Test
{
    [TestClass]
    public class DbContextFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new SketchDbContext())
            {
            }
        }
    }
}
