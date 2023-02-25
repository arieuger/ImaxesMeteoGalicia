using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private const string API_URI = "https://servizos.meteogalicia.gal/mgrss/observacion/jsonCamaras.action";

    [SerializeField] private RawImage image;

    private CameraList cameraList;
    
    void Start() {
        loadNewImage();
    }

    void Update() {
        
    }

    private void loadNewImage() {
        StartCoroutine(RequestHandler.GetRequest(API_URI, false, handler => {
            cameraList = JsonUtility.FromJson<CameraList>(handler.text);
            Debug.Log(DateTime.Parse(cameraList.listaCamaras[0].dataUltimaAct));
            
            StartCoroutine(RequestHandler.GetRequest(cameraList.listaCamaras[0].imaxeCamara, true, handler => {
                image.texture = ((DownloadHandlerTexture)handler).texture;
            }));
        }));
    }
}
