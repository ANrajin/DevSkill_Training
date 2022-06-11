using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    public class PersonEnumerator : IEnumerator<Person>
    {
        private Person[] _persons;
        private int _current;

        public Person Current => _persons[_current];

        object IEnumerator.Current => _persons[_current];

        public PersonEnumerator(Person[] persons)
        {
            _persons = persons;
            _current = -1;
        }

        public void Dispose()
        {
            _persons = new Person[0];
        }

        public bool MoveNext()
        {
            _current++;
            return _current < _persons.Length;
        }

        public void Reset()
        {
            _current = -1;
        }
    }
}
