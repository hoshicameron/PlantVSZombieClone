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
        // Set text
        costText.SetText(defenderCost.ToString());
    }

    public void SetDefenderPrefab()
    {
        // Invoke defender selection event
        EventHandler.CallDefenderSelectionEvent(selectedDefenderPrefab,defenderCost);
    }

}
