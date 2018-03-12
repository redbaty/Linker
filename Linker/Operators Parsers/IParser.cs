// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IParser.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the IParser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker.Operators_Parsers
{
    /// <summary>
    /// The Parser interface.
    /// </summary>
    /// <typeparam name="TSource">
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// </typeparam>
    interface IParser<TSource, TTarget>
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
        void Parse(string fullCall, LinkMode mode, LinkBuilder<TSource, TTarget> builder);
    }
}