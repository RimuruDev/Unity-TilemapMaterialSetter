using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor.SceneManagement;

namespace RimuruDev.TilemapMaterialSetter
{
    public class TilemapMaterialSetter : EditorWindow
    {
        private string materialPath = "Assets/Internal/Common/PixelSnapMat.mat";
        private string scenesPath = "Assets/Internal/Scenes/Levels";

        [MenuItem("RimuruDev Tools/Set Tilemap Material")]
        public static void ShowWindow() =>
            GetWindow<TilemapMaterialSetter>("Set Tilemap Material");

        private void OnGUI()
        {
            GUILayout.Label("Tilemap Material Setter", EditorStyles.boldLabel);

            EditorGUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label("Paths", EditorStyles.boldLabel);

            materialPath = EditorGUILayout.TextField("Material Path", materialPath);
            scenesPath = EditorGUILayout.TextField("Scenes Path", scenesPath);

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("Set Material for All Tilemaps"))
                SetMaterialForAllTilemaps(materialPath, scenesPath);
        }

        private static void SetMaterialForAllTilemaps(string materialPath, string scenesPath)
        {
            var material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

            if (material == null)
            {
                Debug.LogError("Material not found at path: " + materialPath);
                return;
            }

            var scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { scenesPath });

            foreach (var scenePath in scenePaths)
            {
                var sceneFilePath = AssetDatabase.GUIDToAssetPath(scenePath);
                var scene = EditorSceneManager.OpenScene(sceneFilePath);

                var renderers = FindObjectsOfType<TilemapRenderer>();

                foreach (var renderer in renderers)
                {
                    renderer.material = material;
                    EditorUtility.SetDirty(renderer);
                }

                EditorSceneManager.MarkSceneDirty(scene);
                EditorSceneManager.SaveScene(scene);

                Debug.Log($"Material set for Tilemaps in scene: {sceneFilePath}");
            }

            AssetDatabase.SaveAssets();
            Debug.Log("Material set for all Tilemaps in all scenes.");
        }
    }
}