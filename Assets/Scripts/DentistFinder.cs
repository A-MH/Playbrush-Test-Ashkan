using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class DentistFinder : MonoBehaviour
{
    [SerializeField]
    private DentistDetailsPanelManager ddpm;
    [SerializeField]
    private NearbyDentistsMapManager map;
    private Vector2 homeLatLong;
    public string apiKey;

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetClosestDentists());
    }

    IEnumerator GetClosestDentists()
    {
        while (GEOLocationManager.homeLatLong == null)
            yield return new WaitForEndOfFrame();
        homeLatLong = (Vector2)GEOLocationManager.homeLatLong;
        using UnityWebRequest webRequest = UnityWebRequest.Get($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={homeLatLong[0]},{homeLatLong[1]}&type=dentist&rankby=distance&key={apiKey}");
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();
        //yield return map.ShowMap(homeLatLong, apiKey);
        var nearbyDentists = JsonUtility.FromJson<NearbyDentists>(webRequest.downloadHandler.text);
        yield return map.ShowMap(apiKey, homeLatLong, nearbyDentists);
        ddpm.PopulateDentistDetailsPanel(nearbyDentists);
        //Debug.Log("Received: " + webRequest.downloadHandler.text);
        //File.WriteAllText("web result.json", webRequest.downloadHandler.text);
    }
}