namespace Boneject.Tests;

internal static class NullString
{
    public static string Create(object? obj) => obj == null ? "NULL" : "NOT NULL";
}