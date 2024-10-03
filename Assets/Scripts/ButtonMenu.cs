using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private string buttonOneScene;
    [SerializeField] private string buttonTwoScene;
    public void ButtonOne()
    {
        SceneManager.LoadScene(buttonOneScene);
    }
    public void ButtonTwo()
    {
        SceneManager.LoadScene(buttonTwoScene);
    }
}
