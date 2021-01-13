using System;
using System.Collections;
using System.Collections.Generic;
using Main;
using UnityEditor;
using UnityEngine;

public class BonnouCSVImportPostprocessor : AssetPostprocessor
{
    private const string AssetName = "Bonnou.csv";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string str in importedAssets)
        {
            if (str.IndexOf(AssetName, StringComparison.Ordinal) == -1) continue;
            TextAsset data = AssetDatabase.LoadAssetAtPath<TextAsset>(str);
            string assetFile = str.Replace(".csv", ".asset");
            var gm = AssetDatabase.LoadAssetAtPath<BonnouRepository>(assetFile);
            if (gm == null)
            {
                gm = ScriptableObject.CreateInstance<BonnouRepository>();
                AssetDatabase.CreateAsset(gm, assetFile);
            }

            gm.bonnouEntities = CSVSerializer.Deserialize<BonnouEntity>(data.text);

            EditorUtility.SetDirty(gm);
            AssetDatabase.SaveAssets();
        #if DEBUG_LOG || UNITY_EDITOR
            Debug.Log("Reimported Asset: " + str);
        #endif
        }
    }
}