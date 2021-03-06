﻿using System;
using System.Collections.Generic;
using NExpect.Exceptions;
using NUnit.Framework;
using PeanutButter.RandomGenerators;

// ReSharper disable MemberHidesStaticFromOuterClass
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable ExpressionIsAlwaysNull

namespace NExpect.Tests.ObjectEquality
{
    [TestFixture]
    public class TestingObjectEquality
    {
        [TestFixture]
        public class To
        {
            public class Equal
            {
                [Test]
                public void Expect_src_ToEqual_value_WhenMatches_ShouldNotThrow()
                {
                    // Arrange
                    var actual = 1;
                    var expected = 1;
                    // Pre-Assert

                    // Act
                    Assert.That(
                        () => Expectations.Expect(actual).To.Equal(expected),
                        Throws.Nothing
                    );
                    // Assert
                }

                [Test]
                public void Expect_src_ToEqual_value_WhenDoesNotMatch_ShouldThrow()
                {
                    // Arrange
                    var actual = 1;
                    var expected = 2;
                    // Pre-Assert

                    // Act
                    Assert.That(
                        () => Expectations.Expect(actual).To.Equal(expected),
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"Expected {expected} but got {actual}")
                    );
                    // Assert
                }

                [Test]
                public void
                    Expect_src_ToEqual_value_WhenDoesNotMatch_GivenCustomMessage_ShouldThrowWithCustomMessageAndRegularOne()
                {
                    // Arrange
                    var actual = 1;
                    var expected = 2;
                    var custom = RandomValueGen.GetRandomString(5);
                    // Pre-Assert

                    // Act
                    Assert.That(
                        () => Expectations.Expect(actual).To.Equal(expected, custom),
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"Expected {expected} but got {actual}")
                    );
                    Assert.That(
                        () => Expectations.Expect(actual).To.Equal(expected, custom),
                        Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains(custom)
                    );
                    // Assert
                }

                [Test]
                public void Negation_WhenValuesDoNotMatch_ShouldNotThrow()
                {
                    // Arrange
                    var actual = 1;
                    var expected = 2;

                    // Pre-Assert

                    // Act
                    Assert.That(
                        () => Expectations.Expect(actual).Not.To.Equal(expected),
                        Throws.Nothing
                    );

                    // Assert
                }

                [Test]
                public void ReversedNegation_WhenValuesDoNotMatch_ShouldNotThrow()
                {
                    // Arrange
                    var actual = 1;
                    var expected = 2;

                    // Pre-Assert

                    // Act
                    Assert.That(
                        () => Expectations.Expect(actual).To.Not.Equal(expected),
                        Throws.Nothing
                    );

                    // Assert
                }


