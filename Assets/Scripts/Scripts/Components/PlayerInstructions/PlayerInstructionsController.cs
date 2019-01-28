using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInstructionsController : EntityComponent {

    [SerializeField] GameObject startText;
    [SerializeField] GameObject lostText;
    private CanvasGroup canvasGroup;

    public override void InitializeComponent()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void UpdateComponent() {}

    public void ShowStartText(float _duration)
    {
        new WaitJob(delegate
        {
            startText.SetActive(true);
            StartCoroutine(FadeIn(2f));
            new WaitJob(delegate
            {
                StartCoroutine(FadeOut(2f));
                new WaitJob(delegate
                {
                    startText.SetActive(false);
                }, 2f);
            }, _duration);
        }, 1.5f);
    }

    public void ShowLostText()
    {
        lostText.SetActive(true);
        StartCoroutine(FadeIn(2f));
    }

    IEnumerator FadeIn(float _duration)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        canvasGroup.alpha = 0f;
        for(float t = 0f; t < _duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = t / _duration;
            yield return wait;
        }
    }

    IEnumerator FadeOut(float _duration)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        canvasGroup.alpha = 1f;
        for (float t = 0f; t < _duration; t += Time.deltaTime)
        {
            canvasGroup.alpha = 1 - (t / _duration);
            yield return wait;
        }
    }
}
