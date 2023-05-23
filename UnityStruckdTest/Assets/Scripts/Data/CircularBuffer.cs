using UnityEngine;
using System;

namespace UnityStruckdTest.Data
{
    public class CircularBuffer<T>
    {
        private readonly T[] _buffer;
        private readonly int _capacity;
        private int _currentSize = 0;
        private int _currentIndex = 0;

        public int Size => _currentSize;

        public CircularBuffer(int capacity)
        {
            _capacity = capacity;
            _buffer = new T[capacity];
        }

        public void Enqueue(T value)
        {
            _currentSize = Mathf.Clamp(_currentSize + 1, 0, _capacity);
            _buffer[_currentIndex] = value;
            _currentIndex = _currentIndex + 1 < _capacity ? _currentIndex + 1 : 0;
        }

        public T Dequeue()
        {
            if (_currentSize == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            _currentIndex = _currentIndex - 1 < 0 ? _capacity - 1 : _currentIndex - 1;
            var value = _buffer[_currentIndex];
            _currentSize = Mathf.Clamp(_currentSize - 1, 0, _capacity);
            return value;
        }
    }
}