////////////////////////////////////////////////////////////
// File:                 <MarsEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetMars))]
public class MarsEditor : Editor {

    // Planet
    PlanetMars planetMars;

    // Shape and colour editor
    Editor shapeEditorMars;
    Editor colourEditorMars;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetMars.GenerateMars();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Mars")) {

            //Generate planet
            planetMars.GenerateMars();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorMars(planetMars.MarsshapeSettings, planetMars.OnShapeSettingsUpdatedMars, ref planetMars.shapeSettingsMarsFoldOut, ref shapeEditorMars);
        DrawSettingsEditorMars(planetMars.MarscolourSettings, planetMars.OnColourSettingsUpdatedMars, ref planetMars.colourSettingsMarsFoldOut, ref colourEditorMars);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorMars(Object settingsMars, System.Action onSettingsUpdatedMars, ref bool foldOut, ref Editor editorMars) {

        // If settings is not = null
        if (settingsMars != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsMars);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsMars, null, ref editorMars);
                    editorMars.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedMars != null) {
                            onSettingsUpdatedMars();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetMars = (PlanetMars)target;

    }

}

