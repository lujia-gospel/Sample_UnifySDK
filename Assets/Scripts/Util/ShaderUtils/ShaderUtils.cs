using System.Collections;
using System.Collections.Generic;
using Horizon;
using UnityEngine;
using UnityEngine.Rendering;

public class ShaderUtils 
{
    // 编辑器下bundle的重置shader
    public static T[] EditorResetShader<T>(Transform transform) where T : Renderer
    {
#if UNITY_EDITOR
        T[] renders = transform.GetComponentsInChildren<T>();

        for (int i = 0; i < renders.Length; i++)
        {
            T render = renders[i];
            render.material.shader = ShaderCache.GetShader(render.material.shader.name);
        }

        return renders;
#else
            return null;
#endif
    }

    // 编辑器下bundle的重置shader
    public static void EditorResetParticleShader(Transform transform)
    {
#if UNITY_EDITOR
        ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < particles.Length; i++)
        {
            ParticleSystemRenderer render = particles[i].GetComponent<ParticleSystemRenderer>();

            if (render.material != null)
                render.material.shader = ShaderCache.GetShader(render.material.shader.name);

            if (render.trailMaterial != null)
                render.trailMaterial.shader = ShaderCache.GetShader(render.material.shader.name);
        }
#endif
    }

    public void SetShaderFloatValue(MeshRenderer[] renderers, int propertyId, float value)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            MeshRenderer renderer = renderers[i];
            if (!renderer.material.HasProperty(propertyId))
            {
                continue;
            }
            renderer.material.SetFloat(propertyId, value);
        }
    }

    public static void SetMaterialKeyword(GameObject root, string keyName, bool isEnable)
    {
        if (root == null)
        {
            UnityEngine.Debug.LogError("ERROR : material keyword is null !!!!! " );
            return;
        }
        MeshRenderer[] renderers = root.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
           MeshRenderer renderer = renderers[i];
            if(isEnable)
                renderer.material.EnableKeyword(keyName);
            else
                renderer.material.DisableKeyword(keyName);
        }
    }

    internal static void SetShaderFloatValue(int propertiesId, float realVal, params Material[] mats)
    {
        if (mats == null || mats.Length == 0)
            return;

        for (int i = 0; i < mats.Length; i++)
        {
            Material mat = mats[i];
            if (!mat.HasProperty(propertiesId))
            {
                continue;
            }
            mat.SetFloat(propertiesId, realVal);
        }
    }

    internal static void SetShaderColorValue(int propertiesId, Color color, params Material[] mats)
    {
        if (mats == null || mats.Length == 0)
            return;

        for (int i = 0; i < mats.Length; i++)
        {
            Material mat = mats[i];
            if (!mat.HasProperty(propertiesId))
            {
                continue;
            }
            mat.SetColor(propertiesId, color);
        }
    }

    public static bool HasThisProperties(Shader shader, string properties)
    {
        if (shader == null)
            return false;
#if UNITY_EDITOR
        for (int i = 0; i < UnityEditor.ShaderUtil.GetPropertyCount(shader); i++)
        {
            if (UnityEditor.ShaderUtil.GetPropertyName(shader, i).Equals(properties))
                return true;
        }
#endif

        return false;
    }

}
