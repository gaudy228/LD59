using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Description")]
public class DescriptionSO : ScriptableObject
{
    [TextArea(3, 5)]
    [field: SerializeField] public string Description;
}
