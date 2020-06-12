using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Button))]
public class ChoiceButton : MonoBehaviour, IDisplayable
{
    [SerializeField] TextMeshProUGUI _textView = null;
    public string Text => _textView.text;

    [Header("Animation Settings")]
    [SerializeField] float xAnimOffset = 100f;
    [SerializeField] float _startDelayInSeconds = .2f;
    [SerializeField] float _showSpeedInSeconds = .2f;
    [SerializeField] float _hideSpeedInSeconds = .2f;

    //public ChoiceData Choice { get; private set; }
    public bool IsChoiceActive { get; private set; }

    float _startXPos = 0;
    Button _buttonView = null;
    Image _buttonImage;
    CanvasGroup _canvasGroup = null;
    Coroutine _animationCoroutine = null;

    private void Awake()
    {
        _buttonView = GetComponent<Button>();
        _buttonImage = _buttonView.image;
        _canvasGroup = GetComponent<CanvasGroup>();

        _startXPos = transform.localPosition.x;
        _buttonImage.raycastTarget = false;
        // enforce off default state, in case a designer has left it enabled
        gameObject.SetActive(false);
    }

    /*
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
    */

    /*
    public void PrepareNewChoice(ChoiceData choice)
    {
        // inject
        this.Choice = choice;

        SetupButton();
        SetText(choice.ChoiceText);
        SetChoiceActive(true);
    }
    */

    //TODO
    public void LoadNewChoice()
    {
        // load the choice
        // setup the button
        // set the text from choice
        // enable choice clickable
    }

    void SetupButton()
    {
        // make sure we've disposed of our previous listeners
        _buttonView.onClick.RemoveAllListeners();

        //_buttonView.onClick.AddListener(delegate { gameController.ApplyChoice(Choice); });
        //_buttonView.onClick.AddListener(revealChoices.DisableChoices);
    }

    public void SetText(string newText)
    {
        _textView.text = newText;
    }

    IEnumerator AnimateShow(float startDelay, float showSpeedInSeconds)
    {
        float currentAlpha = 0;
        _canvasGroup.alpha = 0;
        // x offset data
        float animStartXPos = _startXPos - xAnimOffset;
        float animEndXPos = _startXPos;
        float currentXPos = animStartXPos;

        yield return new WaitForSeconds(startDelay);

        for (float t = 0f; t < 1.0f; t += Time.deltaTime / showSpeedInSeconds)
        {
            // opacity animate
            currentAlpha = Mathf.Lerp(0f, 1f, t);
            _canvasGroup.alpha = currentAlpha;
            // position animate
            currentXPos = Mathf.Lerp(animStartXPos, animEndXPos, t);
            transform.localPosition = new Vector2(currentXPos, transform.localPosition.y);
            yield return null;
        }
        // ensure we hit our final value
        _canvasGroup.alpha = 1;
        transform.localPosition = new Vector2(animEndXPos, transform.localPosition.y);
        // only allow button to be clickable if animation has finished
        _buttonImage.raycastTarget = true;
    }

    IEnumerator AnimateHide(float startDelay, float hideSpeedInSeconds)
    {
        float currentAlpha = 1;
        _canvasGroup.alpha = 1;
        _buttonImage.raycastTarget = false;

        yield return new WaitForSeconds(startDelay);

        for (float t = 1f; t > 0; t -= Time.deltaTime / hideSpeedInSeconds)
        {
            currentAlpha = Mathf.Lerp(0, 1f, t);
            _canvasGroup.alpha = currentAlpha;
            yield return null;
        }
        // ensure we hit our final value
        _canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }

    public void Display()
    {
        // ensure our gameObject is active
        gameObject.SetActive(true);

        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(AnimateShow(_startDelayInSeconds, _showSpeedInSeconds));
    }

    public void Hide()
    {
        // if we're currently animating, kill it early to start our new one
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
        _animationCoroutine = StartCoroutine(AnimateHide(_startDelayInSeconds, _hideSpeedInSeconds));
    }
}

