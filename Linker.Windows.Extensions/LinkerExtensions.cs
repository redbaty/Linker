// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkerExtensions.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the LinkerExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker.Windows.Extensions
{
    using System.Collections.Generic;
    using System.Windows.Data;

    /// <summary>
    /// The linker extensions.
    /// </summary>
    public static class LinkerExtensions
    {
        /// <summary>
        /// The get binding.
        /// </summary>
        /// <param name="link">
        /// The link.
        /// </param>
        /// <typeparam name="TSource">
        /// </typeparam>
        /// <typeparam name="TTarget">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public static IEnumerable<BindingBase> GetBinding<TSource, TTarget>(this Link<TSource, TTarget> link)
        {
            foreach (var linkMapper in link.Mappers)
            {
                yield return new Binding(linkMapper.TargetPropertyInfo.Name)
                                 {
                                     Source = linkMapper.IsContextBinding
                                                  ? link.Context
                                                  : link.Source
                                 };
            }
        }
    }
}