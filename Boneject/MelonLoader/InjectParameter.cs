using System.Reflection;
using MelonLoader;

namespace Boneject.MelonLoader;

public delegate object? InjectParameter(object? previous, ParameterInfo? parameter, MelonInfoAttribute info);