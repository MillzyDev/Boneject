using System;

namespace Boneject;

public static class Assert
{
    public static void DerivesFrom<T>(Type type)
    {
        if (!type.IsSubclassOf(typeof(T)))
            throw new Exception($"Expected type '{type.Name} to derive from {typeof(T).Name}");
    }
}