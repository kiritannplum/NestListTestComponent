using System;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace NestListTestComponent
{
    public class Util
    {
        public static string GetCallerScriptRelativeDirectoryPath([CallerFilePath] string sourceFilePath = "") => new Uri(Application.dataPath).MakeRelativeUri(new(System.IO.Path.GetDirectoryName(sourceFilePath))).ToString();
    }

    [CustomEditor(typeof(NestListTestComponent))]
    public class NestListTestComponentEditor : Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            int instanceID = serializedObject.targetObject.GetInstanceID();
            string propertyInstancePath = $"{instanceID}";

            Debug.LogWarning($"CreateInspectorGUI/{GetType().Name}\r\n{propertyInstancePath}");

            string layoutFilePathBase = $"{Util.GetCallerScriptRelativeDirectoryPath()}/Layouts/{GetType().Name}";
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{layoutFilePathBase}.uxml");
            VisualElement uxml = visualTreeAsset.CloneTree();

            ListView u_TestList = uxml.Q<ListView>("TestList");
            u_TestList.BindProperty(serializedObject.FindProperty(nameof(NestListTestComponent._TestList)));

            uxml.Bind(serializedObject);

            EditorApplication.delayCall += () =>
            {
                Debug.LogError($"DelayCall/{GetType().Name}\r\n{propertyInstancePath}");
            };

            return uxml;
        }
    }

    [CustomPropertyDrawer(typeof(TestList))]
    public class TestListDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedObject serializedObject = property.serializedObject;

            int instanceID = serializedObject.targetObject.GetInstanceID();
            string propertyPath = property.propertyPath;
            string propertyInstancePath = $"{instanceID}.{propertyPath}";

            Debug.LogWarning($"CreatePropertyGUI/{GetType().Name}\r\n{propertyInstancePath}");

            string layoutFilePathBase = $"{Util.GetCallerScriptRelativeDirectoryPath()}/Layouts/TestListDrawer";
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{layoutFilePathBase}.uxml");
            VisualElement uxml = visualTreeAsset.CloneTree();

            ListView u_TestInners = uxml.Q<ListView>("TestInners");
            u_TestInners.BindProperty(property.FindPropertyRelative(nameof(TestList._TestInners)));

            EditorApplication.delayCall += () =>
            {
                Debug.LogError($"DelayCall/{GetType().Name}\r\n{propertyInstancePath}");
            };

            return uxml;
        }
    }
}