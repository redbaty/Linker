namespace Linker.Console.Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var x = new Person();
            var y = new Person();
            var builder = new LinkBuilder<Person, Person>().WithSource(x).WithTarget(y)
                .Map(i => i.LastName, i => i.FirstName).Build();

            x.FirstName = "Hello";
            y.FirstName = "Fak";
        }
    }
}