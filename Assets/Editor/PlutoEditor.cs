////////////////////////////////////////////////////////////
// File:                 <PlutoEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetPluto))]
public class PlutoEditor : Editor {

    // Planet
    PlanetPluto planetPluto;

    // Shape and colour editor
    Editor shapeEditorPluto;
    Editor colourEditorPluto;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetPluto.GeneratePluto();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Pluto")) {

            //Generate planet
            planetPluto.GeneratePluto();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorPluto(planetPluto.PlutoshapeSettings, planetPluto.OnShapeSettingsUpdatedPluto, ref planetPluto.shapeSettingsPlutoFoldOut, ref shapeEditorPluto);
        DrawSettingsEditorPluto(planetPluto.PlutocolourSettings, planetPluto.OnColourSettingsUpdatedPluto, ref planetPluto.colourSettingsPlutoFoldOut, ref colourEditorPluto);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorPluto(Object settingsPluto, System.Action onSettingsUpdatedPluto, ref bool foldOut, ref Editor editorPluto) {

        // If settings is not = null
        if (settingsPluto != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsPluto);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsPluto, null, ref editorPluto);
                    editorPluto.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedPluto != null) {
                            onSettingsUpdatedPluto();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetPluto = (PlanetPluto)target;

    }

}

