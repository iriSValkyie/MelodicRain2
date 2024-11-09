using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MelodicRain2
{
    /// <summary>
    /// StreamingAssetsに対してのアセットローダー
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class StreamingAssetsLoader<T>:IAssetLoadable<T>
    {
        private UnityWebRequest unityWebRequest;
        
        public async UniTask<T> Load(string path)
        {
            if (!System.IO.File.Exists(path.Replace("file://", "")))
            {
                Debug.LogError($"StreamingAssetsLoader:File {path} does not exist");
                return default;
            }
            unityWebRequest = UnityWebRequest.Get(path);
            await unityWebRequest.SendWebRequest();
            return Convert(unityWebRequest);
        }

        /// <summary>
        /// ジェネリック型に変換するための処理
        /// </summary>
        /// <param name="unityWebRequest"></param>
        /// <returns></returns>
        protected abstract T Convert(UnityWebRequest unityWebRequest);
    }
}