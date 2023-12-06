using System;

namespace UnifySDK.Event.Purchase
{
    [UnifySDKEventData]
    public struct PurchaseOrderInfoData
    {
        public string goodsID;
        public string goodsName;
        public string goodsDesc;
        public string quantifier; //商品量词
        public string cpOrderID;
        public string callbackUrl;
        public string extrasParams;
        public double price;
        public double amount;
        public int count;
        public string productId;
        public string roleName;
        public string roleLv;
        public string serverName;
        public string currency;
    }

    [UnifySDKEventData]
    public struct PurchaseRoleInfoData
    {
        public enum SubmitType
        {
            StartGame,
            CreateRole,
            UpdateRole,
            Purchase,
        }

        public SubmitType submitType;

        public string channelUserId;

        /// <summary>
        /// 一级货币[充值获得] 
        /// </summary>
        public string gold1;

        /// <summary>
        /// 二级货币[游戏内产出]  g
        /// </summary>
        public string gold2;

        public string serverName;
        public string serverID;
        public string gameRoleName;
        public string gameRoleID;
        public string gameRoleBalance;
        public string vipLevel;
        public string gameRoleLevel;
        public string partyName;
        public string roleCreateTime;
        public string fightPower;
        public string profession;
        public string gameRoleGender;
        public string gameRolePower;
        public string partyId;
        public String professionId;
        public String partyRoleId;
        public String partyRoleName;
        public String friendlist;
    }

    [UnifySDKEventData]
    public struct PurchaseSuccessData
    {
        public string orderId;
        public string cpOrderId;
        public string extraParam;
    }

    [UnifySDKEventData]
    public struct PurchaseFailedData
    {
        public string orderId;
        public string cpOrderId;
        public string extraParam;
    }

    [UnifySDKEventData]
    public struct PurchaseCancelData
    {
        public string orderId;
        public string cpOrderId;
        public string extraParam;
    }

    [UnifySDKEventData]
    public struct ConsumeSucceedData
    {
        public string purchaseToken;
    }

    [UnifySDKEventData]
    public struct RePaySuccessData
    {
        public string orderId;
        public string cpOrderId;
        public string goodsId;
        public string purchaseTokenl;
        public string packageName;
    }

    [UnifySDKEventData]
    public struct RestoreSuccessData
    {
        public string extraParam;
    }
}