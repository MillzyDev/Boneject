using System;
using Ninject;

namespace Boneject.MelonLoader.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class OnInitializeAttribute : Attribute
{
}