////////////////////////////////////////////////////////////
// File:                 <SaturnEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetSaturn))]
public class SaturnEditor : Editor {

    // Planet
    PlanetSaturn planetSaturn;

    // Shape and colour editor
    Editor shapeEditorSaturn;
    Editor colourEditorSaturn;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetSaturn.GenerateSaturn();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Saturn")) {

            //Generate planet
            planetSaturn.GenerateSaturn();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorSaturn(planetSaturn.SaturnshapeSettings, planetSaturn.OnShapeSettingsUpdatedSaturn, ref planetSaturn.shapeSettingsSaturnFoldOut, ref shapeEditorSaturn);
        DrawSettingsEditorSaturn(planetSaturn.SaturncolourSettings, planetSaturn.OnColourSettingsUpdatedSaturn, ref planetSaturn.colourSettingsSaturnFoldOut, ref colourEditorSaturn);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorSaturn(Object settingsSaturn, System.Action onSettingsUpdatedSaturn, ref bool foldOut, ref Editor editorSaturn) {

        // If settings is not = null
        if (settingsSaturn != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsSaturn);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsSaturn, null, ref editorSaturn);
                    editorSaturn.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedSaturn != null) {
                            onSettingsUpdatedSaturn();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetSaturn = (PlanetSaturn)target;

    }

}

