////////////////////////////////////////////////////////////
// File:                 <JupiterEditor.cs>
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

[CustomEditor(typeof(PlanetJupiter))]
public class JupiterEditor : Editor {

    // Planet
    PlanetJupiter planetJupiter;

    // Shape and colour editor
    Editor shapeEditorJupiter;
    Editor colourEditorJupiter;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetJupiter.GenerateJupiter();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Jupiter")) {

            //Generate planet
            planetJupiter.GenerateJupiter();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorJupiter(planetJupiter.JupitershapeSettings, planetJupiter.OnShapeSettingsUpdatedJupiter, ref planetJupiter.shapeSettingsJupiterFoldOut, ref shapeEditorJupiter);
        DrawSettingsEditorJupiter(planetJupiter.JupitercolourSettings, planetJupiter.OnColourSettingsUpdatedJupiter, ref planetJupiter.colourSettingsJupiterFoldOut, ref colourEditorJupiter);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorJupiter(Object settingsJupiter, System.Action onSettingsUpdatedJupiter, ref bool foldOut, ref Editor editorJupiter) {

        // If settings is not = null
        if (settingsJupiter != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsJupiter);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsJupiter, null, ref editorJupiter);
                    editorJupiter.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedJupiter != null) {
                            onSettingsUpdatedJupiter();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetJupiter = (PlanetJupiter)target;

    }

}

