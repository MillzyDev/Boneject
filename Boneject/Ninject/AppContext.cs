using System;
using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject;
using UnityEngine;

namespace Boneject.Ninject
{
    [RegisterTypeInIl2Cpp]
    public class AppContext : MonoBehaviour
    {
        private IKernel? _kernel;
        
        public AppContext(IntPtr ptr) : base(ptr)
        {
        }

        [Inject]
        public void Inject(IKernel kernel)
        {
            _kernel = kernel;
        }

        private void Start()
        {
            BonejectManager bonejectManager = Mod.BonejectManager;

            var baseModule = new AppModule(gameObject.GetInstanceID(), bonejectManager);
            _kernel!.Load(baseModule);

            MelonLogger.Msg("VoidG114 context loaded.");
        }
    }
}
