////////////////////////////////////////////////////////////
// File:                 <UranusEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetUranus))]
public class UranusEditor : Editor {

    // Planet
    PlanetUranus planetUranus;

    // Shape and colour editor
    Editor shapeEditorUranus;
    Editor colourEditorUranus;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetUranus.GenerateUranus();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Uranus")) {

            //Generate planet
            planetUranus.GenerateUranus();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorUranus(planetUranus.UranusshapeSettings, planetUranus.OnShapeSettingsUpdatedUranus, ref planetUranus.shapeSettingsUranusFoldOut, ref shapeEditorUranus);
        DrawSettingsEditorUranus(planetUranus.UranuscolourSettings, planetUranus.OnColourSettingsUpdatedUranus, ref planetUranus.colourSettingsUranusFoldOut, ref colourEditorUranus);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorUranus(Object settingsUranus, System.Action onSettingsUpdatedUranus, ref bool foldOut, ref Editor editorUranus) {

        // If settings is not = null
        if (settingsUranus != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsUranus);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsUranus, null, ref editorUranus);
                    editorUranus.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedUranus != null) {
                            onSettingsUpdatedUranus();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetUranus = (PlanetUranus)target;

    }

}

