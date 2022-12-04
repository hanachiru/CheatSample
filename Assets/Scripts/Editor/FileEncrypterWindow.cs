using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class FileEncrypterWindow : EditorWindow
    {
        private static readonly byte[] Key = Encoding.ASCII.GetBytes("abcdefghijklmnop");
            
        private string _targetFilePath;
        
        [MenuItem("Tools/FileEncrypter")]
        private static void Init()
        {
            var instance = GetWindow<FileEncrypterWindow>();
            instance.minSize = new Vector2(700, 80);
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Target File", _targetFilePath);
            if (GUILayout.Button("Select Target File"))
            {
                var selectedPathName = EditorUtility.OpenFilePanel( "Select Target Folder",  Application.dataPath,  "");
                if (!string.IsNullOrEmpty(selectedPathName)) _targetFilePath = selectedPathName;
            }

            if (GUILayout.Button("Encrypt"))
            {
                if (!File.Exists(_targetFilePath))
                {
                    Debug.Log($"対象ファイルが存在しません。 : {_targetFilePath}");
                    return;
                }

                var file = File.ReadAllText(_targetFilePath);
                var encryptedFile = FileEncrypter.Encrypy(file, Key);
                File.WriteAllBytes(_targetFilePath, encryptedFile);
                AssetDatabase.Refresh();
                Debug.Log("Encrypted");
            }
        }
    }
}