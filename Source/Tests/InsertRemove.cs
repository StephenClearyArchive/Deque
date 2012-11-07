using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito;
using System.Diagnostics.CodeAnalysis;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InsertRemove
    {
        [TestMethod]
        public void Clear_EmptiesAllItems()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.Clear();
            Assert.AreEqual(0, deque.Count);
            Assert.IsTrue(deque.SequenceEqual(new int[] { }));
        }

        [TestMethod]
        public void Clear_DoesNotChangeCapacity()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            Assert.AreEqual(3, deque.Capacity);
            deque.Clear();
            Assert.AreEqual(3, deque.Capacity);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveFromFront_Empty_ThrowsException()
        {
            var deque = new Deque<int>();
            deque.RemoveFromFront();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveFromBack_Empty_ThrowsException()
        {
            var deque = new Deque<int>();
            deque.RemoveFromBack();
        }

        [TestMethod]
        public void Remove_ItemPresent_RemovesItemAndReturnsTrue()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3, 4 });
            var result = deque.Remove(3);
            Assert.IsTrue(result);
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 2, 4 }));
        }

        [TestMethod]
        public void Remove_ItemNotPresent_KeepsItemsReturnsFalse()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3, 4 });
            var result = deque.Remove(5);
            Assert.IsFalse(result);
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 2, 3, 4 }));
        }

        [TestMethod]
        public void Insert_InsertsElementAtIndex()
        {
            var deque = new Deque<int>(new[] { 1, 2 });
            deque.Insert(1, 13);
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 13, 2 }));
        }

        [TestMethod]
        public void Insert_AtIndex0_IsSameAsAddToFront()
        {
            var deque1 = new Deque<int>(new[] { 1, 2 });
            var deque2 = new Deque<int>(new[] { 1, 2 });
            deque1.Insert(0, 0);
            deque2.AddToFront(0);
            Assert.IsTrue(deque1.SequenceEqual(deque2));
        }

        [TestMethod]
        public void Insert_AtCount_IsSameAsAddToBack()
        {
            var deque1 = new Deque<int>(new[] { 1, 2 });
            var deque2 = new Deque<int>(new[] { 1, 2 });
            deque1.Insert(deque1.Count, 0);
            deque2.AddToBack(0);
            Assert.IsTrue(deque1.SequenceEqual(deque2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Insert_NegativeIndex_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.Insert(-1, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Insert_IndexTooLarge_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.Insert(deque.Count + 1, 0);
        }

        [TestMethod]
        public void RemoveAt_RemovesElementAtIndex()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveFromBack();
            deque.AddToFront(0);
            deque.RemoveAt(1);
            Assert.IsTrue(deque.SequenceEqual(new[] { 0, 2 }));
        }

        [TestMethod]
        public void RemoveAt_Index0_IsSameAsRemoveFromFront()
        {
            var deque1 = new Deque<int>(new[] { 1, 2 });
            var deque2 = new Deque<int>(new[] { 1, 2 });
            deque1.RemoveAt(0);
            deque2.RemoveFromFront();
            Assert.IsTrue(deque1.SequenceEqual(deque2));
        }

        [TestMethod]
        public void RemoveAt_LastIndex_IsSameAsRemoveFromFront()
        {
            var deque1 = new Deque<int>(new[] { 1, 2 });
            var deque2 = new Deque<int>(new[] { 1, 2 });
            deque1.RemoveAt(1);
            deque2.RemoveFromBack();
            Assert.IsTrue(deque1.SequenceEqual(deque2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void RemoveAt_NegativeIndex_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveAt(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void RemoveAt_IndexTooLarge_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveAt(deque.Count);
        }
    }
}
