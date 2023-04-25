using System.Collections.Generic;
using System.Linq;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;
using Ninject;

namespace Boneject.MelonLoader;

public static class ModInitInjector
{
    public delegate object? ModInitLoadCallback(InjectableMelonMod mod);

    private static readonly INinjectSettings _ninjectSettings = new NinjectSettings
    {
        AllowNullInjection = true,
        MethodInjection = true,
        PropertyInjection = false,
        ThrowOnGetServiceNotFound = false,
#pragma warning disable CS0618
        InjectNonPublic = true,
#pragma warning restore CS0618
        InjectAttribute = typeof(OnInitializeAttribute),
        CachePruningInterval = default,
        DefaultScopeCallback = null,
        LoadExtensions = false,
        ExtensionSearchPatterns = new string[] {},
        UseReflectionBasedInjection = false,
        ActivationCacheDisabled = false
    };
    
    private static readonly HashSet<ModInitLoadCallback> _loadInstructions = new();
    

    public static void AddInjector(ModInitLoadCallback loadCallback) 
        => _loadInstructions.Add(loadCallback);

    internal static void Inject(InjectableMelonMod mod)
    {
        BonejectKernel kernel = new(_ninjectSettings);

        foreach (var obj in _loadInstructions.Select(loadCallback => loadCallback(mod)).Where(obj => obj != null))
        {
            kernel.Bind(obj?.GetType()).ToConstant(obj).InSingletonScope();
        }
        
        kernel.Inject(mod);
    }
}