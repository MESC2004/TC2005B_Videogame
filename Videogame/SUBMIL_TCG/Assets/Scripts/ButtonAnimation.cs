using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    private Vector3 originalPosition;

    public float moveAmount = 10f;
    private bool isPointerInside = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerInside = true;
        rectTransform.anchoredPosition = new Vector3(originalPosition.x, originalPosition.y + moveAmount, originalPosition.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Start a coroutine to delay the exit check
        StartCoroutine(DelayedCheck(eventData));
    }

    private IEnumerator DelayedCheck(PointerEventData eventData)
    {
        // Wait for the end of the frame to ensure the pointer has actually exited
        yield return new WaitForEndOfFrame();

        // Check if the pointer is still inside the button area
        if (!RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position, eventData.pressEventCamera))
        {
            isPointerInside = false;
            rectTransform.anchoredPosition = originalPosition;
        }
    }
}
