using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class SpriteJiggleMaker : MonoBehaviour
{
    [Header("Sprite Bone Settings")]
    public Sprite sprite; 
    public int boneCount = 12; 
    public float radius = 2f; 

    [Button("Add Bones to Sprite DOESNT WORK")]
    public void AddBones()
    {
        if (sprite == null)
        {
            Debug.LogError("No sprite assigned!");
            return;
        }

        // Get the asset path of the Sprite
        string spritePath = AssetDatabase.GetAssetPath(sprite);
        if (string.IsNullOrEmpty(spritePath))
        {
            Debug.LogError("Unable to locate the Sprite asset in the project.");
            return;
        }

        // Load the importer for the Sprite
        var importer = AssetImporter.GetAtPath(spritePath) as TextureImporter;
        if (importer == null)
        {
            Debug.LogError("The asset does not have a valid TextureImporter.");
            return;
        }

        // Access the SerializedObject of the TextureImporter
        SerializedObject importerSerializedObject = new SerializedObject(importer);

        // Access the "m_SpriteSheet.m_Bones" property
        SerializedProperty bonesProperty = importerSerializedObject.FindProperty("m_SpriteSheet.m_Bones");
        if (bonesProperty == null)
        {
            Debug.LogError("Failed to find bones property in the Sprite's SerializedObject.");
            return;
        }

        // Add new bones to the property
        float angleStep = 360f / boneCount;
        bonesProperty.ClearArray();
        for (int i = 0; i < boneCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            bonesProperty.InsertArrayElementAtIndex(i);
            SerializedProperty boneProperty = bonesProperty.GetArrayElementAtIndex(i);

            boneProperty.FindPropertyRelative("name").stringValue = $"Bone_{i}";
            boneProperty.FindPropertyRelative("m_Position").vector3Value = position;
            boneProperty.FindPropertyRelative("m_Rotation").quaternionValue = Quaternion.identity;
            boneProperty.FindPropertyRelative("m_Length").floatValue = 1f;
            boneProperty.FindPropertyRelative("m_ParentId").intValue = -1; // No parent
        }

        // Apply changes and reimport the Sprite
        importerSerializedObject.ApplyModifiedProperties();
        importer.SaveAndReimport();

        Debug.Log($"{boneCount} bones added to {sprite.name} and saved to the .meta file!");
    }
}