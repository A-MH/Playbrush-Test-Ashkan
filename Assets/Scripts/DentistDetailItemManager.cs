using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// responsible for populating a single dentist detail in the list
/// </summary>
public class DentistDetailItemManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI detailsText;
    [SerializeField]
    private TextMeshProUGUI mapIDText;

    public void PopulateDentistDetailBox(string practiceName, string Adress, char mapIDText)
    {
        detailsText.text = practiceName + "\n" + Adress;
        this.mapIDText.text = mapIDText.ToString();
    }
}
