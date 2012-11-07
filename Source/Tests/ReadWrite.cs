using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ReadWrite
    {
        [TestMethod]
        public void GetItem_ReadsElements()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            Assert.AreEqual(1, deque[0]);
            Assert.AreEqual(2, deque[1]);
            Assert.AreEqual(3, deque[2]);
        }

        [TestMethod]
        public void GetItem_Split_ReadsElements()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveFromBack();
            deque.AddToFront(0);
            Assert.AreEqual(0, deque[0]);
            Assert.AreEqual(1, deque[1]);
            Assert.AreEqual(2, deque[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void GetItem_IndexTooLarge_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            var _ = deque[3];
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void GetItem_NegativeIndex_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            var _ = deque[-1];
        }

        [TestMethod]
        public void SetItem_WritesElements()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque[0] = 7;
            deque[1] = 11;
            deque[2] = 13;
            Assert.IsTrue(deque.SequenceEqual(new[] { 7, 11, 13 }));
        }

        [TestMethod]
        public void SetItem_Split_WritesElements()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveFromBack();
            deque.AddToFront(0);
            deque[0] = 7;
            deque[1] = 11;
            deque[2] = 13;
            Assert.IsTrue(deque.SequenceEqual(new[] { 7, 11, 13 }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetItem_IndexTooLarge_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque[3] = 13;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void SetItem_NegativeIndex_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque[-1] = 13;
        }
    }
}
