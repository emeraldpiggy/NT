using SimpleInjector;

namespace IOC
{
    public interface INestedContainer
    {
        Container ParentContainer { get; }
    }
}