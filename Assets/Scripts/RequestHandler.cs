using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;

public class RequestHandler {
    
    public static IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {

            yield return webRequest.SendWebRequest();

            if (!webRequest.result.Equals(UnityWebRequest.Result.Success)) {
                Debug.LogError($"Erro: {webRequest.error}");
            } else {
                CameraList requestResult = JsonUtility.FromJson<CameraList>(webRequest.downloadHandler.text);
                Debug.Log(DateTime.Parse(requestResult.listaCamaras[0].dataUltimaAct));
            }
        }
    }
}