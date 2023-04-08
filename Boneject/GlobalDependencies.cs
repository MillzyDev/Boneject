namespace Boneject;

public static class GlobalDependencies
{
    private static readonly Dictionary<Type, object?> Dependencies = new();

    public static void AddDependency<T>() where T : class
        => AddDependency<T>(null);

    public static void AddDependency(Type type, object? dependency = null)
        => Dependencies.Add(type, dependency);
    
    public static void AddDependency<T>(T? dependency)
        => Dependencies.Add(typeof(T), dependency);

    public static object? GetDependency(Type type)
        => Dependencies.FirstOrDefault(x => x.Key == type).Value;

    public static T? GetDependency<T>()
        => (T?) Dependencies.FirstOrDefault(x => x.Key == typeof(T)).Value;

    public static Dictionary<Type, object?> Get()
        => Dependencies;
}