using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito;
using System.Diagnostics.CodeAnalysis;
using System.Collections;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ObjectList
    {
        [TestMethod]
        public void IndexOf_ItemPresent_ReturnsItemIndex()
        {
            var deque = new Deque<int>(new[] { 1, 2 }) as IList;
            var result = deque.IndexOf(2);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void IndexOf_ItemNotPresent_ReturnsNegativeOne()
        {
            var deque = new Deque<int>(new[] { 1, 2 }) as IList;
            var result = deque.IndexOf(3);
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void IndexOf_ItemPresentAndSplit_ReturnsItemIndex()
        {
            var deque_ = new Deque<int>(new[] { 1, 2, 3 });
            deque_.RemoveFromBack();
            deque_.AddToFront(0);
            var deque = deque_ as IList;
            Assert.AreEqual(0, deque.IndexOf(0));
            Assert.AreEqual(1, deque.IndexOf(1));
            Assert.AreEqual(2, deque.IndexOf(2));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void IndexOf_WrongItemType_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2 }) as IList;
            deque.IndexOf(this);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Contains_WrongItemType_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2 }) as IList;
            deque.Contains(this);
        }

        [TestMethod]
        public void Contains_ItemPresent_ReturnsTrue()
        {
            var deque = new Deque<int>(new[] { 1, 2 }) as IList;
            Assert.IsTrue(deque.Contains(2));
        }

        [TestMethod]
        public void Contains_ItemNotPresent_ReturnsFalse()
        {
            var deque = new Deque<int>(new[] { 1, 2 }) as IList;
            Assert.IsFalse(deque.Contains(3));
        }

        [TestMethod]
        public void Contains_ItemPresentAndSplit_ReturnsTrue()
        {
            var deque_ = new Deque<int>(new[] { 1, 2, 3 });
            deque_.RemoveFromBack();
            deque_.AddToFront(0);
            var deque = deque_ as IList;
            Assert.IsTrue(deque.Contains(0));
            Assert.IsTrue(deque.Contains(1));
            Assert.IsTrue(deque.Contains(2));
            Assert.IsFalse(deque.Contains(3));
        }

        [TestMethod]
        public void IsReadOnly_ReturnsFalse()
        {
            var deque = new Deque<int>() as IList;
            Assert.IsFalse(deque.IsReadOnly);
        }

        [TestMethod]
        public void CopyTo_CopiesItems()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 }) as IList;
            var results = new int[3];
            deque.CopyTo(results, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 }) as IList;
            deque.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_NegativeOffset_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 }) as IList;
            var results = new int[3];
            deque.CopyTo(results, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_InsufficientSpace_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 }) as IList;
            var results = new int[3];
            deque.CopyTo(results, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_WrongType_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 }) as IList;
            var results = new TestContext[3];
            deque.CopyTo(results, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_MultidimensionalArray_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 }) as IList;
            var results = new int[3, 3];
            deque.CopyTo(results, 0);
        }

        [TestMethod]
        public void NullableType_AllowsInsertingNull()
        {
            var deque = new Deque<int?>() as IList;
            var result = deque.Add(null);
            Assert.AreEqual(0, result);
            Assert.IsTrue(deque.Cast<int?>().SequenceEqual(new int?[] { null }));
        }

        [TestMethod]
        public void ClassType_AllowsInsertingNull()
        {
            var deque = new Deque<object>() as IList;
            var result = deque.Add(null);
            Assert.AreEqual(0, result);
            Assert.IsTrue(deque.Cast<object>().SequenceEqual(new object[] { null }));
        }

        [TestMethod]
        public void InterfaceType_AllowsInsertingNull()
        {
            var deque = new Deque<IList>() as IList;
            var result = deque.Add(null);
            Assert.AreEqual(0, result);
            Assert.IsTrue(deque.Cast<IList>().SequenceEqual(new IList[] { null }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Struct_InsertNull_ThrowsException()
        {
            var deque = new Deque<int>() as IList;
            deque.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void GenericStruct_InsertNull_ThrowsException()
        {
            var deque = new Deque<KeyValuePair<int, int>>() as IList;
            deque.Add(null);
        }

        [TestMethod]
        public void IsFixedSize_IsFalse()
        {
            var deque = new Deque<int>() as IList;
            Assert.IsFalse(deque.IsFixedSize);
        }

        [TestMethod]
        public void IsSynchronized_IsFalse()
        {
            var deque = new Deque<int>() as IList;
            Assert.IsFalse(deque.IsSynchronized);
        }

        [TestMethod]
        public void SyncRoot_IsSelf()
        {
            var deque = new Deque<int>() as IList;
            Assert.AreSame(deque, deque.SyncRoot);
        }

        [TestMethod]
        public void Insert_InsertsItem()
        {
            var deque = new Deque<int>() as IList;
            deque.Insert(0, 7);
            Assert.IsTrue(deque.Cast<int>().SequenceEqual(new[] { 7 }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Insert_WrongType_ThrowsException()
        {
            var deque = new Deque<int>() as IList;
            deque.Insert(0, this);
        }

        [TestMethod]
        public void Remove_RemovesItem()
        {
            var deque = new Deque<int>(new[] { 13 }) as IList;
            deque.Remove(13);
            Assert.IsTrue(deque.Cast<int>().SequenceEqual(new int[] { }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Remove_WrongType_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 13 }) as IList;
            deque.Remove(this);
        }

        [TestMethod]
        public void Get_GetsItem()
        {
            var deque = new Deque<int>(new[] { 13 }) as IList;
            var value = (int)deque[0];
            Assert.AreEqual(13, value);
        }

        [TestMethod]
        public void Set_SetsItem()
        {
            var deque = new Deque<int>(new[] { 13 }) as IList;
            deque[0] = 7;
            Assert.IsTrue(deque.Cast<int>().SequenceEqual(new[] { 7 }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Set_WrongType_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 13 }) as IList;
            deque[0] = this;
        }
    }
}
