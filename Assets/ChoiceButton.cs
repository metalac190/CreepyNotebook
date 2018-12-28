using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class ChoiceButton : MonoBehaviour
{
    [SerializeField] Button button;
    public Button Button { get { return button; } }
    [SerializeField] TextMeshProUGUI textUI;
    public string Text { get { return textUI.text; } }
    [SerializeField] float xAnimOffset = 100f;

    public ChoiceData Choice { get; private set; }
    public bool IsChoiceActive { get; private set; }

    float startXPos;

    Coroutine animationInstance;
    Image buttonImage;
    CanvasGroup canvasGroup;
    GameController gameController;
    RevealChoices revealChoices;

    public void Initialize(GameController gameController, RevealChoices revealChoices)
    {
        // inject
        this.gameController = gameController;
        this.revealChoices = revealChoices;
        // local refs
        canvasGroup = GetComponent<CanvasGroup>();
        buttonImage = GetComponent<Image>();

        startXPos = transform.localPosition.x;
        buttonImage.raycastTarget = false;
        // enforce off default state, in case a designer has left it enabled
        gameObject.SetActive(false);
    }

    public void PrepareNewChoice(GameController gameController, ChoiceData choice)
    {
        // inject
        this.Choice = choice;

        SetupButton();
        SetText(choice.ChoiceText);
        SetChoiceActive(true);
    }

    void SetupButton()
    {
        // make sure we've disposed of our previous listeners
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(delegate { gameController.ApplyChoice(Choice); });
        button.onClick.AddListener(revealChoices.DisableChoices);
    }

    public void SetChoiceActive(bool isChoiceActive)
    {
        IsChoiceActive = isChoiceActive;
    }

    public void Show(float startDelayInSeconds, float showSpeedInSeconds)
    {
        // ensure our gameObject is active
        gameObject.SetActive(true);

        if(animationInstance != null)
        {
            StopCoroutine(animationInstance);
        }

        animationInstance = StartCoroutine(AnimateShow(startDelayInSeconds, showSpeedInSeconds));
    }

    public void Hide(float startDelayInSeconds, float hideSpeedInSeconds)
    {
        // if we're currently animating, kill it early to start our new one
        if(animationInstance != null)
        {
            StopCoroutine(animationInstance);
        }

        animationInstance = StartCoroutine(AnimateHide(startDelayInSeconds, hideSpeedInSeconds));

    }

    public void SetText(string newText)
    {
        textUI.text = newText;
    }

    IEnumerator AnimateShow(float startDelay, float showSpeedInSeconds)
    {
        float currentAlpha = 0;
        canvasGroup.alpha = 0;
        // x offset data
        float animStartXPos = startXPos - xAnimOffset;
        float animEndXPos = startXPos;
        float currentXPos = animStartXPos;

        yield return new WaitForSeconds(startDelay);

        for (float t = 0f; t < 1.0f; t+= Time.deltaTime / showSpeedInSeconds)
        {
            // opacity animate
            currentAlpha = Mathf.Lerp(0f, 1f, t);
            canvasGroup.alpha = currentAlpha;
            // position animate
            currentXPos = Mathf.Lerp(animStartXPos, animEndXPos, t);
            transform.localPosition = new Vector2(currentXPos, transform.localPosition.y);
            yield return null;
        }
        // ensure we hit our final value
        canvasGroup.alpha = 1;
        transform.localPosition = new Vector2(animEndXPos, transform.localPosition.y);
        // only allow button to be clickable if animation has finished
        buttonImage.raycastTarget = true;
    }

    IEnumerator AnimateHide(float startDelay, float hideSpeedInSeconds)
    {
        float currentAlpha = 1;
        canvasGroup.alpha = 1;
        buttonImage.raycastTarget = false;

        yield return new WaitForSeconds(startDelay);

        for (float t = 1f; t > 0; t -= Time.deltaTime / hideSpeedInSeconds)
        {
            currentAlpha = Mathf.Lerp(0, 1f, t);
            canvasGroup.alpha = currentAlpha;
            yield return null;
        }
        // ensure we hit our final value
        canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }
}
