using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEngine.Experimental.Rendering.Universal {

    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Rendering/2D/Tilemap Shadow Caster 2D (Experimental)")]
    public class TilemapShadowCaster2D : ShadowCasterGroup2D {
        void Reset() {//TODO make a button do this

            foreach (Transform child in transform) {//TODO make a button do this
                GameObject.DestroyImmediate(child.gameObject);
            }

            if (GetShadowCasters() != null) {
                // foreach (ShadowCaster2D sc in GetShadowCasters()) {
                //     try {
                //         DestroyImmediate(sc.gameObject);
                //     } catch (Exception e) when (e is MissingReferenceException || e is NullReferenceException) {
                //         //TODO warn user
                //     }
                // }
                
                GetShadowCasters().Clear();
            }

            // Tilemaps.TilemapCollider2D collider = GetComponent<Tilemaps.TilemapCollider2D>();
            // collider.usedByComposite = true;
            // CompositeCollider2D compositeCollider = gameObject.AddComponent<CompositeCollider2D>();

            // try {
            //     if (compositeCollider.pathCount != 0) {
            //         for (int pathIndex = 0; pathIndex < compositeCollider.pathCount; pathIndex++) {
            //             Vector2[] pathVertices = new Vector2[compositeCollider.GetPathPointCount(pathIndex)];
            //             compositeCollider.GetPath(pathIndex, pathVertices);
            //             GameObject scHost = new GameObject();
            //             scHost.name = "ShadowCaster2D";
            //             scHost.transform.parent = transform;
            //             ShadowCaster2D sc = scHost.AddComponent<ShadowCaster2D>();
            //             sc.m_ShapePath = Array.ConvertAll<Vector2, Vector3>(pathVertices, vec2To3);
            //             sc.useRendererSilhouette = false;
            //             RegisterShadowCaster2D(sc);
            //         }
            //     } else {
            //         Debug.Log("Composite collider had no path");
            //     }

            // } catch (System.Exception ex) {
            //     Debug.Log(ex.ToString());
            // }

            // DestroyImmediate(compositeCollider);
            // DestroyImmediate(GetComponent<Rigidbody2D>());
            // collider.usedByComposite = false;


            Tilemaps.Tilemap tilemap = GetComponent<Tilemaps.Tilemap>();

            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                Sprite sprite = tilemap.GetSprite(position);
                if (sprite == null)
                    continue;

                int PhysicsShape = sprite.GetPhysicsShapeCount();
                if (PhysicsShape >= 0) {
                    GameObject scHost = new GameObject();
                    scHost.name = "ShadowCaster2D";//TODO give them a better name
                    scHost.transform.parent = transform;

                    List<Vector2> shapeVertices = new List<Vector2>(PhysicsShape);
                    sprite.GetPhysicsShape(0, shapeVertices);

                    ShadowCaster2D sc = scHost.AddComponent<ShadowCaster2D>();
                    sc.m_ShapePath = Array.ConvertAll<Vector2, Vector3>(shapeVertices.ToArray(), vec2To3);
                    
                    RegisterShadowCaster2D(sc);
                    sc.transform.position = tilemap.GetCellCenterWorld(new Vector3Int(position.x, position.y, 0));
                    sc.useRendererSilhouette = false;
                    sc.selfShadows = true;
                    sc.enabled = false;
                    sc.m_Mesh = null;
                    sc.enabled = true;
                }
            }
        }

        private Vector3 vec2To3(Vector2 inputVector) {
            return new Vector3(inputVector.x, inputVector.y, 0);
        }
    }
}
