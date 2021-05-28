////////////////////////////////////////////////////////////
// File:                 <MercuryEditor.cs>
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

[CustomEditor(typeof(PlanetMercury))]
public class MercuryEditor : Editor {

    // Planet
    PlanetMercury planetMercury;

    // Shape and colour editor
    Editor shapeEditorMercury;
    Editor colourEditorMercury;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetMercury.GenerateMercury();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Mercury")) {

            //Generate planet
            planetMercury.GenerateMercury();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorMercury(planetMercury.MercuryshapeSettings, planetMercury.OnShapeSettingsUpdatedMercury, ref planetMercury.shapeSettingsMercuryFoldOut, ref shapeEditorMercury);
        DrawSettingsEditorMercury(planetMercury.MercurycolourSettings, planetMercury.OnColourSettingsUpdatedMercury, ref planetMercury.colourSettingsMercuryFoldOut, ref colourEditorMercury);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorMercury(Object settingsMercury, System.Action onSettingsUpdatedMercury, ref bool foldOut, ref Editor editorMercury) {

        // If settings is not = null
        if (settingsMercury != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsMercury);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsMercury, null, ref editorMercury);
                    editorMercury.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedMercury != null) {
                            onSettingsUpdatedMercury();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetMercury = (PlanetMercury)target;

    }

}

