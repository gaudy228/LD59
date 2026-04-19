using System;
using UnityEngine;

public class Localization : MonoBehaviour
{
    public static Action OnSwitchLocalization;

    public static bool IsEng = true;

    public static bool IsRu = false;

    public void EngLocalization()
    {
        IsEng = true;
        IsRu = false;
        OnSwitchLocalization?.Invoke();
    }

    public void RuLocalization()
    {
        IsRu = true;
        IsEng = false;
        OnSwitchLocalization?.Invoke();
    }
}
