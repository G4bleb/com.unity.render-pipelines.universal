using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TilemapShadowCaster2D))]
public class TilemapShadowCasterEditor : Editor
{
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        TilemapShadowCaster2D tilemapShadowCaster = (TilemapShadowCaster2D)target;

        if (GUILayout.Button("Bake Shadow Casters from CompositeCollider2D")) {
            tilemapShadowCaster.ClearShadowCasters();
            tilemapShadowCaster.FromCompositeCollider2D();
        }

        if(GUILayout.Button("Generate Shadow Casters from Grid Tiles Sprites")){
            tilemapShadowCaster.ClearShadowCasters();
            tilemapShadowCaster.FromGridTilesSprites();
        }

        GUILayout.Space(20);

        if(GUILayout.Button("Clear Shadow Casters")){
            tilemapShadowCaster.ClearShadowCasters();
        }
    }
}