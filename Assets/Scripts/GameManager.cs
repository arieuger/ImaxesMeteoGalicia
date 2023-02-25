using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    private const string API_URI = "https://servizos.meteogalicia.gal/mgrss/observacion/jsonCamaras.action";

    [SerializeField] private RawImage image;
    [SerializeField] private TMP_Text loadingText;
    private CameraList cameraList;
    private int index;
    
    void Start() {
        loadNewImage();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !loadingText.IsActive()) {
            loadNewImage();
        }

        if (Input.GetKeyDown(KeyCode.R) && !loadingText.IsActive()) {
            loadImageTexture();
        }
    }

    private void loadNewImage() {
        loadingText.gameObject.SetActive(true);
        StartCoroutine(RequestHandler.GetRequest(API_URI, false, handler => {
            cameraList = JsonUtility.FromJson<CameraList>(handler.text);
            index = Random.Range(0, cameraList.listaCamaras.Count);
            loadImageTexture();
        }));
    }

    private void loadImageTexture() {
        loadingText.gameObject.SetActive(true);
        StartCoroutine(RequestHandler.GetRequest(cameraList.listaCamaras[index].imaxeCamara, true, handler => {
            image.texture = ((DownloadHandlerTexture)handler).texture;
            loadingText.gameObject.SetActive(false);
        }));
    }
}
