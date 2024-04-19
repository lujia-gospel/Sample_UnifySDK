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
UnfiyRootPath = os.path.join(UnityDataPath, "Scripts/_UnifySDK")
ExternalRootPath = os.path.join(UnityDataPath, "Scripts/_UnifySDK/_UnifySDK.Extend")

print("root_path:"+root_path)
print("UnityPath:"+UnityPath)
print("UnityDataPath:"+UnityDataPath)
print("ExternalRootPath:"+ExternalRootPath)

def plugins_move_to_project(file_name):
    source_path = f"{ExternalRootPath}/{file_name}/Plugins"
    # 检查源路径是否存在
    if not os.path.exists(source_path):
        print(f"源路径不存在: {source_path}")
        return  # 如果源路径不存在，直接返回并不执行后续逻辑

    target_path = f"{UnityDataPath}/Plugins"
    source_dire_info = os.path.abspath(source_path)
    dest_dire = os.path.abspath(target_path)
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

def delete_unneeded_directories_and_meta(parent_directory, needSDKList):
    """
    删除parent_directory下不在needSDKList中的所有子目录及其对应的.meta文件。
    :param parent_directory: 要遍历的父目录路径
    :param needSDKList: 包含需要保留的子目录名称的列表
    """
    # 获取parent_directory下的所有子目录
    subdirectories = [d for d in os.listdir(parent_directory) if os.path.isdir(os.path.join(parent_directory, d))]

    for subdir in subdirectories:
        # 如果子目录名称不在needSDKList中，则删除该子目录及其对应的.meta文件
        if subdir not in needSDKList:
            full_path = os.path.join(parent_directory, subdir)
            meta_path = f"{full_path}.meta"  # 构造.meta文件的路径

            shutil.rmtree(full_path)  # 删除子目录
            print(f"已删除目录: {full_path}")

            if os.path.exists(meta_path):  # 检查对应的.meta文件是否存在
                os.remove(meta_path)  # 删除.meta文件
                print(f"已删除.meta文件: {meta_path}")

def logic(args):
    for arg in args:
        print(f"UnifySDKTool SDK Logic {arg}")

    SDKListTemplatePath = os.path.join(UnfiyRootPath,"_UnifySDK.Runtime/Core/ConfigSetting/Resources/SDKListTemplate.txt")
    print(f"SDKListTemplatePath: {SDKListTemplatePath}")
    SDKListModel = None
    if len(args) > 0:
        SDKListModel = args[0]
        print(f"args[0]（Jenkins SDKListModel）: {args[0]}") 
    
    with open(SDKListTemplatePath, "r") as file:
        SDKListTemplate = file.read()
    sdkModelDic = json.loads(SDKListTemplate)
    needSDKList = []

    if SDKListModel is not None and SDKListModel not in sdkModelDic:
        print(f"SDKListTemplate 不包含该值 {SDKListModel} （Jenkins SDKListModel）")
    elif SDKListModel in sdkModelDic:
        needSDKList = sdkModelDic[SDKListModel].keys()

    subdirectories = delete_unneeded_directories_and_meta(ExternalRootPath,needSDKList)
    print("子目录列表:", subdirectories)

    delet_path = f"{UnityDataPath}/Plugins" 
    delete_plugins_folder(delet_path)
    for sdkName in needSDKList:
        plugins_move_to_project(sdkName)

def main(args):
    if len(args) > 0:
        logic(args)    
    else:
        print(f"args count == 0")
		
		
if __name__ == "__main__":
    main(sys.argv[1:]) 
