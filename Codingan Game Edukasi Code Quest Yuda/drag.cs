using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Puzzle")]
    public int idPuzzle;

    [Header("Canvas")]
    public Canvas canvas;

    [Header("Semua Slot")]
    public Transform[] semuaSlot;

    [HideInInspector]
    public Transform currentSlot;

    // LIST SEMUA PUZZLE (lebih ringan dari FindObjectsOfType)
    public static List<drag> semuaPuzzle = new List<drag>();

    private RectTransform rectTransform;

    private Vector2 posAwal;
    private Vector3 scaleAwal;

    void Awake()
    {
        semuaPuzzle.Add(this);

        rectTransform = GetComponent<RectTransform>();
    }

    void OnDestroy()
    {
        semuaPuzzle.Remove(this);
    }

    void Start()
    {
        posAwal = rectTransform.anchoredPosition;
        scaleAwal = transform.localScale;

        currentSlot = null;
    }

    // =========================
    // MULAI DRAG
    // =========================
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
    }

    // =========================
    // SAAT DRAG
    // =========================
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // =========================
    // SELESAI DRAG
    // =========================
    public void OnEndDrag(PointerEventData eventData)
    {
        Transform slotTerdekat = null;
        float jarakTerdekat = Mathf.Infinity;

        // CARI SLOT TERDEKAT
        foreach (Transform slot in semuaSlot)
        {
            float jarak = Vector2.Distance(
                rectTransform.position,
                slot.position
            );

            if (jarak < jarakTerdekat)
            {
                jarakTerdekat = jarak;
                slotTerdekat = slot;
            }
        }

        // JIKA DEKAT SLOT
        if (slotTerdekat != null && jarakTerdekat < 100f)
        {
            bool slotTerpakai = false;

            // CEK SLOT SUDAH DIPAKAI?
            foreach (drag item in semuaPuzzle)
            {
                if (item != this && item.currentSlot == slotTerdekat)
                {
                    slotTerpakai = true;
                    break;
                }
            }

            // JIKA SLOT KOSONG
            if (!slotTerpakai)
            {
                rectTransform.position = slotTerdekat.position;
                currentSlot = slotTerdekat;
            }
            else
            {
                KembaliKeAwal();
            }
        }
        else
        {
            KembaliKeAwal();
        }

        transform.localScale = scaleAwal;
        transform.SetAsLastSibling();
    }

    // =========================
    // RESET KE POSISI AWAL
    // =========================
    void KembaliKeAwal()
    {
        rectTransform.anchoredPosition = posAwal;
        currentSlot = null;
    }

    // =========================
    // RESET PUZZLE
    // =========================
    public void ResetPuzzle()
    {
        rectTransform.anchoredPosition = posAwal;
        transform.localScale = scaleAwal;
        currentSlot = null;
    }
}