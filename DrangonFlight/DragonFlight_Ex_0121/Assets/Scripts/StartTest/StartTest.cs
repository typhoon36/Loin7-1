using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartTest : MonoBehaviour
{
    private Text starttext;

    private Coroutine blinkCoroutine;

    void Start()
    {
        starttext = GetComponent<Text>();

        blinkCoroutine = StartCoroutine(BlinkText(0.5f));

        StartCoroutine(CountdownAndStopBlink(3));

    }


    //텍스트를 주기적으로 켜고 끄는 코루틴
    IEnumerator BlinkText(float interval)
    {
        while (true)
        {
            if (starttext != null)
            {
                starttext.enabled = !starttext.enabled;
            }
            yield return new WaitForSeconds(interval);
        }
    }


    //카운트다운을 표시하고 끝나면 깜박임 코루틴을 중단
    IEnumerator CountdownAndStopBlink(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            starttext.text = "CountDown : " + i;

            yield return new WaitForSeconds(1f);
        }

        starttext.text = "Go!";

        StopCoroutine(blinkCoroutine);
        starttext.enabled = false;

    }


}
