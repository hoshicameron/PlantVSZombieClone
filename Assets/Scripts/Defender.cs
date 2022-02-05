using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : MonoBehaviour
{
    [HideInInspector] public DefenderSpot spot;

    private void OnDisable()
    {
        // Reset related spot after defender destroyed
        if (spot != null)
        {
            spot.ResetSpot();
        }
    }
}
