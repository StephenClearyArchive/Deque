# Note: An updated version of this project is [here](https://github.com/StephenCleary/Deque).

# Deque

A simple double-ended queue (deque) in C#. Unit tested.

This deque provides O(1) indexed access, O(1) removals from the front and back, amortized O(1) insertions to the front and back, and O(N) insertions and removals anywhere else (with the operations getting slower as the index approaches the middle).

The project is available [via NuGet](http://nuget.org/packages/Nito.Deque) as a C# source package. The package will install a single source file that defines an internal `Deque<T>` class with the following signature (in addition to the members of `IList<T>` and `IList`):

````C#
internal sealed class Deque<T> : IList<T>, System.Collections.IList
{
  // Initializes a new instance of the Deque<T> class with the specified capacity.
  public Deque(int capacity);

  /// Initializes a new instance of the Deque<T> class with the elements from the specified collection.
  public Deque(IEnumerable<T> collection);

  // Initializes a new instance of the Deque<T> class.
  public Deque();

  // Gets or sets the capacity for this deque. This value must always be greater than zero, and this property cannot be set to a value less than Count.
  public int Capacity { get; set; }

  // Inserts a single element at the back of this deque.
  public void AddToBack(T value);

  // Inserts a single element at the front of this deque.
  public void AddToFront(T value);

  // Inserts a collection of elements into this deque.
  public void InsertRange(int index, IEnumerable<T> collection);

  // Removes a range of elements from this deque.
  public void RemoveRange(int offset, int count);

  // Removes and returns the last element of this deque.
  public T RemoveFromBack();

  // Removes and returns the first element of this deque.
  public T RemoveFromFront();
}
````
