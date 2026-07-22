using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundUI : MonoBehaviour
{
    [Header("Image ON")]
    public Sprite imageOn;

    [Header("Image OFF")]
    public Sprite imageOff;

    [Header("Button Image")]
    public Image buttonImage;

    private bool isMute = false;

    public void ToggleSound()
    {
        isMute = !isMute;

        // ON OFF suara
        MusikControl.Instance.audioMusik.mute = isMute;

        // Ganti gambar button
        if (isMute)
        {
            buttonImage.sprite = imageOff;
        }
        else
        {
            buttonImage.sprite = imageOn;
        }
    }
}