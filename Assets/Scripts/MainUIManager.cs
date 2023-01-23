using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the main UI
/// </summary>
public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loginCanvasGO;
    [SerializeField]
    private TextMeshProUGUI email;
    [SerializeField]
    new private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI registrationDate;
    [SerializeField]
    private TextMeshProUGUI AccessLevel;
    [SerializeField]
    private TextMeshProUGUI oAuthToken;

    public void PopulateUserInfoPanel(UserInfo userinfo)
    {
        email.text = $"Email: {userinfo.email}";
        name.text = $"Name: {userinfo.first_name} {userinfo.family_name}";
        registrationDate.text = $"Registration Date: {userinfo.registered_at.Remove(userinfo.registered_at.LastIndexOf("T"))}";
        AccessLevel.text = $"Access Level: {userinfo.access_level}";
        oAuthToken.text = $"OAuth token: {userinfo.token}";
    }
}
