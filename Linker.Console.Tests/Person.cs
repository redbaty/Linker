namespace Linker.Console.Tests
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using Linker.Console.Tests.Annotations;

    internal class Person : INotifyPropertyChanged
    {
        private string firstName;

        private string lastName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName
        {
            get => this.firstName;
            set
            {
                this.firstName = value;
                this.OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => this.lastName;
            set
            {
                this.lastName = value;
                this.OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{nameof(this.FirstName)}: {this.FirstName}";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}