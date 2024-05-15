# Unity Tilemap Material Setter

Unity Tilemap Material Setter - это инструмент для редактора Unity, который автоматизирует процесс установки материалов для всех компонентов `TilemapRenderer` во всех сценах, находящихся в указанной директории.

## Зачем это нужно

В крупных проектах Unity с множеством сцен и тайлмапов вручную изменять материал для каждого `TilemapRenderer` может быть трудоемким и подверженным ошибкам процессом. Этот инструмент решает данную проблему, автоматизируя этот процесс и экономя ваше время.

<img width="504" alt="image" src="https://github.com/RimuruDev/Unity-TilemapMaterialSetter/assets/85500556/1325ffbb-56c7-47b4-af62-632aff6bb927">


## Как использовать

### Установка

1. Скачайте и распакуйте [последний релиз](https://github.com/RimuruDev/Unity-TilemapMaterialSetter/releases) или склонируйте репозиторий:

    ```bash
    git clone https://github.com/RimuruDev/Unity-TilemapMaterialSetter.git
    ```

2. Поместите папку `Editor` в директорию вашего проекта Unity.

### Использование

1. Откройте Unity и дождитесь компиляции скрипта.
2. В меню Unity выберите `RimuruDev Tools > Set Tilemap Material`.
3. В открывшемся окне введите путь к материалу и путь к папке со сценами.
    - `Material Path`: Путь к материалу, который вы хотите установить (например, `Assets/Internal/Common/PixelSnapMat.mat`).
    - `Scenes Path`: Путь к директории, содержащей ваши сцены (например, `Assets/Internal/Scenes/Levels`).
4. Нажмите кнопку `Set Material for All Tilemaps`.

Скрипт откроет каждую сцену из указанной директории, найдет все компоненты `TilemapRenderer` и установит для них указанный материал. После этого он сохранит изменения в сценах.

## Пример кода

```csharp
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
