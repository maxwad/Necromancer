using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;

[Overlay(typeof(SceneView), "Maksim")]
[Icon(RootIconsFolder + "Auto-SaveIcon.png")]
public class MaksimToolbarOverlay : ToolbarOverlay
{
    MaksimToolbarOverlay() : base(
        AutoSaveToolbarDropdownToggle.id)
    { }

    public const string RootIconsFolder = "Assets/Editor/";
}


[EditorToolbarElement(id, typeof(SceneView))]
class AutoSveToolbarDropdownToggle : EditorToolbarDropdownToggle
{
    public const string id = "MaksimToolbar/AutoSaveToolbarDropdownToggle";

    public AutoSveToolbarDropdownToggle()
    {
        text = "Auto-Save";
        tooltip = "Save scene in selected count of minutes";
        icon = AssetDatabase.LoadAssetAtPath<Texture2D>(MaksimToolbarOverlay.RootIconsFolder + "Auto-SaveIcon.png");

        dropdownClickable.clicked += Dropdown;

        value = SceneAutoSave.AutoSaveEnabled;
    }

    public override void SetValueWithoutNotify(bool newValue)
    {
        SceneAutoSave.AutoSaveEnabled = newValue;
        base.SetValueWithoutNotify(newValue);
    }

    void Dropdown()
    {
        var menu = new GenericMenu();
        menu.AddItem(new GUIContent("1 Minute"), SceneAutoSave.MinutesBetweenSaves == 1, () => SceneAutoSave.MinutesBetweenSaves = 1);
        menu.AddItem(new GUIContent("3 Minutes"), SceneAutoSave.MinutesBetweenSaves == 3, () => SceneAutoSave.MinutesBetweenSaves = 3);
        menu.AddItem(new GUIContent("5 Minutes"), SceneAutoSave.MinutesBetweenSaves == 5, () => SceneAutoSave.MinutesBetweenSaves = 5);
        menu.AddItem(new GUIContent("10 Minutes"), SceneAutoSave.MinutesBetweenSaves == 10, () => SceneAutoSave.MinutesBetweenSaves = 10);
        menu.ShowAsContext();
    }
}
