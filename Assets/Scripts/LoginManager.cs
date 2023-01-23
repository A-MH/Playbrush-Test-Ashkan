using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// user login manager
/// </summary>
public class LoginManager : MonoBehaviour
{
    [SerializeField]
    private LoginPanelManager loginPanelManager;
    [SerializeField]
    private MainUIManager mainUiManager;

    public void OnLoginClicked()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        //string requestBody = "{\"email\":\"ashkan_m_h@hotmail.co.uk\",\"password\":\"hoseynSherafat1\"}";
        string requestBody = "{\"email\":\"" + loginPanelManager.userName.text + "\",\"password\":\"" + loginPanelManager.password.text + "\"}";
        print(requestBody);
        using UnityWebRequest www  = UnityWebRequest.Put($"https://smart-api.playbrush.com/api/v1/user/login/json", requestBody);
        www.method = "POST";
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            loginPanelManager.errorMessage.gameObject.SetActive(true);
            loginPanelManager.errorMessage.text = www.error;
        }
        else
        {
            Debug.Log("login successful");
            loginPanelManager.transform.gameObject.SetActive(false);
            UserInfo userinfo = JsonUtility.FromJson<UserInfo>(www.downloadHandler.text);
            mainUiManager.gameObject.SetActive(true);
            mainUiManager.PopulateUserInfoPanel(userinfo);
            File.WriteAllText("user info.json", www.downloadHandler.text);
            loginPanelManager.gameObject.SetActive(false);
        }
    }
}
