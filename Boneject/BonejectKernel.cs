using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ninject;

namespace Boneject;

[Serializable]
public class BonejectKernel : StandardKernel
{
    public BonejectKernel DeepClone()
    {
        using var memStream = new MemoryStream();
        
        var formatter = new BinaryFormatter();
        formatter.Serialize(memStream, this);
        memStream.Position = 0;
        
        return (BonejectKernel)formatter.Deserialize(memStream);
    }
}