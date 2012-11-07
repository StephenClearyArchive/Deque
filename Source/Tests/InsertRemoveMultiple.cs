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
    public class InsertRemoveMultiple
    {
        [TestMethod]
        public void InsertMultiple()
        {
            InsertTest(new[] { 1, 2, 3 }, new[] { 7, 13 });
            InsertTest(new[] { 1, 2, 3, 4 }, new[] { 7, 13 });
        }

        [TestMethod]
        public void Insert_RangeOfZeroElements_HasNoEffect()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.InsertRange(1, new int[] { });
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 2, 3 }));
        }

        [TestMethod]
        public void InsertMultiple_MakesRoomForNewElements()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.InsertRange(1, new[] { 7, 13 });
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 7, 13, 2, 3 }));
            Assert.AreEqual(5, deque.Capacity);
        }

        [TestMethod]
        public void RemoveMultiple()
        {
            RemoveTest(new[] { 1, 2, 3 });
            RemoveTest(new[] { 1, 2, 3, 4 });
        }

        [TestMethod]
        public void Remove_RangeOfZeroElements_HasNoEffect()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveRange(1, 0);
            Assert.IsTrue(deque.SequenceEqual(new[] { 1, 2, 3 }));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), AllowDerivedTypes = true)]
        public void Remove_NegativeCount_ThrowsException()
        {
            var deque = new Deque<int>(new[] { 1, 2, 3 });
            deque.RemoveRange(1, -1);
        }

        private void InsertTest(IEnumerable<int> initial, IEnumerable<int> items)
        {
            var totalCapacity = initial.Count() + items.Count();
            for (int rotated = 0; rotated <= totalCapacity; ++rotated)
            {
                for (int index = 0; index <= initial.Count(); ++index)
                {
                    // Calculate the expected result using the slower List<int>.
                    var result = new List<int>(initial);
                    for (int i = 0; i != rotated; ++i)
                    {
                        var item = result[0];
                        result.RemoveAt(0);
                        result.Add(item);
                    }
                    result.InsertRange(index, items);

                    // First, start off the deque with the initial items.
                    var deque = new Deque<int>(initial);

                    // Ensure there's enough room for the inserted items.
                    deque.Capacity += items.Count();

                    // Rotate the existing items.
                    for (int i = 0; i != rotated; ++i)
                    {
                        var item = deque[0];
                        deque.RemoveFromFront();
                        deque.AddToBack(item);
                    }

                    // Do the insert.
                    deque.InsertRange(index, items);

                    // Ensure the results are as expected.
                    Assert.IsTrue(deque.SequenceEqual(result));
                }
            }
        }

        private void RemoveTest(IEnumerable<int> initial)
        {
            for (int count = 0; count <= initial.Count(); ++count)
            {
                for (int rotated = 0; rotated <= initial.Count(); ++rotated)
                {
                    for (int index = 0; index <= initial.Count() - count; ++index)
                    {
                        // Calculated the expected result using the slower List<int>.
                        var result = new List<int>(initial);
                        for (int i = 0; i != rotated; ++i)
                        {
                            var item = result[0];
                            result.RemoveAt(0);
                            result.Add(item);
                        }
                        result.RemoveRange(index, count);

                        // First, start off the deque with the initial items.
                        var deque = new Deque<int>(initial);

                        // Rotate the existing items.
                        for (int i = 0; i != rotated; ++i)
                        {
                            var item = deque[0];
                            deque.RemoveFromFront();
                            deque.AddToBack(item);
                        }

                        // Do the remove.
                        deque.RemoveRange(index, count);

                        // Ensure the results are as expected.
                        Assert.IsTrue(deque.SequenceEqual(result));
                    }
                }
            }
        }
    }
}
