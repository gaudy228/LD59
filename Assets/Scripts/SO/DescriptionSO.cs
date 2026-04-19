using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Description")]
public class DescriptionSO : ScriptableObject
{
    [TextArea(3, 5)]
    [field: SerializeField] public string RuDescription;

    [TextArea(3, 5)]
    [field: SerializeField] public string EngDescription;

    public string Description()
    {
        if (Localization.IsEng)
        {
            return EngDescription;
        }
        else if(Localization.IsRu)
        {
            return RuDescription;
        }
        return null;
    }
}