                [Test]
                public void AlternativeEqualGrammar_HappyPath()
                {
                    // Arrange
                    var value = RandomValueGen.GetRandomInt();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(value).To.Be.Equal.To(value);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_SadPath()
                {
                    // Arrange
                    var value = RandomValueGen.GetRandomInt();
                    var expected = RandomValueGen.GetAnother(value);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(value).To.Be.Equal.To(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_Negated_HappyPath()
                {
                    // Arrange
                    var value = RandomValueGen.GetRandomInt();
                    var unexpected = RandomValueGen.GetAnother(value);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(value).Not.To.Be.Equal.To(unexpected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_Negated_SadPath()
                {
                    // Arrange
                    var value = RandomValueGen.GetRandomInt();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(value).Not.To.Be.Equal.To(value);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_AltNegated_HappyPath()
                {
                    // Arrange
                    var value = RandomValueGen.GetRandomInt();
                    var unexpected = RandomValueGen.GetAnother(value);
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(value).To.Not.Be.Equal.To(unexpected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void AlternativeEqualGrammar_AltNegated_SadPath()
                {
                    // Arrange
                    var value = RandomValueGen.GetRandomInt();
                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(value).To.Not.Be.Equal.To(value);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [TestFixture]
                public class ActingOnNulls
                {
                    [Test]
                    public void GivenActualIsNull_WhenDoesNotMatch_ShouldThrow_WithValidMessage()
                    {
                        // Arrange
                        string actual = null;
                        string expected = "1";
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () => Expectations.Expect(actual).To.Equal(expected),
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected \"{expected}\" but got {actual}")
                        );
                        // Assert
                    }

                    [Test]
                    public void GivenExpectationIsNull_WhenDoesNotMatch_ShouldThrow_WithValidMessage()
                    {
                        // Arrange
                        string actual = "1";
                        string expected = null;
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () => Expectations.Expect(actual).To.Equal(expected),
                            Throws.Exception
                                .InstanceOf<UnmetExpectationException>()
                                .With.Message.Contains($"Expected (null) but got \"{actual}\"")
                        );
                        // Assert
                    }

                    [Test]
                    public void GivenActualAndExpectationAreNull_WhenMatches_ShouldNotThrow()
                    {
                        // Arrange
                        string actual = null;
                        string expected = null;
                        // Pre-Assert

                        // Act
                        Assert.That(
                            () => Expectations.Expect(actual).To.Equal(expected),
                            Throws.Nothing
                        );
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Intersection
            {
                [TestFixture]
                public class Equal
                {
                    public class NamedIdentifier
                    {
                        public int Id { get; }
                        public string Name { get; }

                        public NamedIdentifier(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                    }

                    public class OtherNamedIdentifier
                    {
                        public int Id { get; }
                        public string Name { get; }
                        public string Type => GetType().Name;

                        public OtherNamedIdentifier(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                    }

                    [Test]
                    public void PositiveResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Intersection.Equal(right);
                        }, Throws.Nothing);
                        // Assert
                    }

                    // TODO: it would be nice if the message could clarify that there
                    //  are no intersecting properties
                    [Test]
                    public void WhenNoPropertiesInCommon_ShouldThrow()
                    {
                        // Arrange
                        var left = new {moo = "cow"};
                        var right = new {cow = "cake"};
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Intersection.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void NegativeResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(2, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Intersection.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).Not.To.Intersection.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_AltGrammar_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new OtherNamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Not.Intersection.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }
            }

            [TestFixture]
            public class Deep
            {
                [TestFixture]
                public class Equal
                {
                    public class NamedIdentifier
                    {
                        public int Id { get; }
                        public string Name { get; }

                        public NamedIdentifier(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                    }

                    [Test]
                    public void PositiveResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Deep.Equal(right);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void NegativeResult_ShouldNotThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(2, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Deep.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).Not.To.Deep.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void PositiveResult_Negated_AltGrammar_ShouldThrow()
                    {
                        // Arrange
                        var left = new NamedIdentifier(1, "moo");
                        var right = new NamedIdentifier(1, "moo");
                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(left).To.Not.Deep.Equal(right);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }
            }
        }

        public class Match
        {
            [Test]
            public void WhenMatches_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = RandomValueGen.GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(str).To.Match(s => s == str, "looking for: " + str);
                }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_WithSimpleBooleanReturn_ShouldThrow()
            {
                // Arrange
                var str = RandomValueGen.GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(str).To.Match(s => s != str, "looking for: !" + str);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_Negated_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = RandomValueGen.GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(str).Not.To.Match(s => s != str, "looking for: !" + str);
                }, Throws.Nothing);

                // Assert
            }

            [Test]
            public void WhenDoesntMatch_NegatedAlt_WithSimpleBooleanReturn_ShouldNotThrow()
            {
                // Arrange
                var str = RandomValueGen.GetRandomString();
                // Pre-Assert

                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(str).To.Not.Match(s => s != str, "looking for: !" + str);
                }, Throws.Nothing);

                // Assert
            }
        }



        [TestFixture]
        public class ActingOnInts
        {
            [TestFixture]
            public class Greater
            {
                public class Than
                {
                    [Test]
                    public void WhenActualIsEqualToExpected_ShouldThrow()
                    {
                        // Arrange
                        var actual = 8;
                        var expected = 8;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("8 to be greater than 8"));
                        // Assert
                    }

                    [Test]
                    public void WhenActualIsLessThanToExpected_ShouldThrow()
                    {
                        // Arrange
                        var actual = 7;
                        var expected = 8;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains("7 to be greater than 8"));
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                    {
                        // Arrange
                        var actual = RandomValueGen.GetRandomInt(5, 10);
                        var expected = RandomValueGen.GetRandomInt(0, 4);

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Be.Greater.Than(expected);
                            },
                            Throws.Nothing);

                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDecimals()
                    {
                        // Arrange
                        decimal start = 0.6M;
                        decimal test = 0.5M;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDecimalsAndDouble()
                    {
                        // Arrange
                        decimal start = 0.6M;
                        double test = 0.5;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDoublesAndDecimal()
                    {
                        // Arrange
                        double start = 0.6;
                        decimal test = 0.5M;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDoublesAndFloat()
                    {
                        // Arrange
                        double start = 0.6;
                        float test = 0.5F;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDoublesAndLong()
                    {
                        // Arrange
                        double start = 0.6;
                        long test = -1;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithFloatAndDecimal()
                    {
                        // Arrange
                        float start = 0.6F;
                        decimal test = 0.5M;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithFloatAndLong()
                    {
                        // Arrange
                        double start = 0.6;
                        long test = 0;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithLongAndDecimal()
                    {
                        // Arrange
                        long start = 1;
                        decimal test = 0;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithLongAndDouble()
                    {
                        // Arrange
                        long start = 1;
                        double test = 0;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDecimalsAndFloat()
                    {
                        // Arrange
                        decimal start = 0.6M;
                        float test = 0.5F;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDecimalsAndInt()
                    {
                        // Arrange
                        decimal start = 0.6M;
                        int test = 0;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_ShouldWorkWithDecimalsAndLong()
                    {
                        // Arrange
                        decimal start = 0.6M;
                        long test = 0;
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                        {
                            Expectations.Expect(start).To.Be.Greater.Than(test);
                        }, Throws.Nothing);
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                    {
                        // Arrange
                        var actual = RandomValueGen.GetRandomInt(5, 10);

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Be.Greater.Than(actual);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());

                        // Assert
                    }

                    [Test]
                    public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
                    {
                        // Arrange
                        var actual = RandomValueGen.GetRandomInt(5, 10);
                        var expected = RandomValueGen.GetRandomInt(11, 20);

                        // Pre-Assert

                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Be.Greater.Than(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());

                        // Assert
                    }

                    [Test]
                    public void GreaterThan_Negated_WhenActualIsGreaterThanExpected_ShouldThrow()
                    {
                        // Arrange
                        var actual = RandomValueGen.GetRandomInt(5, 10);
                        var expected = RandomValueGen.GetRandomInt(1, 4);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).Not.To.Be.Greater.Than(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }

                    [Test]
                    public void GreaterThan_AltNegated_WhenActualIsGreaterThanExpected_ShouldThrow()
                    {
                        // Arrange
                        var actual = RandomValueGen.GetRandomInt(5, 10);
                        var expected = RandomValueGen.GetRandomInt(1, 4);
                        // Pre-Assert
                        // Act
                        Assert.That(() =>
                            {
                                Expectations.Expect(actual).To.Not.Be.Greater.Than(expected);
                            },
                            Throws.Exception.InstanceOf<UnmetExpectationException>());
                        // Assert
                    }
                }

                [TestFixture]
                public class And
                {
                    [TestFixture]
                    public class Less
                    {
                        [TestFixture]
                        public class Than
                        {
                            [TestFixture]
                            public class HomogenousTypes
                            {
                                [TestFixture]
                                public class IntIntInt
                                {
                                    private (int min, int max, int actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomInt(1, 5);
                                        var max = RandomValueGen.GetRandomInt(10, 15);
                                        var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }

                                [TestFixture]
                                public class LongLongLong
                                {
                                    private (long min, long max, long actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomInt(1, 5);
                                        var max = RandomValueGen.GetRandomInt(10, 15);
                                        var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }

                                [TestFixture]
                                public class DecimalDecimalDecimal
                                {
                                    private (decimal min, decimal max, decimal actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomInt(1, 5);
                                        var max = RandomValueGen.GetRandomInt(10, 15);
                                        var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }

                                [TestFixture]
                                public class FloatFloatFloat
                                {
                                    private (float min, float max, float actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomInt(1, 5);
                                        var max = RandomValueGen.GetRandomInt(10, 15);
                                        var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }

                                [TestFixture]
                                public class DoubleDoubleDouble
                                {
                                    private (double min, double max, double actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomInt(1, 5);
                                        var max = RandomValueGen.GetRandomInt(10, 15);
                                        var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }

                                [TestFixture]
                                public class DateTimeDateTimeDateTime
                                {
                                    private (DateTime min, DateTime max, DateTime actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomDate(new DateTime(2001, 1, 1));
                                        var max = RandomValueGen.GetRandomDate(new DateTime(2030, 1, 1));
                                        var actual = RandomValueGen.GetRandomDate(
                                            min.AddMilliseconds(1),
                                            max.AddMilliseconds(-1)
                                        );
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }
                            }

                            [TestFixture]
                            public class HeteroGeneousTypes
                            {
                                [TestFixture]
                                public class IntDoubleFloat
                                {
                                    private (int min, double max, float actual) Source()
                                    {
                                        var min = RandomValueGen.GetRandomInt(1, 5);
                                        var max = RandomValueGen.GetRandomInt(10, 15);
                                        var actual = RandomValueGen.GetRandomInt(min + 1, max - 1);
                                        return (min, max, actual);
                                    }

                                    [Test]
                                    public void PositiveExpectation_WhenIntsWithinRange_ShouldNotThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Nothing);
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .Not.To.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }

                                    [Test]
                                    public void NegativeExpectation_AltSyntax_WhenIntsWithinRange_ShouldThrow()
                                    {
                                        // Arrange
                                        var (min, max, actual) = Source();
                                        // Pre-Assert
                                        // Act
                                        Assert.That(() =>
                                        {
                                            Expectations.Expect(actual)
                                                .To.Not.Be.Greater.Than(min)
                                                .And.Less.Than(max);
                                        }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                                        // Assert
                                    }
                                }
                            }
                        }
                    }
                }
            }

            [TestFixture]
            public class LessThan
            {
                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = RandomValueGen.GetRandomInt(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = RandomValueGen.GetRandomInt(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = RandomValueGen.GetRandomInt(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).Not.To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }
            }
        }

        [TestFixture]
        public class ActingOnDecimals
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(5, 10);
                    var expected = RandomValueGen.GetRandomDecimal(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(actual);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(5, 10);
                    var expected = RandomValueGen.GetRandomDecimal(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(1, 5);
                    var expected = RandomValueGen.GetRandomDecimal(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).Not.To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }
            }

            [TestFixture]
            public class LessThan
            {
                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(1, 5);
                    var expected = RandomValueGen.GetRandomDecimal(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDecimal(1, 5);
                    var expected = RandomValueGen.GetRandomDecimal(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }
            }
        }

        [TestFixture]
        public class ActingOnDoubles
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDouble(5, 10);
                    var expected = RandomValueGen.GetRandomDouble(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDouble(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(actual);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDouble(5, 10);
                    var expected = RandomValueGen.GetRandomDouble(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }
            }

            [TestFixture]
            public class LessThan
            {
                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDouble(1, 5);
                    var expected = RandomValueGen.GetRandomDouble(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDouble(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDouble(1, 5);
                    var expected = RandomValueGen.GetRandomDouble(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }
            }
        }

        [TestFixture]
        public class ActingOnFloats
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(5, 10);
                    var expected = GetRandomFloat(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(actual);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(5, 10);
                    var expected = GetRandomFloat(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }
            }

            [TestFixture]
            public class LessThan
            {
                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(1, 5);
                    var expected = GetRandomFloat(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{(double) actual} to be less than {(double) expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomFloat(1, 5);
                    var expected = GetRandomFloat(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{(double) actual} to be less than {(double) expected}"));
                    // Assert
                }
            }
        }

        [TestFixture]
        public class ActingOnDateTimes
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = RandomValueGen.GetRandomDate(null, actual.AddTicks(-1));

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(actual);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = RandomValueGen.GetRandomDate(actual.AddTicks(1), null);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = RandomValueGen.GetRandomDate(actual.AddTicks(1), null);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).Not.To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }
            }

            [TestFixture]
            public class LessThan
            {
                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = RandomValueGen.GetRandomDate(actual.AddTicks(1), null);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = RandomValueGen.GetRandomDate(null, actual.AddTicks(-1));
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_Negated_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomDate();
                    var expected = RandomValueGen.GetRandomDate(null, actual.AddTicks(-1));
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).Not.To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }
            }
        }

        private static float GetRandomFloat(float min, float max)
        {
            return (float) RandomValueGen.GetRandomDouble(min, max);
        }

        private static long GetRandomLong(long min, long max)
        {
            return RandomValueGen.GetRandomInt((int) min, (int) max);
        }

        [TestFixture]
        public class ActingOnLongs
        {
            [TestFixture]
            public class GreaterThan
            {
                [Test]
                public void GreaterThan_WhenActualIsGreaterThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = GetRandomLong(5, 10);
                    var expected = GetRandomLong(0, 4);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Nothing);

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomLong(5, 10);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(actual);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }

                [Test]
                public void GreaterThan_WhenActualIsLessThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = GetRandomLong(5, 10);
                    var expected = GetRandomLong(11, 20);

                    // Pre-Assert

                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Greater.Than(expected);
                        },
                        Throws.Exception.InstanceOf<UnmetExpectationException>());

                    // Assert
                }
            }

            [TestFixture]
            public class LessThan
            {
                [Test]
                public void LessThan_WhenActualIsLessThanExpected_ShouldNotThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = RandomValueGen.GetRandomInt(6, 12);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Nothing);
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsEqualToExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = actual;
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }

                [Test]
                public void LessThan_WhenActualIsGreaterThanExpected_ShouldThrow()
                {
                    // Arrange
                    var actual = RandomValueGen.GetRandomInt(1, 5);
                    var expected = RandomValueGen.GetRandomInt(-5, 0);
                    // Pre-Assert
                    // Act
                    Assert.That(() =>
                        {
                            Expectations.Expect(actual).To.Be.Less.Than(expected);
                        },
                        Throws.Exception
                            .InstanceOf<UnmetExpectationException>()
                            .With.Message.Contains($"{actual} to be less than {expected}"));
                    // Assert
                }
            }
        }

        [TestFixture]
        public class ActingOnMismatchedTypes
        {
            [Test]
            public void Expect_Byte_ToEqual_Int_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                byte actual = 1;
                int expected = 1;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Byte_ToEqual_Int_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                byte actual = 1;
                int expected = 2;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void Expect_Short_ToEqual_Long_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                short actual = 1;
                long expected = 1;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Short_ToEqual_Long_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                short actual = 1;
                long expected = 2;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void Expect_Int_ToEqual_Long_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                int actual = 1;
                long expected = 1;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Int_ToEqual_Long_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                int actual = 1;
                long expected = 2;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void Expect_Float_ToEqual_Double_WhenMatches_ShouldNotThrow()
            {
                // Arrange
                float actual = 1.1f;
                double expected = 1.1f;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Nothing
                );
                // Assert
            }

            [Test]
            public void Expect_Float_ToEqual_Double_WhenNotMatches_ShouldThrow()
            {
                // Arrange
                float actual = 1.1f;
                double expected = 1.2f;
                // Pre-Assert

                // Act
                Assert.That(
                    () => Expectations.Expect(actual).To.Equal(expected),
                    Throws.Exception
                        .InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }
        }

        [TestFixture]
        public class ReferenceEqualityTesting
        {
            public class Coordinate
            {
                int X { get; }
                int Y { get; }

                public Coordinate(int x, int y)
                {
                    X = x;
                    Y = y;
                }

                public override int GetHashCode()
                {
                    return $"{X}-{Y}".GetHashCode();
                }

                public override bool Equals(object obj)
                {
                    var other = obj as Coordinate;
                    if (other == null)
                        return false;
                    return other.X == X && other.Y == Y;
                }
            }

            [Test]
            public void Be_WhenHaveSameRef_ShouldNotThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(instance).To.Be(instance);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_WhenHaveDifferentRefButAreEqual_ShouldThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                var other = new Coordinate(2, 3);
                // Pre-Assert
                Assert.That(instance, Is.EqualTo(other));
                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(instance).To.Be(other);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.EqualTo($"Expected {instance} to be the same reference as {other}"));
                // Assert
            }

            [Test]
            public void Be_Negated_WhenHaveSameRef_ShouldThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(instance).Not.To.Be(instance);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>()
                    .With.Message.EqualTo($"Expected {instance} not to be the same reference as {instance}"));
                // Assert
            }

            [Test]
            public void Be_Negated_WhenHaveDifferentRefButAreEqual_ShouldNotThrow()
            {
                // Arrange
                var instance = new Coordinate(2, 3);
                var other = new Coordinate(2, 3);
                // Pre-Assert
                Assert.That(instance, Is.EqualTo(other));
                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(instance).Not.To.Be(other);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_ActingOnCollection_WhenRefEqual_ShouldNotThrow()
            {
                // Arrange
                var collection = new List<int>();
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(collection).To.Be(collection);
                }, Throws.Nothing);
                // Assert
            }

            [Test]
            public void Be_ActingOnCollection_WhenNotRefEqual_ShouldThrow()
            {
                // Arrange
                var collection = new List<int>();
                var other = new List<int>();
                // Pre-Assert
                // Act
                Assert.That(() =>
                {
                    Expectations.Expect(collection).To.Be(other);
                }, Throws.Exception.InstanceOf<UnmetExpectationException>());
                // Assert
            }
        }

        [TestFixture]
        public class UnmetExpectationMessageTesting
        {
            [Test]
            public void GivenStrings_WhenNotMatched_ShouldThrow_WithQuotesAroundValuesInMessage()
            {
                // Arrange
                var actual = RandomValueGen.GetRandomString();
                var expected = RandomValueGen.GetAnother(actual);
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expectations.Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected \"{expected}\" but got \"{actual}\"")
                );
                // Assert
            }

            [Test]
            public void GivenStrings_WhenMatchedAndNegated_ShouldThrow_WithQuotesAroundValuesInMessage()
            {
                // Arrange
                var value = RandomValueGen.GetRandomString();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expectations.Expect(value).To.Not.Equal(value);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Did not expect \"{value}\", but got exactly that")
                );
                // Assert
            }

