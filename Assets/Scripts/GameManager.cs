using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private const string API_URI = "https://servizos.meteogalicia.gal/mgrss/observacion/jsonCamaras.action";

    [SerializeField] private RawImage image;
    private CameraList cameraList;
    private bool isLoading;
    private int index;
    
    void Start() {
        isLoading = false;
        loadNewImage();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !isLoading) {
            loadNewImage();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isLoading) {
            loadImageTexture();
        }
    }

    private void loadNewImage() {
        isLoading = true;
        StartCoroutine(RequestHandler.GetRequest(API_URI, false, handler => {
            cameraList = JsonUtility.FromJson<CameraList>(handler.text);
            index = Random.Range(0, cameraList.listaCamaras.Count);
            loadImageTexture();
        }));
    }

    private void loadImageTexture() {
        isLoading = true;
        StartCoroutine(RequestHandler.GetRequest(cameraList.listaCamaras[index].imaxeCamara, true, handler => {
            image.texture = ((DownloadHandlerTexture)handler).texture;
            isLoading = false;
        }));
    }
}
