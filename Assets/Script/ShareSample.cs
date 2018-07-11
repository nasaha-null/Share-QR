using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class ShareSample : MonoBehaviour
{
    public void ShareScreenShot()
    {
        StartCoroutine(Share());
    }

    IEnumerator Share()
    {
        //スクリーンショットの保存先と名前(重複対策に実行時間を付与)を設定
        string fileName = String.Format("image_{0:yyyyMMdd_Hmmss}.png", DateTime.Now);
        string imagePath = Application.persistentDataPath + "/" + fileName;

        // スクリーンショットの撮影と待機
        ScreenCapture.CaptureScreenshot(fileName);
        yield return new WaitForEndOfFrame();

        // Shareするメッセージの設定と待機
        string text = "hoge";
        string URL = "foo";
        yield return new WaitForSeconds(1);

        //Shareする
        SocialConnector.SocialConnector.Share(text, URL, imagePath);
    }
}