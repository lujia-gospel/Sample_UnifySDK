#if UNITY_IOS
using UnityEngine;
using System.Collections;
using quickgame;

public class TestUnity : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        //Kinds of Buttons
        if (GUI.Button(getRectByNo(0), "Init"))
        {
            QKGame.onInit();
        }

        //Login
        if (GUI.Button(getRectByNo(1), "Login"))
        {
            QKGame.onLogin();
        }

        //guestLogin
        if (GUI.Button(getRectByNo(2), "guestLogin"))
        {
            QKGame.onGuestLogin();
        }

        //Center
        if (GUI.Button(getRectByNo(3), "Center"))
        {
            QKGame.onUserCenter();
        }

        //LogOut
        if (GUI.Button(getRectByNo(4), "Logout"))
        {
            QKGame.onLogout();
        }

        //UserID
        if (GUI.Button(getRectByNo(5), "UserID"))
        {
            QKGame.onUserID();
        }

        //UserName
        if (GUI.Button(getRectByNo(6), "UserName"))
        {
            QKGame.onUserName();
        }

        //Pay
        if (GUI.Button(getRectByNo(7), "Pay 1 RMB"))
        {
            QKGame.onPayy("com.yuanbao.1", "元宝", 1, "123", "url", "extrasParams");
        }

        //UpdateRole
        if (GUI.Button(getRectByNo(8), "UpdateRole"))
        {
            // onUpdateRole("roleid","roleName","sv_name","roleLevel","vipLevel");
        }

        //UserToken
        if (GUI.Button(getRectByNo(9), "UserToken"))
        {
            QKGame.onUserToken();
        }
    }

    Rect getRectByNo(int no)
    {
        return new Rect((Screen.width / 2 - ((2 - no % 3) * 180) + 70), 70 * (no / 3) + 200, 160, 60);
    }
}
#endif