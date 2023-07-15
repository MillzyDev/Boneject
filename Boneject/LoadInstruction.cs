using System;

namespace Boneject
{
    internal struct LoadInstruction
    {
        public readonly Type BaseModule;
        public readonly Action<BonejectKernel> OnLoad;

        public LoadInstruction(Type baseModule, Action<BonejectKernel> onLoad)
        {
            BaseModule = baseModule;
            OnLoad = onLoad;
        }
    }
}
