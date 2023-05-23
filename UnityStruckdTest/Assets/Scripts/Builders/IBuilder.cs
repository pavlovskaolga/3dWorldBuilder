using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Builders
{
    public interface IBuilder<T>
    {
        BaseActor Build(T context);
    }
}