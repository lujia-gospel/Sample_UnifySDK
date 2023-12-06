#!usr/bin/env python
import os
import shutil
import sys
import json

UnityPath = ''
UnityDataPath = ''
ExternalRootPath = ''

root_path = os.path.abspath(os.path.dirname(__file__))
UnityPath = root_path.replace("\\Tools\\UnifySDKTool", "")
UnityPath = UnityPath.replace("/Tools/UnifySDKTool", "")
UnityDataPath = os.path.join(UnityPath, "Assets")
ExternalRootPath = os.path.join(UnityPath, "Tools/SdkFrame")
print("root_path:"+root_path)
print("UnityPath:"+UnityPath)
print("UnityDataPath:"+UnityDataPath)
print("ExternalRootPath:"+ExternalRootPath)

def write_file_string(path, data):
    p = os.path.dirname(path)
    if not os.path.exists(p):
        os.makedirs(p,True)
    with open(path, 'w', encoding='utf-8') as file:
        file.write(data)

def read_file_string(path):
    if os.path.exists(path):
        with open(path, 'r', encoding='utf-8') as file:
            return file.read()
    return ''

def plugins_move_to_project(file_name, path):
    source_path = f"{UnityDataPath}/{path}/Plugins" 
    target_path = f"{UnityDataPath}/Plugins"  
    source_dire_info = os.path.abspath(source_path)
    dest_dire = os.path.abspath(target_path)
    #print(f"  CopyDireToDire  sourcePath: {source_path} targetPath: {target_path}  ")
    print(f"  CopyDireToDire  sourceDireInfo: {source_dire_info} destDire: {dest_dire}  ")
    merge_folders(source_dire_info, dest_dire)

def delete_plugins_folder(target_path):
    # 使用shutil.rmtree()
    if os.path.isdir(target_path):
        shutil.rmtree(target_path)

def merge_folders(source_folder, destination_folder):
    for root, dirs, files in os.walk(source_folder):
        # 构造目标文件夹路径
        destination_root = os.path.join(destination_folder, os.path.relpath(root, source_folder))

        # 如果目标文件夹不存在，则创建
        if not os.path.exists(destination_root):
            os.makedirs(destination_root)

        # 移动文件
        for file in files:
            source_path = os.path.join(root, file)
            destination_path = os.path.join(destination_root, file)
            shutil.move(source_path, destination_path)
    print("文件夹内容已成功移动到目标路径，并合并同名文件夹。")
    delete_plugins_folder(source_folder)

def delete_dire_fun(delete_dire):
    if os.path.isfile(delete_dire):
        os.remove(delete_dire)
        delete_dire_meta = delete_dire + ".meta"
        if os.path.isfile(delete_dire_meta):
            os.remove(delete_dire_meta)
    if os.path.isdir(delete_dire):
        shutil.rmtree(delete_dire)
        delete_dire_meta = delete_dire + ".meta"
        if os.path.isfile(delete_dire_meta):
            os.remove(delete_dire_meta)

def logic(args):
    for arg in args:
        print(f"UnifySDKTool SDK Logic {arg}")

    SDKListTemplatePath = f"{UnityPath}/Assets/Scripts/_UnifySDK/_UnifySDK.Runtime/Core/ConfigSetting/Resources/SDKListTemplate.txt"
    print(f"SDKListTemplatePath: {SDKListTemplatePath}")

    AssetConfigPaths = f"{UnityPath}/Assets/Scripts/_UnifySDK/_UnifySDK.Editor/Editor/ConfigSetting/Resources"
    SDKListModel = None
    CustomSDKList = None
    if len(args) > 0:
        SDKListModel = args[0]
        print(f"args[0]: {args[0]}")  
    if len(args) > 1:
        CustomSDKList = args[1]
        print(f"args[1]: {args[1]}")  
    
    with open(SDKListTemplatePath, "r") as file:
        SDKListTemplate = file.read()
    sdkModelDic = json.loads(SDKListTemplate)
    needSDKList = []

    if SDKListModel is not None and SDKListModel not in sdkModelDic:
        print(f"SDKListTemplate 不包含该值 {SDKListModel} （Jenkins SDKListModel）")
    elif SDKListModel in sdkModelDic:
        needSDKList = sdkModelDic[SDKListModel].keys()

    assetConfigFiles = [file for file in os.listdir(AssetConfigPaths) if file.endswith(".asset")]

    for file in assetConfigFiles:
        filePath = os.path.join(AssetConfigPaths, file)
        sdkName,_ = os.path.splitext(file)
        if sdkName not in needSDKList:
        
            with open(filePath, "r") as assetFile:
                assetConfig = assetFile.read()
            
            list = assetConfig.split("  - assetFolder: ")[1:]
		      
            for item in list:
                item = item.replace("\n", "").replace("\r", "")
                targetPath = os.path.join(UnityDataPath, item)
                delete_dire_fun(targetPath)
    delet_path = f"{UnityDataPath}/Plugins" 
    delete_plugins_folder(delet_path)
    for file in assetConfigFiles:
        sdkName,_ = os.path.splitext(file)

        if sdkName in needSDKList:
            filePath = os.path.join(AssetConfigPaths, file)
            with open(filePath, "r") as assetFile:
                assetConfig = assetFile.read()

            list = assetConfig.split("  - assetFolder: ")[1:]

            for item in list:
                item = item.replace("\n", "").replace("\r", "")
                plugins_move_to_project(sdkName, item)

def main(args):
    if len(args) > 0:
        logic(args)    
    else:
        print(f"args count == 0")
		
		
if __name__ == "__main__":
    main(sys.argv[1:]) 
