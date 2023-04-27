using System;

namespace Boneject;

public class BonejectException : Exception
{
    public BonejectException() : base()
    {
    }

    public BonejectException(string message) : base(message)
    {
    }

    public BonejectException(string message, Exception innerException) : base(message, innerException)
    {
    }
}