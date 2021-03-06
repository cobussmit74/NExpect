﻿// ReSharper disable InheritdocConsiderUsage
namespace NExpect.Interfaces
{
    /// <summary>
    /// Continuation of Not after To, ie ...Not.To...
    /// </summary>
    /// <typeparam name="T">Underlying type of the continuation</typeparam>
    public interface INotAfterTo<T>: ICanAddMatcher<T>
    {
        /// <summary>
        /// Continuation of Be, carrying type
        /// </summary>
        IBe<T> Be { get; }
        /// <summary>
        /// Starts a deep equality test
        /// </summary>
        IDeep<T> Deep { get; }
        /// <summary>
        /// Starts an intersection equality test
        /// -> only tests properties and fields which match by name and type
        /// </summary>
        IIntersection<T> Intersection { get; }
    }
}