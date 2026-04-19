using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tutorPanels = new List<GameObject>();
    [SerializeField] private GameObject _map;

    private GameObject _curTutorPanel;

    private int _curIndex;

    private void Start()
    {
        Next();
    }

    public void Next()
    {
        if(_curIndex >= _tutorPanels.Count)
        {
            Skip();
            return;
        }

        if(_curTutorPanel != null)
        {
            _curTutorPanel.SetActive(false);
        }
        _tutorPanels[_curIndex].SetActive(true);
        _curTutorPanel = _tutorPanels[_curIndex];

        _curIndex++;
    }

    public void Skip()
    {
        //_map.SetActive(true);
        gameObject.SetActive(false);
    }
}
