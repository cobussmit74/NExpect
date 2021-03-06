using System;
using NExpect.Implementations;
using NExpect.Interfaces;
using NExpect.MatcherLogic;
// ReSharper disable UnusedMember.Global

namespace NExpect
{
    /// <summary>
    /// Provides Match() continuations which allow providing a simple
    /// lambda to do your matching, for when writing an entire extension method
    /// seems like an overkill.
    /// </summary>
    public static class MatchProviderExtensions
    {
        /// <summary>
        /// Match the value under test with a simple Func which takes in your value
        /// and returns true if the test should pass.
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="test">Func to test the original value with</param>
        /// <typeparam name="T"></typeparam>
        public static void Match<T>(
            this ITo<T> continuation,
            Func<T, bool> test
        )
        {
            continuation.Match(test, null);
        }

        /// <summary>
        /// Match the value under test with a simple Func which takes in your value
        /// and returns true if the test should pass.
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="test">Func to test the original value with</param>
        /// <typeparam name="T"></typeparam>
        public static void Match<T>(
            this IToAfterNot<T> continuation,
            Func<T, bool> test
        )
        {
            continuation.Match(test, null);
        }

        /// <summary>
        /// Match the value under test with a simple Func which takes in your value
        /// and returns true if the test should pass.
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="test">Func to test the original value with</param>
        /// <typeparam name="T"></typeparam>
        public static void Match<T>(
            this INotAfterTo<T> continuation,
            Func<T, bool> test
        )
        {
            continuation.Match(test, null);
        }

        /// <summary>
        /// Match the value under test with a simple Func which takes in your value
        /// and returns true if the test should pass.
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="test">Func to test the original value with</param>
        /// <param name="customMessage">Message to include in the result upon failure</param>
        /// <typeparam name="T"></typeparam>
        public static void Match<T>(
            this ITo<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(MatchMatcherFor(test, customMessage));
        }

        /// <summary>
        /// Match the value under test with a simple Func which takes in your value
        /// and returns true if the test should pass.
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="test">Func to test the original value with</param>
        /// <param name="customMessage">Message to include in the result upon failure</param>
        /// <typeparam name="T"></typeparam>
        public static void Match<T>(
            this IToAfterNot<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(MatchMatcherFor(test, customMessage));
        }

        /// <summary>
        /// Match the value under test with a simple Func which takes in your value
        /// and returns true if the test should pass.
        /// </summary>
        /// <param name="continuation">Continuation to act on</param>
        /// <param name="test">Func to test the original value with</param>
        /// <param name="customMessage">Message to include in the result upon failure</param>
        /// <typeparam name="T"></typeparam>
        public static void Match<T>(
            this INotAfterTo<T> continuation,
            Func<T, bool> test,
            string customMessage
        )
        {
            continuation.AddMatcher(MatchMatcherFor(test, customMessage));
        }

        private static Func<T, IMatcherResult> MatchMatcherFor<T>(
            Func<T, bool> test,
            string customMessage
        )
        {
            return actual =>
            {
                var passed = test(actual);
                var message = passed
                    ? $"Expected {actual} not to be matched"
                    : $"Expected {actual} to be matched";
                return new MatcherResult(
                    passed,
                    MessageHelpers.FinalMessageFor(message, customMessage)
                );
            };
        }
    }
}