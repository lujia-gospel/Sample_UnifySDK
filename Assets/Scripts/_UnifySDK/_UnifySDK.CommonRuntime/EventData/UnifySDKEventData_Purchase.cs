using System;

namespace UnifySDK.Event
{
      public struct PurchaseOrderInfo {
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
      }
      
      public struct PurchaseRoleInfo
      {
          public int SubmitType;
          
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
      
      public struct PurchaseSuccessData
      {
          public string orderId;
          public string cpOrderId;
          public string extraParam;
      }
      
      public struct PurchaseFailedData
      {
          public string orderId;
          public string cpOrderId;
          public string extraParam;
      }
      
      public struct PurchaseCancelData
      {
          public string orderId;
          public string cpOrderId;
          public string extraParam;
      }
}