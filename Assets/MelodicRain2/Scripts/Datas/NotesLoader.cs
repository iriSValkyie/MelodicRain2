using UnityEngine;
using UnityEngine.Networking;

namespace MelodicRain2
{
    public class NotesLoader:StreamingAssetsLoader<NotesData>
    {
        protected override NotesData Convert(UnityWebRequest unityWebRequest)
        {
            string rawText = System.Text.Encoding.UTF8.GetString(unityWebRequest.downloadHandler.data, 3, unityWebRequest.downloadHandler.data.Length - 3);
            return JsonUtility.FromJson<NotesData>(rawText);
        }
    }
}