using NUnit.Framework;

namespace Example.ObjectsComparingExamples;

public class PassedWithIEquatableExample
{
    public class Person : IEquatable<Person>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Age);
        }

        public bool Equals(Person? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Age == other.Age;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Person)obj);
        }
    }

    public static class PersonGetter
    {
        public static Person GetIvan()
        {
            return new Person { Name = "Ivan", Age = 22 };
        }
    }

    public class Tests
    {
        [Test]
        public void WithIEquatable()
        {
            var expected = new Person { Name = "Ivan", Age = 23 };

            var actual = PersonGetter.GetIvan();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}