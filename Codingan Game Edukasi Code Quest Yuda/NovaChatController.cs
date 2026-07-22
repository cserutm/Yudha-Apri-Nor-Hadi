using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaChatController : MonoBehaviour
{
    public GameObject panelChat;

    void Start()
    {
        panelChat.SetActive(false); // awalnya disembunyikan
    }

    public void BukaChat()
    {
        panelChat.SetActive(true);
    }

    public void TutupChat()
    {
        panelChat.SetActive(false);
    }
}