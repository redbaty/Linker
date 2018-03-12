namespace Linker.Console.Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var x = new Person();
            var y = new Person();
            var builder = new LinkBuilder<Person, Person>()
                .Parse("{Binding FirstName}", LinkMode.OneWay)
                .WithSource(x)
                .WithTarget(y).Build();

            x.FirstName = "Hello";
            y.FirstName = "Fak";
        }
    }
}