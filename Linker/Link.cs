// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Link.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Link type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;

    public class Link<TSource, TTarget>
    {
        /// <summary>
        ///     The source.
        /// </summary>
        private TSource source;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Link{TSource,TTarget}" /> class.
        /// </summary>
        public Link()
        {
            this.Targets.CollectionChanged += this.TargetsOnCollectionChanged;
        }

        /// <summary>
        ///     Gets or sets a value indicating whether is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        ///     Gets the mappers.
        /// </summary>
        public List<Mapper<TSource, TTarget>> Mappers { get; } = new List<Mapper<TSource, TTarget>>();

        /// <summary>
        ///     Gets or sets the source.
        /// </summary>
        public TSource Source
        {
            get => this.source;
            set
            {
                this.UnbindSource();

                this.BindSource(value);

                this.source = value;
            }
        }

        /// <summary>
        ///     Gets the targets.
        /// </summary>
        public ObservableCollection<TTarget> Targets { get; } = new ObservableCollection<TTarget>();

        /// <summary>
        ///     Updates the source object.
        /// </summary>
        /// <param name="target">
        ///     The object that is calling this update.
        /// </param>
        /// <param name="mapper">
        ///     The mapper that represents the relationship.
        /// </param>
        public void UpdateSource(object target, Mapper<TSource, TTarget> mapper)
        {
            if (mapper == null || mapper.Mode == LinkMode.OneWay) return;

            this.IsEnabled = false;
            mapper.SourcePropertyInfo.SetValue(this.Source, mapper.TargetPropertyInfo.GetValue(target));
            this.IsEnabled = true;
        }

        /// <summary>
        ///     Updates all the targets based on the source.
        /// </summary>
        /// <param name="mapper">
        ///     The mapper that represents the relationship.
        /// </param>
        public void UpdateTargets(Mapper<TSource, TTarget> mapper)
        {
            if (mapper == null) return;

            foreach (var target in this.Targets)
                mapper.TargetPropertyInfo.SetValue(target, mapper.SourcePropertyInfo.GetValue(this.Source));
        }

        /// <summary>
        ///     The bind source.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        private void BindSource(TSource value)
        {
            if (value is INotifyPropertyChanged propertyChanged)
                propertyChanged.PropertyChanged += this.PropertyChangedOnPropertyChanged;
        }

        /// <summary>
        ///     The property changed on property changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="propertyChangedEventArgs">
        ///     The property changed event args.
        /// </param>
        private void PropertyChangedOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (!this.IsEnabled) return;

            if (sender.Equals(this.Source))
                this.UpdateTargets(
                    this.Mappers.FirstOrDefault(
                        i => i.SourcePropertyInfo.Name == propertyChangedEventArgs.PropertyName));
            else
                this.UpdateSource(
                    sender,
                    this.Mappers.FirstOrDefault(
                        i => i.TargetPropertyInfo.Name == propertyChangedEventArgs.PropertyName));
        }

        /// <summary>
        ///     The targets on collection changed.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        ///     The notify collection changed event args.
        /// </param>
        private void TargetsOnCollectionChanged(
            object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in notifyCollectionChangedEventArgs.NewItems)
                        if (newItem is INotifyPropertyChanged propertyChanged)
                            propertyChanged.PropertyChanged += this.PropertyChangedOnPropertyChanged;

                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var oldItem in notifyCollectionChangedEventArgs.OldItems)
                        if (oldItem is INotifyPropertyChanged propertyChanged)
                            propertyChanged.PropertyChanged -= this.PropertyChangedOnPropertyChanged;

                    break;
            }
        }

        /// <summary>
        ///     The unbind source.
        /// </summary>
        private void UnbindSource()
        {
            if (this.source != null && this.source is INotifyPropertyChanged propertyChanged)
                propertyChanged.PropertyChanged -= this.PropertyChangedOnPropertyChanged;
        }
    }
}