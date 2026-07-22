using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetunjukManager : MonoBehaviour
{
    [Header("UI")]
    public Image iconImage;
    public TMP_Text judulText;
    public TMP_Text isiText;

    [Header("Isi Petunjuk")]
    public Sprite[] gambarPetunjuk;
    public string[] judulPetunjuk;

    [TextArea(5,10)]
    public string[] isiPetunjuk;

    int index = 0;

    void Start()
    {
        TampilPetunjuk();
    }

    void TampilPetunjuk()
    {
        iconImage.sprite = gambarPetunjuk[index];
        judulText.text = judulPetunjuk[index];
        isiText.text = isiPetunjuk[index];
    }

    public void NextPetunjuk()
    {
        if(index < isiPetunjuk.Length - 1)
        {
            index++;
            TampilPetunjuk();
        }
    }
    public void PrevPetunjuk()
    {
        if(index > 0)
        {
            index--;
            TampilPetunjuk();
        }
    }
    public void TutupPanel()
    {
        gameObject.SetActive(false);
    }
}