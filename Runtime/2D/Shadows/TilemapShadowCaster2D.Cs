using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UnityEngine.Experimental.Rendering.Universal
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Rendering/2D/Tilemap Shadow Caster 2D (Experimental)")]
    public class TilemapShadowCaster2D : ShadowCasterGroup2D
    {
        void Reset() {
            // // var sc = Resources.Load<GameObject>("Misc/BoxShadowCaster");

            // Tilemap tilemap = GetComponent<Tilemap>();
            // // GameObject shadowCasterContainer = GameObject.Find("shadow_casters");
            // int i = 0;
            // foreach (var position in tilemap.cellBounds.allPositionsWithin) {
            //     if (tilemap.GetTile(position) == null)
            //         continue;

            //     int PhysicsShape = tiles[i].sprite.GetPhysicsShapeCount();
            //     if (PhysicsShape == 0) {
            //         tiles[i].colliderType = Tile.ColliderType.None;
            //     } else { // get the first one
            //         tiles[i].colliderType = Tile.ColliderType.Sprite;
            //         Vector2[] shapeVertices = new Vector2[PhysicsShape];
            //         compositeCollider.GetPhysicsShape(0, shapeVertices);
            //         // m_ShapePath = Array.ConvertAll<Vector2, Vector3>(pathVertices, vec2To3);
            //         // RegisterShadowCaster2D(new ShadowCaster2D());//TODO
            //     }

            //     GameObject shadowCaster = GameObject.Instantiate(sc, shadowCasterContainer.transform);
            //     shadowCaster.transform.position = TilemapUtils.GridCoordsToWorldCoords(new Vector2Int(position.x, position.y));
            //     shadowCaster.name = "shadow_caster_" + i;
            //     i++;
            // }
            if(GetShadowCasters() != null){
                GetShadowCasters().Clear();
            }

            Tilemaps.TilemapCollider2D collider = GetComponent<Tilemaps.TilemapCollider2D>();
            collider.usedByComposite = true;
            gameObject.AddComponent(typeof(CompositeCollider2D));
            CompositeCollider2D compositeCollider = GetComponent<CompositeCollider2D>();
            try {
                if (compositeCollider.pathCount != 0) {
                    for (int pathIndex = 0; pathIndex < compositeCollider.pathCount; pathIndex++) {
                        Vector2[] pathVertices = new Vector2[compositeCollider.GetPathPointCount(pathIndex)];
                        compositeCollider.GetPath(pathIndex, pathVertices);
                        ShadowCaster2D sc = new ShadowCaster2D();
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
