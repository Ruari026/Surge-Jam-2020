using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PersistantData))]
public class PersistantDataResetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset HiScore"))
        {
            PersistantData pd = (PersistantData)target;
            pd.ResetSavedHiScore();
        }

        if (GUILayout.Button("Reset Tutorial"))
        {
            PersistantData pd = (PersistantData)target;
            pd.ResetTutorialSave();
        }
    }
}
