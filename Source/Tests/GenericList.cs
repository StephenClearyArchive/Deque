using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GenericList
    {
        [TestMethod]
        public void IndexOf_ItemPresent_ReturnsItemIndex()
        {
            var deque = new Deque<int>(new[] { 1, 2 });
            var result = deque.IndexOf(2);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void IndexOf_ItemNotPresent_ReturnsNegativeOne()
        {
            var deque = new Deque<int>(new[] { 1, 2 });
            var result = deque.IndexOf(3);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void IndexOf_ItemPresentAndSplit_ReturnsItemIndex()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveFromBack();
            deque.AddToFront(0);
            Assert.AreEqual(0, deque.IndexOf(0));
            Assert.AreEqual(1, deque.IndexOf(1));
            Assert.AreEqual(2, deque.IndexOf(2));
        }

        [TestMethod]
        public void Contains_ItemPresent_ReturnsTrue()
        {
            var deque = new Deque<int>(new[] { 1, 2 });
            Assert.IsTrue(deque.Contains(2));
        }

        [TestMethod]
        public void Contains_ItemNotPresent_ReturnsFalse()
        {
            var deque = new Deque<int>(new[] { 1, 2 });
            Assert.IsFalse(deque.Contains(3));
        }

        [TestMethod]
        public void Contains_ItemPresentAndSplit_ReturnsTrue()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveFromBack();
            deque.AddToFront(0);
            Assert.IsTrue(deque.Contains(0));
            Assert.IsTrue(deque.Contains(1));
            Assert.IsTrue(deque.Contains(2));
            Assert.IsFalse(deque.Contains(3));
        }

        [TestMethod]
        public void Add_IsAddToBack()
        {
            var deque1 = new Deque<int>(new[] { 1, 2 });
            var deque2 = new Deque<int>(new[] { 1, 2 });
            ((ICollection<int>)deque1).Add(3);
            deque2.AddToBack(3);
            Assert.IsTrue(deque1.SequenceEqual(deque2));
        }

        [TestMethod]
        public void NonGenericEnumerator_EnumeratesItems()
        {
            var deque = new Deque<int>(new[] { 1, 2 });
            var results = new List<object>();
            var objEnum = ((System.Collections.IEnumerable)deque).GetEnumerator();
            while (objEnum.MoveNext())
            {
                results.Add(objEnum.Current);
            }
            Assert.IsTrue(deque.SequenceEqual(results.Cast<int>()));
        }

        [TestMethod]
        public void GenericIsReadOnly_ReturnsFalse()
        {
            var deque = new Deque<int>();
            Assert.IsFalse(((ICollection<int>)deque).IsReadOnly);
        }

        [TestMethod]
        public void CopyTo_CopiesItems()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            var results = new int[3];
            ((ICollection<int>)deque).CopyTo(results, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            ((ICollection<int>)deque).CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_NegativeOffset_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            var results = new int[3];
            ((ICollection<int>)deque).CopyTo(results, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_InsufficientSpace_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            var results = new int[3];
            ((ICollection<int>)deque).CopyTo(results, 1);
        }
    }
}
