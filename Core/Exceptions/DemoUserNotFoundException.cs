using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class DemoUserNotFoundException: DemoServiceException
{
    public Guid UserId { get; private set; }

    public DemoUserNotFoundException(Guid userId)
    {
        UserId = userId;
    }
    
    protected DemoUserNotFoundException(SerializationInfo info, StreamingContext context) 
        : base(info, context)
    {
    }
}