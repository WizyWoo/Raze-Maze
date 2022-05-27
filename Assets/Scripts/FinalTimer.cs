using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Com.MyCompany.MyGame;

public class FinalTimer : MonoBehaviour
{
    [SerializeField] GameObject panel, timerObj;
    [SerializeField] Image timeImage;
    [SerializeField] Text timeText;
    [SerializeField] float duration, currentTime;

    void Start()
    {
        panel.SetActive(false);
        timerObj.SetActive(true);
        currentTime = duration;
        timeText.text = currentTime.ToString();
        StartCoroutine(CountdownTimer());
    }

    public IEnumerator CountdownTimer()
    {
        while(currentTime >= 0)
        {
            timeImage.fillAmount = Mathf.InverseLerp(0, duration, currentTime);
            timeText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }
        OpenPanel();

        yield return new WaitForSeconds(0.5f);
        timerObj.SetActive(false);
        panel.SetActive(false);
        GameManager.gameManager.LeaveRoom();
    }

    void OpenPanel()
    {
        timeText.text = "";
        panel.SetActive(true);
    }
}