            [Test]
            public void GivenStringAndNullString_WhenNotMatched_ShouldThrow_WithNullIdentifierInMessage()
            {
                // Arrange
                string actual = null;
                var expected = RandomValueGen.GetRandomString();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expectations.Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected \"{expected}\" but got (null)")
                );
                // Assert
            }

            [Test]
            public void GivenObjectAndNullObject_WhenNotMatched_ShouldThrow_WithNullIdentifierInMessage()
            {
                // Arrange
                object actual = null;
                var expected = new object();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expectations.Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected {} but got (null)")
                );
                // Assert
            }

            [Test]
            public void GivenInts_WhenNotMatched_ShouldThrow_WithoutQuotesAroundValuesInMessage()
            {
                // Arrange
                var actual = RandomValueGen.GetRandomInt();
                var expected = RandomValueGen.GetAnother(actual);
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expectations.Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains($"Expected {expected} but got {actual}")
                );
                // Assert
            }

            [Test]
            public void GivenObjects_WhenNotMatched_ShouldThrow_WithoutQuotesAroundValuesInMessage()
            {
                // Arrange
                var actual = new object();
                var expected = new object();
                // Pre-Assert
                // Act
                Assert.That(() =>
                    {
                        Expectations.Expect(actual).To.Equal(expected);
                    },
                    Throws.Exception.InstanceOf<UnmetExpectationException>()
                        .With.Message.Contains("Expected {} but got {}")
                );
                // Assert
            }
        }
    }
}