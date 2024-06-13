using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    public float moveAmount = 10f;
    private bool isPointerInside = false;
    public AudioClip buttonSound;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;

        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySound(buttonSound);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        rectTransform.anchoredPosition = new Vector3(originalPosition.x, originalPosition.y + moveAmount, originalPosition.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(DelayedCheck(eventData));
    }

    private IEnumerator DelayedCheck(PointerEventData eventData)
    {
        yield return new WaitForEndOfFrame();

        if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position, eventData.pressEventCamera))
        {
            isPointerInside = false;
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
