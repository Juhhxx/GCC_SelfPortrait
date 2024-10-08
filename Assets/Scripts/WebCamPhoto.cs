using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WebCamPhoto : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    [SerializeField] private GameObject[] allPieces = new GameObject[9];

    #if UNITY_IOS || UNITY_WEBGL
    private bool CheckPermissionAndRaiseCallbackIfGranted(UserAuthorization authenticationType)
    {
        if (Application.HasUserAuthorization(authenticationType))
        {
            InitializeCamera();
        }
        return false;
    }

    private IEnumerator AskForPermissionIfRequired(UserAuthorization authenticationType)
    {
        if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType))
        {
            yield return Application.RequestUserAuthorization(authenticationType);
            if (!CheckPermissionAndRaiseCallbackIfGranted(authenticationType))
                Debug.LogWarning($"Permission {authenticationType} Denied");
        }
    }
    #endif
    
    void Start()
    {
        #if UNITY_IOS || UNITY_WEBGL
        StartCoroutine(AskForPermissionIfRequired(UserAuthorization.WebCam));
        return;
        #else
        InitializeCamera();
        #endif
    }
    void InitializeCamera()
    {
        webCamTexture = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TakePhoto());
        }
    }
    public void PhotoTaker()
    {
        StartCoroutine(TakePhoto());
    }
    IEnumerator TakePhoto()
    {
        webCamTexture.Play();
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        webCamTexture.Stop();
        // byte[] bytes = photo.EncodeToPNG();
        // File.WriteAllBytes("Assets\\Sprites\\" + "photo.png", bytes);
        // Debug.Log($"Take Photo\nSave at: Assets\\Sprites\\photo.png");

        Rect photoFrame = new Rect(360,480,240,240);
        Vector2 photoPivot = new Vector2(120,120);

        for (int i = 0; i < 9; i++)
        {
            Sprite Piece = Sprite.Create(photo,photoFrame,photoPivot);

            allPieces[i].GetComponent<Image>().sprite = Piece;
            // Debug.Log($"Set Piece {i} : Slice at ({photoFrame.x},{photoFrame.y})");

            if ((i + 1) % 3 == 0)
            {
                photoFrame.x -= 480;
                photoFrame.y -= 240;
            }
            else
                photoFrame.x += 240;
        }

    }
}
