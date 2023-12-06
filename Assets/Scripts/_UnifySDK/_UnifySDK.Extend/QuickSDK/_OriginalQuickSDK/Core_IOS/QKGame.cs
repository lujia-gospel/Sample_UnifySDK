#if UNITY_IOS
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using quickgame;
public class QKGame {
	 

     [DllImport("__Internal")]
     private static extern void QKSetListener(string gameObjectName);
     //setListener :   OC callback to Unity
     public static void setListener(QuickListener listener)
     {
        string gameObjectName = listener.gameObject.name;
        QKSetListener(gameObjectName);
     }

    [DllImport("__Internal")]  
    private static extern void QKInit();
	//init
	public static void onInit()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)   
        {  
			QKInit();
        }  
	}

	 [DllImport("__Internal")]  
     private static extern void QKLogin();
	 //Login
     public static void onLogin ()
     {  
          
        if (Application.platform != RuntimePlatform.OSXEditor)   
        {  
			QKLogin(); 
        }  
     }
     
     [DllImport("__Internal")]
     private static extern void QKLoginAsGuest();
     //guestLogin(fast login)
     public static void onGuestLogin ()
     {
          
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            QKLoginAsGuest();
        }
     }
       
     [DllImport("__Internal")]  
     private static extern void QKCenter();  
     //QKCenter
     public static void onUserCenter()
     {  
        if (Application.platform != RuntimePlatform.OSXEditor)   
        {  
			QKCenter(); 
        }  
     }

	[DllImport("__Internal")]  
	private static extern void QKLogout();  
	//Logout
	public static void onLogout()
	{  
		if (Application.platform != RuntimePlatform.OSXEditor)   
		{  
			QKLogout(); 
		}  
	}

	 [DllImport("__Internal")]  
     private static extern void QKUserID();
	 //IsLogined
     public static void onUserID()
     {  
        if (Application.platform != RuntimePlatform.OSXEditor)   
        {  
			QKUserID();
        }  
     }
     
     [DllImport("__Internal")]
     private static extern void QKUserToken();
     //IsLogined
     public static void onUserToken()
     {
         if (Application.platform != RuntimePlatform.OSXEditor)
         {
             QKUserToken();
         }
     }

	[DllImport("__Internal")]  
	private static extern void QKUserName();
	//UserName
	public static void onUserName()
	{  
		if (Application.platform != RuntimePlatform.OSXEditor)   
		{  
			QKUserName();
		}  
	}

	[DllImport("__Internal")]  
	private static extern void QKCongzi(string productId,string productName,float amount,string orderNo,string callBackUrl,string extrasParams);
	//Pay RMB
	public static void onPayy(string productId,string productName,float amount,string orderNo,string callBackUrl,string extrasParams)
	{  
		if (Application.platform != RuntimePlatform.OSXEditor)   
		{  
			QKCongzi(productId,productName,amount,orderNo,callBackUrl,extrasParams);
		}  
	}
    
    [DllImport("__Internal")]
    private static extern void QKUpdateRole(string roleId,string role_name,string server_Id,string sv_name,string role_level,string vipLevel,string role_power);
    //Update RoleInfo
    public static void onUpdateRole(string roleId,string role_name,string server_Id,string sv_name,string role_level,string vipLevel,string role_power)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            QKUpdateRole(roleId,role_name,server_Id,sv_name,role_level,vipLevel,role_power);
        }
    }

    [DllImport("__Internal")]
    private static extern void QKRestoreNonConsumptionProducts();
    //Restoring purchased non-consumable or auto-subscription items
    public static void onRestoreNonConsumptionProducts()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            QKRestoreNonConsumptionProducts();
        }
    }
}
#endif
