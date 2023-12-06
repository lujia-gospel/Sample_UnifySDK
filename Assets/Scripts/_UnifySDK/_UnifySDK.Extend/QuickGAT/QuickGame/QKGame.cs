using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class QKGameGAT {
 #if UNITY_IPHONE 
	 [DllImport("__Internal")]  
     private static extern void luLuInit();
	//init
	public static void txQKInit()
	{
		if (Application.platform != RuntimePlatform.OSXEditor)   
        {  
			luLuInit();
        }  
	}
	 [DllImport("__Internal")]  
     private static extern void luLuLogin(bool isShowMenu);
	 //Login
     public static void txQKLogin (bool isShowMenu)
     {  
        if (Application.platform != RuntimePlatform.OSXEditor)
        {  
			luLuLogin(isShowMenu); 
        }  
     }  
     [DllImport("__Internal")]
     private static extern void luLuFastStartGame();
     //Login
     public static void txQKFastStartGame()
     {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luLuFastStartGame();
        }
     }
     [DllImport("__Internal")]  
     private static extern void luLuUserCenter();  
     //luLuUserCenter
     public static void txQKCenter()
     {  
        if (Application.platform != RuntimePlatform.OSXEditor)   
        {  
			luLuUserCenter(); 
        }  
     }
	[DllImport("__Internal")]
	private static extern void luLuLogout();  
	//Logout
	public static void txQKLogout()
	{  
		if (Application.platform != RuntimePlatform.OSXEditor)   
		{  
			luLuLogout(); 
		}  
	}
     [DllImport("__Internal")]
    private static extern void luLuBindAccount();
    //bindAccount
    public static void txQKBindAccount()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luLuBindAccount();
        }
    }
    [DllImport("__Internal")]
    private static extern void luLuBindAccountWithType(int type);
    //bindAccountType
    public static void txQKBindAccountWithType(int type)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luLuBindAccountWithType(type);
        }
    }
 
    [DllImport("__Internal")]
    private static extern void luLuAccountDeletion();
    //accountDeletion
    public static void txQKAccountDeletion()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luLuAccountDeletion();
        }
    }
    [DllImport("__Internal")]
    private static extern void luLuShowMenu();
    //luLuShowMenu
    public static void txQKShowMenu()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luLuShowMenu();
        }
    }
    [DllImport("__Internal")]
    private static extern void luLuDismissMenu();
    //luLuDismissMenu
    public static void txQKDismissMenu()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luLuDismissMenu();
        }
    }
    [DllImport("__Internal")]
     private static extern void luluSetRoleInfo(string serverName,string serverId,string roleId,string roleName,string roleLevel,string roleVipLevel);
    //setRoleInfo,参数 游戏服名 游戏服id 角色名 角色id 角色等级 角色vip等级
    public static void txQKSetRoleInfo(string serverName,string serverId,string roleId,string roleName,string roleLevel,string roleVipLevel)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luluSetRoleInfo(serverName,serverId,roleId,roleName,roleLevel,roleLevel);
        }
    }
    [DllImport("__Internal")]
    private static extern void luluFbSharePhoto(string imageName,string caption,string tag);
    /**
     * FB原生图片分享
     * imageName  分享的图片名
     * caption 分享的照片创建标题,如果传nil则没有标题，FB平台不允许预填写分享内容这里填写了也不会在FB里展示
     * tag 话题标签,可为空，会显示在分享对话框中，因此用户在发布之前可决定是否将它删除
     * completeResults 分享回调
         分享成功 status=0，error = nil
         分享失败 status=1，error != nil
         取消分享 status=2，error = nil
     * tip:
     1.照片大小必须小于 12MB
     2.用户需要安装版本 7.0 或以上的原生 iOS 版 Facebook 应用
     */
    
    /// imageName = "1080-1920.jpg"
    public static void txQKFbSharePhoto(string imageName,string caption,string tag)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luluFbSharePhoto(imageName,caption,tag);
        }
    }
    
    [DllImport("__Internal")]
    private static extern void luluFbShareUrl(string url,string quote,string tag);

    public static void txQKFbShareUrl(string url,string quote,string tag)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luluFbShareUrl(url,quote,tag);
        }
    }
	[DllImport("__Internal")]  
	private static extern void luLuCongzi(string productId,string productName,string amount,string orderNo,string callBackUrl,string extrasParams);
	//Pay RMB,参数 苹果商品ID 商品名称 价格 游戏订单号 游戏收单服务器地址 透传参数
	public static void txQKCongzi(string productId,string productName,string amount,string orderNo,string callBackUrl,string extrasParams)
	{  
		if (Application.platform != RuntimePlatform.OSXEditor)   
		{  
			luLuCongzi(productId,productName,amount,orderNo,callBackUrl,extrasParams);
		}  
	}
    [DllImport("__Internal")]
    private static extern void luluRestoreNonConsumptionProducts();
    //lulurestoreNonConsumptionProducts
    public static void txQKRestoreNonConsumptionProducts()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luluRestoreNonConsumptionProducts();
        }
    }
    [DllImport("__Internal")]
    private static extern void luluFindProductInfo(string productIds);

    public static void txQKFindProductInfo(string productIds)
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            luluFindProductInfo(productIds);
        }
    }
    
    [DllImport("__Internal")]
    private static extern string luLuGetUserBindInfo();
    public static string txGetUserBindInfo()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return luLuGetUserBindInfo();
        }

        return "";
    } 
    
    [DllImport("__Internal")]
    private static extern bool luluIsUserGuest();
    public static bool txIsUserGuest()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return luluIsUserGuest();
        }

        return false;
    } 
    
    
    [DllImport("__Internal")]
    private static extern string luLuGetDeviceID();
    public static string txGetDeviceID()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return luLuGetDeviceID();
        }

        return "";
    } 
    
    [DllImport("__Internal")]
    private static extern string luLuGetchannelCode();
    public static string txGetchannelCode()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return luLuGetchannelCode();
        }

        return "";
    } 
    
    [DllImport("__Internal")]
    private static extern string luLuGetgetNationCode();
    public static string txGetgetNationCode()
    {
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return luLuGetgetNationCode();
        }

        return "";
    }  
    
#endif
}
