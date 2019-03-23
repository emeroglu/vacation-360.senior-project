using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class App : MonoBehaviour
{
    private bool app;

    private bool touch;
    private Vector3 touchOffset;

    private UniWebView webview;

    private Dictionary<string, AssetBundle> bundles;

    private void Start()
    {
        app = false;

        bundles = new Dictionary<string, AssetBundle>();

        Input.compass.enabled = true;
        Input.gyro.enabled = true;

        GameObject go = new GameObject("WebView");

        webview = go.AddComponent<UniWebView>();

        webview.autoShowWhenLoadComplete = false;
        webview.backButtonEnable = false;
        webview.SetBackgroundColor(new Color(1, 1, 1, 0));
        webview.SetHorizontalScrollBarShow(false);
        webview.SetShowSpinnerWhenLoading(false);
        webview.SetVerticalScrollBarShow(false);
        webview.ShowToolBar(false);
        webview.toolBarShow = false;
        webview.zoomEnable = false;

        webview.insets = new UniWebViewEdgeInsets(0, 0, 0, 0);
        webview.CleanCache();

        webview.url = "http://va.emeroglu.com/App/Index";

        webview.Load();

        webview.OnLoadComplete += webview_OnLoadComplete;
        webview.OnReceivedMessage += webview_OnReceivedMessage;
    }

    private void webview_OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
        webview.Show();
    }

    private void webview_OnReceivedMessage(UniWebView webView, UniWebViewMessage message)
    {
        if (message.path == "photo")
        {
            int id = int.Parse(message.args["id"]);

            string url = "http://va.emeroglu.com/Skyboxes/skybox_" + id + ".unity3d";

            StartCoroutine(Download(url));
        }
    }

    private IEnumerator Download(string url)
    {
        WWW www = new WWW(url);
        yield return www;

        bundles[url] = www.assetBundle;

        Material skybox = www.assetBundle.LoadAllAssets<Material>()[0];

        RenderSettings.skybox = skybox;

        webview.Hide(true, UniWebViewTransitionEdge.Left, 0.4f, null);

        app = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (app)
            {
                app = false;

                webview.Show(true, UniWebViewTransitionEdge.Left, 0.4f, null);
            }
            else
                Application.Quit();
        }

        if (app)
        {
            if (Input.GetMouseButtonDown(0))
                touch = true;

            if (Input.GetMouseButtonUp(0))
                touch = false;

            if (touch)
            {
                float x = (Input.mousePosition.x / Screen.width) * 720;
                float y = (Input.mousePosition.y / Screen.height) * 180;

                if (y < 60)
                    y = 60;

                if (120 < y)
                    y = 120;

                y -= 90;

                touchOffset = new Vector3(x, y, 0);

                Quaternion deviceRotation = Input.gyro.attitude;
                Quaternion cameraRotation = Camera.main.transform.localRotation;
                Quaternion gyroOffset;

                float p1 = 2 * ((deviceRotation.x * deviceRotation.y) + (deviceRotation.z * deviceRotation.w));
                float p2 = 1 - (2 * ((deviceRotation.y * deviceRotation.y) + (deviceRotation.z * deviceRotation.z)));

                float roll = Mathf.Atan2(p1, p2) * (180 / Mathf.PI);

                p1 = 2 * ((deviceRotation.x * deviceRotation.z) - (deviceRotation.y * deviceRotation.w));

                float pitch = Mathf.Asin(p1) * (180 / Mathf.PI);

                p1 = 2 * ((deviceRotation.x * deviceRotation.w) + (deviceRotation.y * deviceRotation.z));
                p2 = 1 - (2 * ((deviceRotation.z * deviceRotation.z) + (deviceRotation.w * deviceRotation.w)));

                float yaw = (Mathf.Atan2(p1, p2) * (180 / Mathf.PI)) - 90;

                gyroOffset = Quaternion.Euler(yaw - touchOffset.y, -roll + touchOffset.x, pitch);

                if (0.5f < Quaternion.Angle(cameraRotation, gyroOffset))
                    Camera.main.transform.localRotation = Quaternion.Lerp(Camera.main.transform.localRotation, gyroOffset, 0.1f);
            }
        }
    }
}
