using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MelodicRain2.Core
{
    public class SceneLoader : MonoBehaviour
    {

        public static SceneLoader Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public async UniTask<bool> LoadScene()
        {
            return true;
        }
    }
}

