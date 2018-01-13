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
    public Slider speedSlider;
    public Text currentLapText;
    public Text totalLapsText;
    public Text minutesText;
    public Text secondsText;
    public Text speedText;
    public Text countDownText;
    public Text roundResultText;
    public Text resultText;
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

    private float targetSlideValue;

    // Use this for initialization
    void Start()
    {
        targetSlideValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (speedSlider.value < targetSlideValue - 0.01f || speedSlider.value > targetSlideValue + 0.01f)
            speedSlider.value = Mathf.Lerp(speedSlider.value, targetSlideValue, Time.deltaTime * lerpSpeed);
        else
            speedSlider.value = targetSlideValue;
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
        minutesText.text = minutes.ToString() + "'";
    }

    public void SetSeconds(int seconds)
    {
        secondsText.text = seconds.ToString() + "''";
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
        if (slideValue <= 1 || slideValue >= 0)
            targetSlideValue = slideValue;
        else
            targetSlideValue = 0;
    }

    public void SetSpeedText(int speed)
    {
        speedText.text = speed.ToString();
    }

    public void SetCountDownText(string text)
    {
        countDownText.text = text;
    }

    public void SetRoundResultText(string text)
    {
        roundResultText.text = text;
    }

    public void SetResultText(string text)
    {
        resultText.text = text;
    }
}
