using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WebCamPhoto : MonoBehaviour
{
    private WebCamTexture webCamTexture;
    [SerializeField] private GameObject[] allPieces = new GameObject[9];
    Rect photoFrame = new Rect(0,0,720,720);
    Vector2 photoPivot = new Vector2(600,240);
    void Start()
    {
        webCamTexture = new WebCamTexture();
        // GetComponent<Renderer>().material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(TakePhoto());
        }
    }
    IEnumerator TakePhoto()
    {
        yield return new WaitForEndOfFrame();

        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();
        
        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes("Assets\\Sprites\\" + "photo.png", bytes);
        Debug.Log($"Take Photo\nSave at: Assets\\Sprites\\photo.png");

        
        // Sprite Piece = Sprite.Create(photo,photoFrame,photoPivot);

        // allPieces[0].GetComponent<Image>().sprite = Piece;

    }
}
