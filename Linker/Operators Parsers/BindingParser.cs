// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BindingParser.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the BindingParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker.Operators_Parsers
{
    using System.Linq;

    using Linker.Annotations;

    /// <summary>
    /// The binding parser.
    /// </summary>
    /// <typeparam name="TSource">
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// </typeparam>
    [Parser("Binding")]
    internal class BindingParser<TSource, TTarget> : IParser<TSource, TTarget>
    {
        /// <summary>
        /// The parse.
        /// </summary>
        /// <param name="fullCall">
        /// The full call.
        /// </param>
        /// <param name="mode">
        /// The mode.
        /// </param>
        /// <param name="builder">
        /// The builder.
        /// </param>
        public void Parse(string fullCall, LinkMode mode, LinkBuilder<TSource, TTarget> builder)
        {
            var propertyPath = fullCall.Split(" ").Last();
            var prop = typeof(TTarget).GetProperty(propertyPath);

            builder.Map(prop, prop, mode);
        }
    }
}