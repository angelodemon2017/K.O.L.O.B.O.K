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

        foreach (Material oldMat in materials)
        {
            // Пропускаем уже URP материалы
            if (oldMat.shader.name.Contains("Universal Render Pipeline"))
                continue;

            // Создаем новый материал с URP шейдером
            Material newMat = new Material(Shader.Find("Universal Render Pipeline/Lit"));

            // Копируем основные параметры
            if (oldMat.HasProperty("_MainTex"))
            {
                newMat.SetTexture("_BaseMap", oldMat.GetTexture("_MainTex"));
            }

            if (oldMat.HasProperty("_Color"))
            {
                newMat.SetColor("_BaseColor", oldMat.GetColor("_Color"));
            }

            // Копируем другие параметры, если они есть
            if (oldMat.HasProperty("_MetallicGlossMap"))
            {
                newMat.SetTexture("_MetallicGlossMap", oldMat.GetTexture("_MetallicGlossMap"));
            }

            if (oldMat.HasProperty("_BumpMap"))
            {
                newMat.SetTexture("_BumpMap", oldMat.GetTexture("_BumpMap"));
            }

            if (oldMat.HasProperty("_EmissionMap"))
            {
                newMat.SetTexture("_EmissionMap", oldMat.GetTexture("_EmissionMap"));
                newMat.EnableKeyword("_EMISSION");
            }

            // Сохраняем новый материал
            string path = AssetDatabase.GetAssetPath(oldMat);
            AssetDatabase.CreateAsset(newMat, path);
            convertedCount++;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"Converted {convertedCount} materials to URP");
    }
}