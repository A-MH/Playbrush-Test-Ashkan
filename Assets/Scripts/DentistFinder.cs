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
    // dummy lat long location provided for the editor
    private Vector2 homeLatLong= new (51.596404f, -0.172103f);
    private string apiKey = "AIzaSyDhHC3VZ9iTNIJ4hPo8h4bSsNxD3eDCTSQ";

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetClosestDentists());
    }

    IEnumerator GetClosestDentists()
    {
        // if player is not running in the editor, then get geolocation from device, else use dummy data
#if !UNITY_EDITOR
        while (GEOLocationManager.homeLatLong == null)
            yield return new WaitForEndOfFrame();
        homeLatLong = (Vector2)GEOLocationManager.homeLatLong;
#endif
        using UnityWebRequest webRequest = UnityWebRequest.Get($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={homeLatLong[0]},{homeLatLong[1]}&type=dentist&rankby=distance&key={apiKey}");
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();
        //yield return map.ShowMap(homeLatLong, apiKey);
        var nearbyDentists = JsonUtility.FromJson<NearbyDentists>(webRequest.downloadHandler.text);
        yield return map.ShowMap(apiKey, homeLatLong, nearbyDentists);
        ddpm.PopulateDentistDetailsPanel(nearbyDentists);
        //Debug.Log("Received: " + webRequest.downloadHandler.text);
        File.WriteAllText("closest dentists.json", webRequest.downloadHandler.text);
    }
}