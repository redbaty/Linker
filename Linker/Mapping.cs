﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Mapping.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the Mapping type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker
{
    using System;
    using System.Reflection;

    /// <summary>
    ///     Represents a property relationship.
    /// </summary>
    /// <typeparam name="TSource">
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// </typeparam>
    public class Mapping<TSource, TTarget>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Mapping{TSource,TTarget}" /> class.
        /// </summary>
        /// <param name="sourcePropertyInfo">
        ///     The source property info.
        /// </param>
        /// <param name="targetPropertyInfo">
        ///     The target property info.
        /// </param>
        /// <param name="parentLink">
        ///     The parent link.
        /// </param>
        /// <param name="mode">
        ///     The mode.
        /// </param>
        public Mapping(
            PropertyInfo sourcePropertyInfo,
            PropertyInfo targetPropertyInfo,
            Link<TSource, TTarget> parentLink,
            LinkMode mode,
            bool isContextBinding)
        {
            this.SourcePropertyInfo = sourcePropertyInfo ?? throw new ArgumentNullException(nameof(sourcePropertyInfo));
            this.TargetPropertyInfo = targetPropertyInfo ?? throw new ArgumentNullException(nameof(targetPropertyInfo));
            this.ParentLink = parentLink ?? throw new ArgumentNullException(nameof(parentLink));
            this.Mode = mode;
            this.IsContextBinding = isContextBinding;
        }

        /// <summary>
        /// Gets a value indicating whether this is mapping a context binding.
        /// </summary>
        public bool IsContextBinding { get; }

        /// <summary>
        ///     Gets the mode.
        /// </summary>
        public LinkMode Mode { get; }

        /// <summary>
        ///     Gets the parent link.
        /// </summary>
        public Link<TSource, TTarget> ParentLink { get; }

        /// <summary>
        ///     Gets the source property info.
        /// </summary>
        public PropertyInfo SourcePropertyInfo { get; }

        /// <summary>
        ///     Gets the target property info.
        /// </summary>
        public PropertyInfo TargetPropertyInfo { get; }
    }
}