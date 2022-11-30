using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class DemoNoContentException: DemoServiceException
{
    public DemoNoContentException()
    { }
    
    public DemoNoContentException(string msg): base(msg)
    { }

    public DemoNoContentException(string msg, Exception innerException): base(msg, innerException)
    { }
    protected DemoNoContentException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}