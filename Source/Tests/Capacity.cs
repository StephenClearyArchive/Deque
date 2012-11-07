using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Capacity
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetTo0_ThrowsException()
        {
            var deque = new Deque<int>();
            deque.Capacity = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetNegative_ThrowsException()
        {
            var deque = new Deque<int>();
            deque.Capacity = -1;
        }

        [TestMethod]
        public void SetLarger_UsesSpecifiedCapacity()
        {
            var deque = new Deque<int>(1);
            Assert.AreEqual(1, deque.Capacity);
            deque.Capacity = 17;
            Assert.AreEqual(17, deque.Capacity);
        }

        [TestMethod]
        public void SetSmaller_UsesSpecifiedCapacity()
        {
            var deque = new Deque<int>(13);
            Assert.AreEqual(13, deque.Capacity);
            deque.Capacity = 7;
            Assert.AreEqual(7, deque.Capacity);
        }

        [TestMethod]
        public void Set_PreservesData()
        {
            var deque = new Deque<int>(new int[] { 1, 2, 3 });
            Assert.AreEqual(3, deque.Capacity);
            deque.Capacity = 7;
            Assert.AreEqual(7, deque.Capacity);
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 2, 3 }));
        }

        [TestMethod]
        public void Set_WhenSplit_PreservesData()
        {
            var deque = new Deque<int>(new int[] { 1, 2, 3 });
            deque.RemoveFromFront();
            deque.AddToBack(4);
            Assert.AreEqual(3, deque.Capacity);
            deque.Capacity = 7;
            Assert.AreEqual(7, deque.Capacity);
            Assert.IsTrue(deque.SequenceEqual(new[] { 2, 3, 4 }));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Set_SmallerThanCount_ThrowsException()
        {
            var deque = new Deque<int>(new int[] { 1, 2, 3 });
            Assert.AreEqual(3, deque.Capacity);
            deque.Capacity = 2;
        }

        [TestMethod]
        public void Set_ToItself_DoesNothing()
        {
            var deque = new Deque<int>(13);
            Assert.AreEqual(13, deque.Capacity);
            deque.Capacity = 13;
            Assert.AreEqual(13, deque.Capacity);
        }
    }
}
