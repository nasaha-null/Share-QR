using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;

public class QRmanager
{
    public string read(WebCamTexture cameraTexture)
    {
        BarcodeReader reader = new BarcodeReader();
        Color32[] color = cameraTexture.GetPixels32();
        int width = cameraTexture.width;
        int height = cameraTexture.height;
        Result result = reader.Decode(color,width,height);
        if(result != null){
            return result.Text;
        }
        return "-1";//エラー時
    }
}

public class QRReader : MonoBehaviour
{
    [SerializeField]
    private int width = 1920;
    [SerializeField]
    private int height = 1080;

    [SerializeField]
    private RawImage displayUI = null;

    private WebCamTexture webCamTexture = null;
    private QRmanager qrManager;

    public Text resultText;

    private IEnumerator Start()
    {
        //カメラの接続と許可の確認
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogFormat("Camera is not found.");
            yield break;
        }

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogFormat("Camera is not allowed.");
            yield break;
        }

        //取得したデバイスからテクスチャの生成
        WebCamDevice[] userCameraDevice = WebCamTexture.devices;
        webCamTexture = new WebCamTexture(userCameraDevice[0].name, width, height);

        //UIのRawImageにwebCamTextureを設定
        displayUI.texture = webCamTexture;

        //撮影開始
        webCamTexture.Play();

        //qrコード管理クラス
        this.qrManager = new QRmanager();
    }

    void Update()
    {
        //カメラから読み取り
        resultText.text = this.qrManager.read(webCamTexture);
    }
}
