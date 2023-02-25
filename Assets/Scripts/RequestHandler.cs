using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class RequestHandler {
    
    public static IEnumerator GetRequest(string uri, bool isImage, System.Action<DownloadHandler> callback = null) {
        
        using (UnityWebRequest webRequest = isImage ? 
                UnityWebRequestTexture.GetTexture(uri) : UnityWebRequest.Get(uri)) {

            yield return webRequest.SendWebRequest();

            if (!webRequest.result.Equals(UnityWebRequest.Result.Success)) {
                Debug.LogError($"Erro: {webRequest.error}");
            } else {
                callback.Invoke(webRequest.downloadHandler);
            }
        }
    }
}