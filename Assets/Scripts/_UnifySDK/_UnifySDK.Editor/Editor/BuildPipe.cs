// //using com.ultrasdk.unity.native.Editor;
// using Horizon.Game.LogicLayer;
// //using MyUltraSDK;
// using UnityEditor;
// using UnityEditor.Build;
// using UnityEditor.Callbacks;
// using UnityEngine;
//
// public class BuildPipe : UnityEditor.Editor, IPreprocessBuild
// {
//     public void OnPreprocessBuild(BuildTarget target, string path)
//     {
//         // 检查文件和配置
//         if (target == BuildTarget.Android && (GameProperties.Get().UseSDKTypeEnum == SDKType.IsUseUltraSdk || GameProperties.Get().UseSDKTypeEnum == SDKType.IsUseUltraSdk2))
//         {
//             // //开启调试
//             // UltraSDKAndroidEditor.SetVerbose(true);
//             // //编译Android清单等文件
//             // UltraSDKAndroidEditor.AndroidBuild();
//             var myUltraProductId = "10191";
//             var myUltraProductKey = "4sauc16ngcq6qakloke6";
//             //处理ultrasdkcfg.xml文件
//             if (GameProperties.Get().UseSDKTypeEnum == SDKType.IsUseUltraSdk)
//             {
//                 myUltraProductId = "10191";
//                 myUltraProductKey = "4sauc16ngcq6qakloke6";
//                 Debug.Log("OnPreprocessBuild1:myUltraProductId" +myUltraProductId + "myUltraProductKey"+myUltraProductKey);
//             }
//
//             if (GameProperties.Get().UseSDKTypeEnum == SDKType.IsUseUltraSdk2)
//             {
//                 myUltraProductId = "10196";
//                 myUltraProductKey = "lhu7dn8uip2ps32t91xn"; 
//                 Debug.Log("OnPreprocessBuild2:myUltraProductId" +myUltraProductId + "myUltraProductKey"+myUltraProductKey);
//             }
//             var productID = myUltraProductId;
//             var productKey = myUltraProductKey;
//             UltraSDKAndroidEditor.UltraSdkConfigXml(productID, productKey);
//         }
//     }
//
//     [PostProcessBuildAttribute(1)]
//     public static void OnPostprocessBuild(BuildTarget target, string path)
//     {
//         if (target == BuildTarget.iOS && (GameProperties.Get().UseSDKTypeEnum == SDKType.IsUseUltraSdk ||
//                                           GameProperties.Get().UseSDKTypeEnum == SDKType.IsUseUltraSdk2))
//         {
//             //开启调试
//             UltraSDKiOSEditor.SetVerbose(true);
//             //融合SDK
//             UltraSDKiOSEditor.iOSBuild(path);
//         }
//     }
//
//     public int callbackOrder { get; }
// }