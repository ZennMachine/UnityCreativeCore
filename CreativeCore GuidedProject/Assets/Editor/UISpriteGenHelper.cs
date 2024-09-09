using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpriteGenTool
{
    [CustomEditor(typeof(SpriteGenTool.UISpriteGenerator))]
    public class UISpriteGenHelper : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SpriteGenTool.UISpriteGenerator spriteGen = (SpriteGenTool.UISpriteGenerator)target;

            EditorGUILayout.Space();

            if(GUILayout.Button("Generate UI Sprites"))
            {
                spriteGen.ProcessByBounds();
            }
        }
    }

}
