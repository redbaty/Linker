// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParserAttribute.cs" company="">
//   
// </copyright>
// <summary>
//   The parser attribute.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker.Annotations
{
    using System;

    /// <summary>
    /// The parser attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class ParserAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserAttribute"/> class.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        public ParserAttribute(string operation)
        {
            this.Operation = operation;
        }

        /// <summary>
        /// Represents which operation this operator supports.
        /// </summary>
        public string Operation { get; }
    }
}