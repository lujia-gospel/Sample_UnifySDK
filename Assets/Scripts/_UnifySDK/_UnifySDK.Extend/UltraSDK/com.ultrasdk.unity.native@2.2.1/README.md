UltraSDK-Native

为了方便您更好享受 UltraSDK 服务 ，我们发布了可在 Unity3D 直接接入的插件版本。<br/>
本插件还在持续完善中，欢迎您积极反馈使用意见、想法和需求，帮助我们更好的为您提供服务。

####  最新版本：UltraSDK-UnityV2.2.1 (发布日期：2023-10-31）

####  Unity插件使用UPM模式开发,游戏开发者在`Packages/manifest.json`配置如下内容即可自动下载插件内容


#### 1、在`dependencies`之前添加如下代码:


```cs

"scopedRegistries": [
{
  "name": "ultra",
  "url": "https://source.sdksrv.com/verdaccio/",
  "scopes": [
    "com.ultrasdk.unity.native"
  ]
  }
],

```

#### 2、在`dependencies`中添加Ultra引用版本

```cs

"com.ultrasdk.unity.native": "2.1.8"

```