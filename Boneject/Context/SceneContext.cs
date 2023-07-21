﻿using System;
using MelonLoader;
using Ninject;
using Ninject.Modules;
using UnityEngine;

namespace Boneject.Context
{
    [RegisterTypeInIl2Cpp]
    public class SceneContext : MonoBehaviour
    {
        [NonSerialized]
        public string ContractName = null!;
        public KernelConfiguration Kernel = null!;
        public BonejectManager BonejectManager = null!;
        public INinjectModule BaseModule = null!;
        
        public SceneContext(IntPtr ptr) : base(ptr)
        {
        }

        private void Awake()
        {
            ContractName = gameObject.scene.name;
        }

        public void Start()
        {
            
        }
    }
}
