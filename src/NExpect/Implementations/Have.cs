using NExpect.Interfaces;

// ReSharper disable ClassNeverInstantiated.Global

namespace NExpect.Implementations
{
    internal class Have<T> :
        ExpectationContext<T>,
        IHasActual<T>,
        IHave<T>
    {
        public T Actual { get; }
        public IA<T> A => Factory.Create<T, A<T>>(Actual, this);
        public IAn<T> An => Factory.Create<T, An<T>>(Actual, this);

        public Have(T actual)
        {
            Actual = actual;
        }
    }
}