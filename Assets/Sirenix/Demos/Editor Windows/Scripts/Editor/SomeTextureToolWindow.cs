using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Sirenix.Demos.Editor_Windows.Scripts.Editor
{
    public class SomeTextureToolWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Odin/Demos/Odin Editor Window Demos/Some Texture Tool")]
        private static void OpenWindow()
        {
            var window = GetWindow<SomeTextureToolWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 600);
            window.titleContent = new GUIContent("Some Texture Tool Window");
        }

        [BoxGroup("Settings")]
        [FolderPath(RequireExistingPath = true)]
        public string OutputPath
        {
            // Use EditorPrefs to hold persisntent user-variables.
            get { return EditorPrefs.GetString("SomeTextureToolWindow.OutputPath"); }
            set { EditorPrefs.SetString("SomeTextureToolWindow.OutputPath", value); }
        }

        [EnumToggleButtons]
        [BoxGroup("Settings")]
        public ScaleMode ScaleMode;

        [HorizontalGroup(0.5f, PaddingRight = 5, LabelWidth = 70)]
        public Texture[] Textures = new Texture[8];

        [ReadOnly]
        [HorizontalGroup]
        [InlineEditor(InlineEditorModes.LargePreview)]
        public Texture Preview;

        [Button(ButtonSizes.Gigantic), GUIColor(0, 1, 0)]
        public void PerformSomeAction()
        {
        }
    }
}
#endif
