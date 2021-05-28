////////////////////////////////////////////////////////////
// File:                 <NeptuneEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetNeptune))]
public class NeptuneEditor : Editor {

    // Planet
    PlanetNeptune planetNeptune;

    // Shape and colour editor
    Editor shapeEditorNeptune;
    Editor colourEditorNeptune;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetNeptune.GenerateNeptune();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Neptune")) {

            //Generate planet
            planetNeptune.GenerateNeptune();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorNeptune(planetNeptune.NeptuneshapeSettings, planetNeptune.OnShapeSettingsUpdatedNeptune, ref planetNeptune.shapeSettingsNeptuneFoldOut, ref shapeEditorNeptune);
        DrawSettingsEditorNeptune(planetNeptune.NeptunecolourSettings, planetNeptune.OnColourSettingsUpdatedNeptune, ref planetNeptune.colourSettingsNeptuneFoldOut, ref colourEditorNeptune);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorNeptune(Object settingsNeptune, System.Action onSettingsUpdatedNeptune, ref bool foldOut, ref Editor editorNeptune) {

        // If settings is not = null
        if (settingsNeptune != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsNeptune);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsNeptune, null, ref editorNeptune);
                    editorNeptune.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedNeptune != null) {
                            onSettingsUpdatedNeptune();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetNeptune = (PlanetNeptune)target;

    }

}

