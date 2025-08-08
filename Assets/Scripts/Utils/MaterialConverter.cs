using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class MaterialConverter : EditorWindow
{
    [MenuItem("Tools/Convert Materials to URP")]
    public static void ShowWindow()
    {
        GetWindow<MaterialConverter>("Material Converter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Convert Built-in Materials to URP", EditorStyles.boldLabel);

        if (GUILayout.Button("Convert Selected Materials"))
        {
            ConvertSelectedMaterials();
        }

        if (GUILayout.Button("Convert All Materials in Project"))
        {
            ConvertAllMaterials();
        }
    }

    private static void ConvertSelectedMaterials()
    {
        var selectedObjects = Selection.objects;
        List<Material> materials = new List<Material>();

        foreach (var obj in selectedObjects)
        {
            if (obj is Material)
            {
                materials.Add((Material)obj);
            }
        }

        if (materials.Count == 0)
        {
            Debug.LogWarning("No materials selected!");
            return;
        }

        ConvertMaterials(materials.ToArray());
    }

    private static void ConvertAllMaterials()
    {
        string[] materialGuids = AssetDatabase.FindAssets("t:Material");
        List<Material> materials = new List<Material>();

        foreach (string guid in materialGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            materials.Add(mat);
        }

        ConvertMaterials(materials.ToArray());
    }

    private static void ConvertMaterials(Material[] materials)
    {
        int convertedCount = 0;
        Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");

        foreach (Material oldMat in materials)
        {
            // Пропускаем уже URP материалы
            if (oldMat.shader.name.Contains("Universal Render Pipeline"))
                continue;

            // Сохраняем все свойства перед изменением шейдера
            Texture mainTex = oldMat.HasProperty("_MainTex") ? oldMat.GetTexture("_MainTex") : null;
            Color mainColor = oldMat.HasProperty("_Color") ? oldMat.GetColor("_Color") : Color.white;
            Texture metallicMap = oldMat.HasProperty("_MetallicGlossMap") ? oldMat.GetTexture("_MetallicGlossMap") : null;
            Texture bumpMap = oldMat.HasProperty("_BumpMap") ? oldMat.GetTexture("_BumpMap") : null;
            Texture emissionMap = oldMat.HasProperty("_EmissionMap") ? oldMat.GetTexture("_EmissionMap") : null;
            Color emissionColor = oldMat.HasProperty("_EmissionColor") ? oldMat.GetColor("_EmissionColor") : Color.black;
            float metallic = oldMat.HasProperty("_Metallic") ? oldMat.GetFloat("_Metallic") : 0f;
            float smoothness = oldMat.HasProperty("_Glossiness") ? oldMat.GetFloat("_Glossiness") : 0.5f;
            float bumpScale = oldMat.HasProperty("_BumpScale") ? oldMat.GetFloat("_BumpScale") : 1f;

            // Меняем шейдер на URP
            oldMat.shader = urpShader;

            // Восстанавливаем свойства в соответствии с URP
            if (mainTex != null) oldMat.SetTexture("_BaseMap", mainTex);
            oldMat.SetColor("_BaseColor", mainColor);

            if (metallicMap != null) oldMat.SetTexture("_MetallicGlossMap", metallicMap);
            if (bumpMap != null) oldMat.SetTexture("_BumpMap", bumpMap);
            if (emissionMap != null)
            {
                oldMat.SetTexture("_EmissionMap", emissionMap);
                oldMat.SetColor("_EmissionColor", emissionColor);
                oldMat.EnableKeyword("_EMISSION");
            }

            oldMat.SetFloat("_Metallic", metallic);
            oldMat.SetFloat("_Smoothness", smoothness);
            oldMat.SetFloat("_BumpScale", bumpScale);

            // Для URP нужно установить режим работы с альфа-каналом
            if (mainColor.a < 1.0f || (mainTex != null && HasTransparency(mainTex)))
            {
                oldMat.SetFloat("_Surface", 1); // 1 = Transparent
                oldMat.SetFloat("_Blend", 0); // 0 = Alpha
                oldMat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                oldMat.SetOverrideTag("RenderType", "Transparent");
            }
            else
            {
                oldMat.SetFloat("_Surface", 0); // 0 = Opaque
                oldMat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
                oldMat.SetOverrideTag("RenderType", "Opaque");
            }

            convertedCount++;
            EditorUtility.SetDirty(oldMat);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Converted {convertedCount} materials to URP");
    }

    private static bool HasTransparency(Texture texture)
    {
        string path = AssetDatabase.GetAssetPath(texture);
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;
        return importer != null && importer.alphaSource != TextureImporterAlphaSource.None;
    }
}