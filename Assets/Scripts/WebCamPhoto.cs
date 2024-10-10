using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WebCamPhoto : MonoBehaviour
{
    [SerializeField] private Image endAnimImage;
    private WebCamTexture webCamTexture;
    private Drag[] allPieces;
    
    void Start()
    {
        allPieces = FindObjectsByType<Drag>(0);
        InitializeCamera();
        
    }
    private void InitializeCamera()
    {
        webCamTexture = new WebCamTexture();
        webCamTexture.Play();
        // GetComponent<Renderer>().material.mainTexture = webCamTexture;
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
        yield return new WaitForEndOfFrame();

        
        Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
        photo.SetPixels(webCamTexture.GetPixels());
        photo.Apply();

        webCamTexture.Stop();
        byte[] bytes = photo.EncodeToPNG();
        File.WriteAllBytes("Assets\\Sprites\\" + "photo.png", bytes);
        // Debug.Log($"Take Photo\nSave at: Assets\\Sprites\\photo.png");

        Rect photoFrame = new Rect(360,480,240,240);
        Vector2 photoPivot = new Vector2(120,120);

        Sprite completedImage = Sprite.Create(photo,new Rect(360,0,720,720),new Vector2(360,360));
        endAnimImage.sprite = completedImage;

        for (int i = 0; i < 9; i++)
        {
            Sprite Piece = Sprite.Create(photo,photoFrame,photoPivot);

            allPieces[i].gameObject.GetComponent<Image>().sprite = Piece;
            allPieces[i].Id = i + 1;
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