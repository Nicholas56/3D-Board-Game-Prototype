using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
/*EAS12337350
 * This is an editor for the tile script, to make it easier to implement into the game
 */
 [CustomEditor(typeof(TileScript))]
public class TileScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TileScript myTarget = (TileScript)target;

        DrawDefaultInspector();

        //Makes a button that calls a function to locate the local tiles and put them in the localTile list
        if (GUILayout.Button("Locate local tiles"))
        {
            myTarget.LocateLocalTiles();
        }
    }
}
