// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkBuilder.cs" company="">
//   
// </copyright>
// <summary>
//   The link builder.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Linker
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    ///     The link builder.
    /// </summary>
    /// <typeparam name="TSource">
    /// </typeparam>
    /// <typeparam name="TTarget">
    /// </typeparam>
    public class LinkBuilder<TSource, TTarget>
    {
        /// <summary>
        ///     Gets the link.
        /// </summary>
        private Link<TSource, TTarget> Link { get; } = new Link<TSource, TTarget>();

        /// <summary>
        ///     Build and enable the Link instance.
        /// </summary>
        /// <returns>
        ///     The <see cref="Link" />.
        /// </returns>
        public Link<TSource, TTarget> Build()
        {
            this.Link.IsEnabled = true;
            return this.Link;
        }

        public LinkBuilder<TSource, TTarget> Map<TProperty>(
            Expression<Func<TSource, TProperty>> sourcePropertyLambda,
            Expression<Func<TTarget, TProperty>> targetPropertyLambda,
            LinkMode mode = LinkMode.TwoWay)
        {
            this.Link.Mappers.Add(
                new Mapper<TSource, TTarget>(
                    CheckIfValid(sourcePropertyLambda, typeof(TSource)),
                    CheckIfValid(targetPropertyLambda, typeof(TTarget)),
                    this.Link,
                    mode));
            return this;
        }

        /// <summary>
        ///     Maps all the properties on the source type.
        /// </summary>
        /// <param name="mode">
        ///     The mode.
        /// </param>
        /// <returns>
        ///     The <see cref="LinkBuilder" />.
        /// </returns>
        public LinkBuilder<TSource, TTarget> MapAll(LinkMode mode = LinkMode.TwoWay)
        {
            foreach (var propertyInfo in typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                this.Link.Mappers.Add(new Mapper<TSource, TTarget>(propertyInfo, propertyInfo, this.Link, mode));

            return this;
        }

        /// <summary>
        ///     Set this object as the Source.
        /// </summary>
        /// <param name="source">
        ///     The source.
        /// </param>
        /// <returns>
        ///     The <see cref="LinkBuilder" />.
        /// </returns>
        public LinkBuilder<TSource, TTarget> WithSource(TSource source)
        {
            this.Link.Source = source;
            return this;
        }

        /// <summary>
        ///     Adds this object as a target.
        /// </summary>
        /// <param name="target">
        ///     The target.
        /// </param>
        /// <returns>
        ///     The <see cref="LinkBuilder" />.
        /// </returns>
        public LinkBuilder<TSource, TTarget> WithTarget(TTarget target)
        {
            this.Link.Targets.Add(target);
            return this;
        }

        /// <summary>
        ///     Adds multiple object as a target.
        /// </summary>
        /// <param name="targets">
        ///     The targets.
        /// </param>
        /// <returns>
        ///     The <see cref="LinkBuilder" />.
        /// </returns>
        public LinkBuilder<TSource, TTarget> WithTargets(IEnumerable<TTarget> targets)
        {
            foreach (var target in targets) this.Link.Targets.Add(target);

            return this;
        }

        private static PropertyInfo CheckIfValid(LambdaExpression sourcepropertyLambda, Type type)
        {
            if (!(sourcepropertyLambda.Body is MemberExpression member))
                throw new ArgumentException(
                    string.Format(
                        "Expression '{0}' refers to a method, not a property.",
                        sourcepropertyLambda.ToString()));

            var propertyInfo = member.Member as PropertyInfo;
            if (propertyInfo == null)
                throw new ArgumentException(
                    string.Format(
                        "Expression '{0}' refers to a field, not a property.",
                        sourcepropertyLambda.ToString()));

            if (type != propertyInfo.ReflectedType && !type.IsSubclassOf(propertyInfo.ReflectedType))
                throw new ArgumentException(
                    string.Format(
                        "Expresion '{0}' refers to a property that is not from type {1}.",
                        sourcepropertyLambda.ToString(),
                        type));

            return propertyInfo;
        }
    }
}