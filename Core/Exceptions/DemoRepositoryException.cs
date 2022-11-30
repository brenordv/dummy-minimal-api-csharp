using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class DemoRepositoryException : DemoBaseException
{
    public DemoRepositoryException()
    { }
    
    public DemoRepositoryException(string msg): base(msg)
    { }

    public DemoRepositoryException(string msg, Exception innerException): base(msg, innerException)
    { }
    
    
    protected DemoRepositoryException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}