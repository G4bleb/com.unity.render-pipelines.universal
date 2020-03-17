using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEngine.Experimental.Rendering.Universal {

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Rendering/2D/Tilemap Shadow Caster 2D (Experimental)")]
    public class TilemapShadowCaster2D : ShadowCasterGroup2D {
        void Reset() {
            if (GetShadowCasters() != null) {
                foreach (ShadowCaster2D sc in GetShadowCasters()) {
                    Destroy(sc.gameObject);
                }
                GetShadowCasters().Clear();
            }

            Tilemaps.TilemapCollider2D collider = GetComponent<Tilemaps.TilemapCollider2D>();
            collider.usedByComposite = true;
            CompositeCollider2D compositeCollider = gameObject.AddComponent<CompositeCollider2D>();

            try {
                if (compositeCollider.pathCount != 0) {
                    for (int pathIndex = 0; pathIndex < compositeCollider.pathCount; pathIndex++) {
                        Vector2[] pathVertices = new Vector2[compositeCollider.GetPathPointCount(pathIndex)];
                        compositeCollider.GetPath(pathIndex, pathVertices);
                        obj = new GameObject();
                        obj.transform.parent = gameObject;
                        ShadowCaster2D sc = obj.AddComponent<ShadowCaster2D>();
                        sc.m_ShapePath = Array.ConvertAll<Vector2, Vector3>(pathVertices, vec2To3);
                        sc.useRendererSilhouette = false;
                    }
                } else {
                    Debug.Log("Composite collider had no path");
                }

            } catch (System.Exception ex) {
                Debug.Log(ex.ToString());
            }

            DestroyImmediate(compositeCollider);
            DestroyImmediate(GetComponent<Rigidbody2D>());
            collider.usedByComposite = false;
        }

        private Vector3 vec2To3(Vector2 inputVector) {
            return new Vector3(inputVector.x, inputVector.y, 0);
        }
    }
}
