using System;
using Boneject.Modules;
using MelonLoader;
using Ninject;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace Boneject
{
    [RegisterTypeInIl2Cpp]
    public class AppContext : MonoBehaviour
    {
        private IKernel? _kernel;
        
        public AppContext(IntPtr ptr) : base(ptr)
        {
        }

        [HideFromIl2Cpp]
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

            MelonLogger.Msg("App context loaded.");
        }
    }
}
