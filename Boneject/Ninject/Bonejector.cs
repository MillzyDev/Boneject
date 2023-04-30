using System;
using System.Collections.Generic;
using Boneject.Ninject.Filters;
using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject.Modules;

namespace Boneject.Ninject;

public class Bonejector
{
    private readonly MelonInfoAttribute _modInfo;
    private readonly HashSet<LoadSet> _loadSets = new();
    private readonly HashSet<LoadInstruction> _loadInstructions = new();


    internal Bonejector(MelonInfoAttribute info)
    {
        _modInfo = info;
    }
    
    internal MelonInfoAttribute ModInfo => _modInfo;
    internal IEnumerable<LoadSet> LoadSets => _loadSets;
    internal IEnumerable<LoadInstruction> LoadInstructions => _loadInstructions;

    public void Load<T>(Context context, params object[] parameters) where T : INinjectModule
    {
        var moduleTypes = ModuleForContext(context);
        ILoadFilter filter = new MultiTypedLoadFilter(moduleTypes);
        _loadSets.Add(new LoadSet(typeof(T), filter, parameters.Length != 0 ? parameters : null));
    }

    public void Load(Context context, Action<BonejectKernel> loadCallback)
    {
        foreach (var module in ModuleForContext(context))
            _loadInstructions.Add(new LoadInstruction(module, loadCallback));
    }

    private static IEnumerable<Type> ModuleForContext(Context context)
    {
        HashSet<Type> moduleTypes = new();
        
        if (context.HasFlag(Context.App))
            moduleTypes.Add(typeof(AppModule));
        if (context.HasFlag(Context.Loading))
            moduleTypes.Add(typeof(LoadingModule));
        if (context.HasFlag(Context.Campaign))
            moduleTypes.Add(typeof(CampaignModule));
        if (context.HasFlag(Context.Hub))
            moduleTypes.Add(typeof(HubModule));
        if (context.HasFlag(Context.Startup))
            moduleTypes.Add(typeof(StartupModule));
        if (context.HasFlag(Context.VoidG114))
            moduleTypes.Add(typeof(VoidG114Module));
        if (context.HasFlag(Context.Player))
            moduleTypes.Add(typeof(PlayerModule));

        return moduleTypes;
    }
}