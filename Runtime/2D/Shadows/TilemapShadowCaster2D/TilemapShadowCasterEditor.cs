using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEngine.Experimental.Rendering.Universal {
    [CustomEditor(typeof(TilemapShadowCaster2D))]
    public class TilemapShadowCasterEditor : Editor {
        public override void OnInspectorGUI() {
            // DrawDefaultInspector();

            TilemapShadowCaster2D tilemapShadowCaster = (TilemapShadowCaster2D)target;

            if (GUILayout.Button("Gen from CompositeCollider2D")) {
                tilemapShadowCaster.ClearShadowCasters();
                tilemapShadowCaster.FromCompositeCollider2D();
            }

            if (GUILayout.Button("Gen from Grid Tiles Sprites")) {
                tilemapShadowCaster.ClearShadowCasters();
                tilemapShadowCaster.FromGridTilesSprites();
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Clear Children")) {
                tilemapShadowCaster.ClearShadowCasters();
            }
        }
    }
}