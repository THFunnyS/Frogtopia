using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSign : MonoBehaviour
{
    void Start()
    {
        // ѕо умолчанию скрываем восклицательный знак при старте
        gameObject.SetActive(false);
    }

    // ѕоказать восклицательный знак
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // —крыть восклицательный знак
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
