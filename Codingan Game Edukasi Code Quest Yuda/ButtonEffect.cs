using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    private Vector3 originalScale;
    private Vector3 targetScale;

    [Header("Scale Effect")]
    public float hoverScale = 1.1f;
    public float clickScale = 0.9f;

    [Header("Animation")]
    public float speed = 10f;

    [Header("Sound")]
    public AudioSource audioSource;

    public AudioClip hoverSound;
    public AudioClip clickSound;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.deltaTime * speed
        );
    }

    // Cursor masuk
    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;

        if (hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }

    // Cursor keluar
    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    // Tombol ditekan
    public void OnPointerDown(PointerEventData eventData)
    {
        targetScale = originalScale * clickScale;

        if (clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }

    // Tombol dilepas
    public void OnPointerUp(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }
}