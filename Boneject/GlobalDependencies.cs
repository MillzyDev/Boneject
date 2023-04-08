namespace Boneject;

public static class GlobalDependencies
{
    private static readonly Dictionary<Type, object?> _dependencies = new();

    public static void AddDependency<T>() where T : class
        => AddDependency<T>(null);

    public static void AddDependency(Type type, object? dependency = null)
        => _dependencies.Add(type, dependency);
    
    public static void AddDependency<T>(T? dependency)
        => _dependencies.Add(typeof(T), dependency);

    public static object? GetDependency(Type type)
        => _dependencies.FirstOrDefault(x => x.Key == type).Value;

    public static T? GetDependency<T>()
        => (T?) _dependencies.FirstOrDefault(x => x.Key == typeof(T)).Value;

    public static Dictionary<Type, object?> Get()
        => _dependencies;
}