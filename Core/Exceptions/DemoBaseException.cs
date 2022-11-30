using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class DemoBaseException: Exception
{
    public DemoBaseException()
    { }
    
    public DemoBaseException(string msg): base(msg)
    { }

    public DemoBaseException(string msg, Exception innerException): base(msg, innerException)
    { }
    
    
    protected DemoBaseException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}