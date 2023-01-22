using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DentistDetailsPanelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dentistDetailsBoxPrefab;
    [SerializeField]
    private GameObject dentistDetailsContent;

    /// <summary>
    /// Create a dentist detail box using the data passed in for each nearby dentist
    /// </summary>
    /// <param name="nearbyDentists"></param>
    public void PopulateDentistDetailsPanel(NearbyDentists nearbyDentists)
    {
        for (int i = 0; i < nearbyDentists.results.Count; i++)
        {
            var nearbyDentist = nearbyDentists.results[i];
            var dentistDetailsBox = Instantiate(dentistDetailsBoxPrefab, dentistDetailsContent.transform);
            dentistDetailsBox.GetComponent<DentistDetailItemManager>().PopulateDentistDetailBox(nearbyDentist.name, nearbyDentist.vicinity, nearbyDentist.mapID);
        }
        dentistDetailsContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, nearbyDentists.results.Count * dentistDetailsContent.GetComponent<GridLayoutGroup>().cellSize.y);
    }
}
