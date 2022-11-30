using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class DemoServiceException : DemoBaseException
{
    public DemoServiceException()
    { }
    
    public DemoServiceException(string msg): base(msg)
    { }

    public DemoServiceException(string msg, Exception innerException): base(msg, innerException)
    { }
    
    protected DemoServiceException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    { }
}