using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    [SerializeField] private GameObject[] slotList = new GameObject[9];
    private Timer timer;
    private Slot slot;
    private GameObject gameWinScreen;
    private int points;
    private int winPoints;
    private bool hasWon = false;
    public bool HasWon => hasWon;
    void Start()
    {
        winPoints = slotList.Count();
        timer = FindFirstObjectByType<Timer>();
        gameWinScreen = FindFirstObjectByType<Won>().gameObject;
    }

    void Update()
    {
        Debug.Log(hasWon);
        if (!hasWon)
        {    
            points = 0;

            for (int i = 0; i < winPoints; i++)
            {
                slot = slotList[i].GetComponent<Slot>();
                if (slot.IsPieceCorrect)
                    points++;
            }

            if (points == winPoints)
                hasWon = true;
        }
        else
        {
            Debug.Log("YOU WON!!!");
            gameWinScreen.transform.GetChild(0).gameObject.SetActive(true);
            timer.StopTimer();
        }
    }
}
