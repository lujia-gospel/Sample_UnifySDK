using UnityEngine;
using UnityEngine.Rendering.Universal;
 
#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
 
public class PostDataProcessor : IPostprocessBuildWithReport, IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
 
    public void OnPreprocessBuild(BuildReport report) => PostProcessData();
 
    public void OnPostprocessBuild(BuildReport report) => PostProcessData();
 
    void PostProcessData()
    {
        ScriptableRendererData[] rendererDataList = (ScriptableRendererData[])typeof(UniversalRenderPipelineAsset).GetField("m_RendererDataList", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(UniversalRenderPipeline.asset);
 
        for (int i = 0; i < rendererDataList.Length; i++)
        {
            #if UNITY_2020_1_OR_NEWER
            UniversalRendererData frd = (UniversalRendererData)rendererDataList[i];
            
            #else
            
            ForwardRendererData frd = (ForwardRendererData)rendererDataList[i];
            #endif
           
            if (frd != null)
            {
                frd.postProcessData.textures = null;
                frd.postProcessData.shaders = null;
            }
        }
    }
 
}
#endif