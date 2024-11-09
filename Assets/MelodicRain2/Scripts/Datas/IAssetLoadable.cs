using Cysharp.Threading.Tasks;

namespace MelodicRain2
{
    /// <summary>
    /// StreamingAssetsや後々対応するAssetBundleに対する共通化対応
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAssetLoadable<T>
    {
        public UniTask<T> Load(string path);
    }
}