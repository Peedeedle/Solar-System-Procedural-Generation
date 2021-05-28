////////////////////////////////////////////////////////////
// File:                 <PlanetEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

    // Planet
    Planet planet;

    // Shape and colour editor
    Editor shapeEditor;
    Editor colourEditor;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planet.GeneratePlanetEarth();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Planet")) {

            //Generate planet
            planet.GeneratePlanetEarth();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdatedEarth, ref planet.shapeSettingsFoldOut, ref shapeEditor);
        DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsUpdatedEarth, ref planet.colourSettingsFoldOut, ref colourEditor);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldOut, ref Editor editor) {

        // If settings is not = null
        if (settings != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settings);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settings, null, ref editor);
                    editor.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdated != null) {
                            onSettingsUpdated();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planet = (Planet)target;

    }

}

