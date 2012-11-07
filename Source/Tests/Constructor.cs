using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Constructor
    {
        // Implementation detail: the default capacity.
        const int DefaultCapacity = 8;

        [TestMethod]
        public void WithoutExplicitCapacity_UsesDefaultCapacity()
        {
            var deque = new Deque<int>();
            Assert.AreEqual(DefaultCapacity, deque.Capacity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void CapacityOf0_ThrowsException()
        {
            var deque = new Deque<int>(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void NegativeCapacity_ThrowsException()
        {
            var deque = new Deque<int>(-1);
        }

        [TestMethod]
        public void CapacityOf1_UsesSpecifiedCapacity()
        {
            var deque = new Deque<int>(1);
            Assert.AreEqual(1, deque.Capacity);
        }

        [TestMethod]
        public void FromEmptySequence_UsesDefaultCapacity()
        {
            var deque = new Deque<int>(new int[] { });
            Assert.AreEqual(DefaultCapacity, deque.Capacity);
        }

        [TestMethod]
        public void FromSequence_InitializesFromSequence()
        {
            var deque = new Deque<int>(new int[] { 1, 2, 3 });
            Assert.AreEqual(3, deque.Capacity);
            Assert.AreEqual(3, deque.Count);
            Assert.IsTrue(deque.SequenceEqual(new int[] { 1, 2, 3 }));
        }
    }
}
