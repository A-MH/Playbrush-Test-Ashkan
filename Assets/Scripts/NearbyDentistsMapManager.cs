using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class NearbyDentistsMapManager : MonoBehaviour
{
    public Vector2 latLong;

    public IEnumerator ShowMap(string apiKey, Vector2 homeLatLong, NearbyDentists nearbyDentists)
    {
        // alphabet string is used for giving each nearby dentist a letter on the map
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string poiLocations = "";
        for (int i = 0; i < nearbyDentists.results.Count; i++)
        {
            string lat = nearbyDentists.results[i].geometry.location.lat;
            string lng = nearbyDentists.results[i].geometry.location.lng;
            poiLocations += $"markers=label:{alphabet[i]}|{lat},{lng}";
            if (i < nearbyDentists.results.Count - 1)
                poiLocations += "&";

            nearbyDentists.results[i].mapID = (char) alphabet[i];
        }
        string url = $"https://maps.googleapis.com/maps/api/staticmap?key={apiKey}&center={homeLatLong[0]},{homeLatLong[1]}&{poiLocations}&size=400x400";
        using UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
        // Request and wait for the desired page.
        yield return webRequest.SendWebRequest();
        //Debug.Log("Received: " + webRequest.downloadHandler.text);
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("web request to map static failed: " + webRequest.error);
        }
        else
        {
            gameObject.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
        }
    }
}
