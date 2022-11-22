using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WorldCreator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WorldCreator creator = (WorldCreator)target;

        if(GUILayout.Button("Create Level"))
        {
            creator.Generate();
        }

    }
}
