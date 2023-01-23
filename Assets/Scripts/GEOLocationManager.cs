using UnityEngine;
using System.Collections;
using TMPro;

/// <summary>
/// This class is responsible for getting user's latitude and longitude
/// </summary>
public class GEOLocationManager : MonoBehaviour
{
    public static Vector2? homeLatLong;
    IEnumerator Start()
    {
        // Start the location service.
        while (Input.location.status != LocationServiceStatus.Running)
        {
            Input.location.Start();
            yield return new WaitForEndOfFrame();
        }

        // Wait until the location service initializes
        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 10 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //consoleText.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            homeLatLong = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            string location = Input.location.lastData.latitude + " " + Input.location.lastData.longitude;
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + location);
        }

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }
}