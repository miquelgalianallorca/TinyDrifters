using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public Image firstPosImage;
    public Image secondPosImage;
    public Image thirdPosImage;
    public Image fourthPosImage;
    public Slider p1SpeedSlider;
    public Slider p2SpeedSlider;
    public Text currentLapText;
    public Text totalLapsText;
    public Text minutesText;
    public Text secondsText;
    public Text p1SpeedText;
    public Text p2SpeedText;
    public Text countDownText;
    public Text resultText;
    public Text p1LivesText;
    public Text p2LivesText;
    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public Sprite com1Sprite;
    public Sprite com2Sprite;
    public Sprite com3Sprite;
    public GameObject racePanel;
    public GameObject versusPanel;

    public enum Racers
    {
        player1,
        player2,
        com1,
        com2,
        com3
    };
    public float lerpSpeed;

    private float p1TargetSlideValue;
    private float p2TargetSlideValue;

    // Use this for initialization
    void Start()
    {
        p1TargetSlideValue = 0;
        p2TargetSlideValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (p1SpeedSlider.value < p1TargetSlideValue - 0.01f || p1SpeedSlider.value > p1TargetSlideValue + 0.01f)
            p1SpeedSlider.value = Mathf.Lerp(p1SpeedSlider.value, p1TargetSlideValue, Time.deltaTime * lerpSpeed);
        else
            p1SpeedSlider.value = p1TargetSlideValue;

        if (p2SpeedSlider.value < p2TargetSlideValue - 0.01f || p2SpeedSlider.value > p2TargetSlideValue + 0.01f)
            p2SpeedSlider.value = Mathf.Lerp(p2SpeedSlider.value, p2TargetSlideValue, Time.deltaTime * lerpSpeed);
        else
            p2SpeedSlider.value = p2TargetSlideValue;
    }

    private Sprite GetSpriteByRacer(Racers racer)
    {
        if (racer == Racers.player1)
        {
            return player1Sprite;
        }
        else if (racer == Racers.player2)
        {
            return player2Sprite;
        }
        else if (racer == Racers.com1)
        {
            return com1Sprite;
        }
        else if (racer == Racers.com2)
        {
            return com2Sprite;
        }
        else if (racer == Racers.com3)
        {
            return com3Sprite;
        }
        return null;
    }

    public void SetMinutes(int minutes)
    {
        minutesText.text = minutes.ToString("D2") + "'";
    }

    public void SetSeconds(int seconds)
    {
        secondsText.text = seconds.ToString("D2") + "''";
    }

    public void SetTotalLaps(int laps)
    {
        totalLapsText.text = laps.ToString();
    }

    public void SetCurrentLap(int lap)
    {
        currentLapText.text = lap.ToString();
    }

    public void SetFirstPosition(Racers racer)
    {
        firstPosImage.sprite = GetSpriteByRacer(racer);
    }

    public void SetFirstPosition(Sprite sprite)
    {
        firstPosImage.sprite = sprite;
    }

    public void SetSecondPosition(Sprite sprite)
    {
        secondPosImage.sprite = sprite;
    }

    public void SetThirdPosition(Sprite sprite)
    {
        thirdPosImage.sprite = sprite;
    }

    public void SetFourthPosition(Sprite sprite)
    {
        fourthPosImage.sprite = sprite;
    }

    public void SetSecondPosition(Racers racer)
    {
        secondPosImage.sprite = GetSpriteByRacer(racer);
    }

    public void SetThirdPosition(Racers racer)
    {
        thirdPosImage.sprite = GetSpriteByRacer(racer);
    }

    public void SetFourthPosition(Racers racer)
    {
        fourthPosImage.sprite = GetSpriteByRacer(racer);
    }

    public void SetSpeedSlider(float slideValue)
    {
        
    }

    //public void SetSpeedText(int speed)
    //{
    //    speedText.text = speed.ToString();
    //}

    public void SetCountDownText(string text)
    {
        countDownText.text = text;
    }

    public void SetResultText(string text)
    {
        resultText.text = text;
    }

    public void PrintTime(float totalTime)
    {
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);
        SetSeconds(seconds);
        SetMinutes(minutes);
    }

    public void SetLives(int p1Lives, int p2Lives)
    {
        p1LivesText.text = p1Lives.ToString();
        p2LivesText.text = p2Lives.ToString();
    }

    public void ActivateDemoUI()
    {
        gameObject.SetActive(false);
    }

    public void ActivateRaceUI()
    {
        gameObject.SetActive(true);
        racePanel.SetActive(true);
        versusPanel.SetActive(false);
        UpdateP1Speed(0,0);

    }

    public void ActivateVersusUI()
    {
        gameObject.SetActive(true);
        versusPanel.SetActive(true);
        racePanel.SetActive(false);
        UpdateP1Speed(0, 0);
        UpdateP2Speed(0, 0);
    }

    public void UpdateP1Speed(float currentSpeed, float maxSpeed)
    {
        p1SpeedText.text = Mathf.RoundToInt(Mathf.Clamp(currentSpeed, 0, maxSpeed)).ToString();
        p1TargetSlideValue = Mathf.Clamp(currentSpeed, 0, maxSpeed) / maxSpeed;
    }

    public void UpdateP2Speed(float currentSpeed, float maxSpeed)
    {
        p2SpeedText.text = Mathf.RoundToInt(Mathf.Clamp(currentSpeed, 0, maxSpeed)).ToString();
        p2TargetSlideValue = Mathf.Clamp(currentSpeed, 0, maxSpeed) / maxSpeed;
    }

}
