using System.Reflection;
using MelonLoader;

namespace Boneject
{
    public delegate object? InjectParameter(object? previous, ParameterInfo? parameter,
                                            MelonInfoAttribute info);
}
