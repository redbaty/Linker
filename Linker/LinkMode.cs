// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkMode.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the LinkMode type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker
{
    /// <summary>
    ///     The link mode.
    /// </summary>
    public enum LinkMode
    {
        /// <summary>
        ///     The source is read-only.
        /// </summary>
        OneWay,

        /// <summary>
        ///     The source and the targets are always in sync.
        /// </summary>
        TwoWay
    }
}