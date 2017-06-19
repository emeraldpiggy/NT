using SimpleInjector;

namespace IOC
{
    public interface IContainerInitialiser
    {
        void ConfigureContainerRegistrations(Container container, RegistrationFilter registrationFilter);
    }
}