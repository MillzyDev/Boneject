using System;
using MelonLoader;
using UnityEngine;

namespace Boneject.Context
{
    [RegisterTypeInIl2Cpp]
    public class GameObjectContext : MonoBehaviour
    {
        public GameObjectContext(IntPtr ptr) : base(ptr)
        {
        }
    }
}
