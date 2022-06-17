using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;


[InitializeOnLoad]
public class SceneAutoSave
{

    public static bool AutoSaveEnabled
    {
        get => EditorPrefs.GetBool("AutoSaveEnabled", false);
        set
        {
            EditorPrefs.SetBool("AutoSaveEnabled", value);
            if (value)
                Stopwatch.Start();
            else
                Stopwatch.Stop();
        }
    }

    public static float MinutesBetweenSaves = 5;


    public static Stopwatch Stopwatch
    {
        get
        {
            if (stopwatch == null)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            return stopwatch;
        }
    }
    static Stopwatch stopwatch;


    static SceneAutoSave()
    {
        Enable();
    }

    static void Enable()
    {
        EditorApplication.update += OnUpdate;
        Stopwatch.Start();
    }

    static void OnUpdate()
    {
        if (!AutoSaveEnabled || Application.isPlaying || UnityUtils.IsInPrefabMode)
            return;

        if (Stopwatch.Elapsed.TotalMinutes > MinutesBetweenSaves)
        {
            UnityEngine.Debug.Log("Auto-saving project files and scenes...");
            Stopwatch.Restart();
            SaveSceneAndProject();
        }
    }


    static void SaveSceneAndProject()
    {
        AssetDatabase.SaveAssets();
        if (!string.IsNullOrEmpty(EditorSceneManager.GetActiveScene().path))
            EditorSceneManager.SaveOpenScenes();
    }



}
