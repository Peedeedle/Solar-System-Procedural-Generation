////////////////////////////////////////////////////////////
// File:                 <VenusEditor.cs>
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

[CustomEditor(typeof(PlanetVenus))]
public class VenusEditor : Editor {

    // Planet
    PlanetVenus planetVenus;

    // Shape and colour editor
    Editor shapeEditorVenus;
    Editor colourEditorVenus;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetVenus.GenerateVenus();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Venus")) {

            //Generate planet
            planetVenus.GenerateVenus();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorVenus(planetVenus.VenusshapeSettings, planetVenus.OnShapeSettingsUpdatedVenus, ref planetVenus.shapeSettingsVenusFoldOut, ref shapeEditorVenus);
        DrawSettingsEditorVenus(planetVenus.VenuscolourSettings, planetVenus.OnColourSettingsUpdatedVenus, ref planetVenus.colourSettingsVenusFoldOut, ref colourEditorVenus);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorVenus(Object settingsVenus, System.Action onSettingsUpdatedVenus, ref bool foldOut, ref Editor editorVenus) {

        // If settings is not = null
        if (settingsVenus != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsVenus);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsVenus, null, ref editorVenus);
                    editorVenus.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedVenus != null) {
                            onSettingsUpdatedVenus();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetVenus = (PlanetVenus)target;

    }

}

