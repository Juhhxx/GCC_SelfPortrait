using System.Linq;
using UnityEngine;

public class WinCheck : MonoBehaviour
{
    private Slot[] slotListWin;
    private Timer timer;
    private Slot slot;
    private GameObject gameWinScreen;
    private int points;
    private int winPoints;
    private bool hasWon = false;
    public bool HasWon => hasWon;
    void Start()
    {
        slotListWin = FindObjectsByType<Slot>(0);
        winPoints = slotListWin.Count();
        timer = GameObject.Find("Timer").GetComponent<Timer>();
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
                slot = slotListWin[i];
                if (slot.IsPieceCorrect)
                    points++;
            }

            if (points == winPoints)
                hasWon = true;
        }
        else
        {
            Debug.Log("YOU WON!!!");
            timer.StopTimer();
            gameWinScreen.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
