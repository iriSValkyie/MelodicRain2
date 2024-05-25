using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MelodicRain2.Core
{
    public class GameSingleton : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
