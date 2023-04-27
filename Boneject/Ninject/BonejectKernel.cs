using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ninject;
using Ninject.Modules;

namespace Boneject.Ninject;

public class BonejectKernel : StandardKernel
{
    public BonejectKernel(params INinjectModule[] modules) : base(modules)
    {
    }

    public BonejectKernel(INinjectSettings settings, params INinjectModule[] modules) : base(settings, modules)
    {
    }

    private object DeepClone()
    {
        using var memStream = new MemoryStream();
        
        var formatter = new BinaryFormatter();
        formatter.Serialize(memStream, this);
        memStream.Position = 0;
        
        return formatter.Deserialize(memStream);
    }

    public BonejectKernel Clone() => (BonejectKernel)DeepClone();
}