using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool gameOver;
    public float startRunningSpeed = 3f;
    // [HideInInspector]
    public float actualRunningSpeed;
    public int coins = 0;
    public Text CoinsText;
    public Canvas GameOverCanvas;
    // private float boostDuration = 2f;
    
    void Start()
    {
        actualRunningSpeed = startRunningSpeed;
    }

    
    public void AddCoin(int number)
    {
        coins += number;
        RefreshCoinText();
    }
    public void GameOver()
    {
        gameOver = true;
        SaveManager.Instance.SaveGame();
        GameOverCanvas.gameObject.SetActive(true);
    }
    public void Boost(float speedMultiplier)
    {
        StopCoroutine("BoostTimer");
        actualRunningSpeed *= speedMultiplier;
        StartCoroutine(BoostTimer(3f));

    }

    void BoostOff()
    {
        StopCoroutine("BoostTimer");
        actualRunningSpeed = startRunningSpeed;
    }

    public void RefreshCoinText()
    {
        CoinsText.text = coins.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator BoostTimer(float duration)
    {
        
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
       BoostOff();
    }
}
