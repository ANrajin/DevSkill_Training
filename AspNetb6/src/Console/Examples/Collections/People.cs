using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class People : ICollection<Person>
    {
        private Person[] _persons;
        private int _current;
        public int Count => _persons.Length;

        public bool IsReadOnly => false;

        public People(int length)
        {
            _persons = new Person[length];
        }

        public void Add(Person item)
        {
            _persons[_current++] = item;
        }

        public void Clear()
        {
            _persons = new Person[0];
        }

        public bool Contains(Person item)
        {
            foreach(var person in _persons)
            {
                if (person == item)
                    return true;
            }
            return false;
        }

        public void CopyTo(Person[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Person> GetEnumerator()
        {
            return new PersonEnumerator(_persons);
        }

        public bool Remove(Person item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
