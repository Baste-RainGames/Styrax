using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyController))]
public class EnemyControllerEditor : Editor
{
    private void OnSceneGUI()
    {
        var start = serializedObject.FindProperty(nameof(EnemyController.start));
        var end = serializedObject.FindProperty(nameof(EnemyController.end));

        var targetPos = (Vector2) ((EnemyController) target).transform.position;

        start.vector2Value = Handles.PositionHandle(start.vector2Value + targetPos, Quaternion.identity) - (Vector3) targetPos;
        end.vector2Value = Handles.PositionHandle(end.vector2Value + targetPos, Quaternion.identity) - (Vector3) targetPos;

        serializedObject.ApplyModifiedProperties();
    }
}