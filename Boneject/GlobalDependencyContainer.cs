namespace Boneject;

public static class GlobalDependencyContainer
{
    private static HashSet<KeyValuePair<Type, object?>> _globalDependencies = new();

    public static void AddDependency<T>() where T : class
        => AddDependency<T>(null);

    public static void AddDependency(Type type, object? dependency = null)
        => _globalDependencies.Add(new KeyValuePair<Type, object?>(type, dependency));
    
    public static void AddDependency<T>(T? dependency)
        => _globalDependencies.Add(new KeyValuePair<Type, object?>(typeof(T), dependency));

    public static object? GetDependency(Type type)
        => _globalDependencies.FirstOrDefault(x => x.Key == type).Value;

    public static T? GetDependency<T>()
        => (T?) _globalDependencies.FirstOrDefault(x => x.Key == typeof(T)).Value;
}