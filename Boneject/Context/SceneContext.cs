using System;
using MelonLoader;
using UnityEngine;

namespace Boneject.Context
{
    [RegisterTypeInIl2Cpp]
    public class SceneContext : MonoBehaviour
    {
        public SceneContext(IntPtr ptr) : base(ptr)
        {
        }
    }
}
