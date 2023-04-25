using System;

namespace Boneject;

public struct InstallSet
{
    public readonly Type moduleType;
    public readonly IInstallFilter installFilter;

    public readonly object[]? initialParameters;

    public InstallSet(Type moduleType, IInstallFilter installFilter, object[]? initialParameters)
    {
        this.moduleType = moduleType;
        this.installFilter = installFilter;
        this.initialParameters = initialParameters;
    }
}