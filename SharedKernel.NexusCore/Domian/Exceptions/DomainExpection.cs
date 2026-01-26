namespace SharedKernel.NexusCore.Domian.Exceptions
{
    public abstract class DomainExpection : Exception
    {

        protected DomainExpection()
        {
        }
        protected DomainExpection(string message)
            : base(message)
        {
        }
        protected DomainExpection(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
