using System;
using MelonLoader;
using UnityEngine;

namespace Boneject.Ninject
{
    [RegisterTypeInIl2Cpp]
    public class ContextUnloader : MonoBehaviour
    {
        public ContextUnloader(IntPtr ptr) : base(ptr)
        {
        }

        public static ContextUnloader AddToObject(GameObject gameObject)
        {
            var destroyDispatcher = gameObject.GetComponent<ContextUnloader>();
            return destroyDispatcher ? destroyDispatcher : gameObject.AddComponent<ContextUnloader>();
        }

        private void OnDestroy()
        {
            Mod.BonejectManager.UnloadForContext(gameObject.GetInstanceID());
        }
    }
}
