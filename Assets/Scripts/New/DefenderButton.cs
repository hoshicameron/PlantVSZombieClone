using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenderButton : MonoBehaviour
{
    [SerializeField] private GameObject selectedDefenderPrefab;
    [SerializeField] private int defenderCost = 0;
    [SerializeField] private TextMeshProUGUI costText;

    private void Start()
    {
        costText.SetText(defenderCost.ToString());
    }

    public void SetDefenderPrefab()
    {
        EventHandler.CallDefenderSelectionEvent(selectedDefenderPrefab,defenderCost);
    }

}
