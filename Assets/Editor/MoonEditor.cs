////////////////////////////////////////////////////////////
// File:                 <MoonEditor.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the settings on the editor>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Planets working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetMoon))]
public class MoonEditor : Editor {

    // Planet
    PlanetMoon planetMoon;

    // Shape and colour editor
    Editor shapeEditorMoon;
    Editor colourEditorMoon;

    // override inspector
    public override void OnInspectorGUI() {

        // using variable check changed GUI
        using (var check = new EditorGUI.ChangeCheckScope()) {
            base.OnInspectorGUI();

            // if check has changed
            if (check.changed) {

                // Generate Planet
                planetMoon.GenerateMoon();

            }
        }
        
        // if GUI button is pressed
        if (GUILayout.Button("Generate Planet")) {

            //Generate planet
            planetMoon.GenerateMoon();

        }

        // settings for each editor (shape / colour)
        DrawSettingsEditorMoon(planetMoon.MoonshapeSettings, planetMoon.OnShapeSettingsUpdatedMoon, ref planetMoon.shapeSettingsMoonFoldOut, ref shapeEditorMoon);
        DrawSettingsEditorMoon(planetMoon.MooncolourSettings, planetMoon.OnColourSettingsUpdatedMoon, ref planetMoon.colourSettingsMoonFoldOut, ref colourEditorMoon);

    }

    // Draws editor settings on GUI
    void DrawSettingsEditorMoon(Object settingsMoon, System.Action onSettingsUpdatedMoon, ref bool foldOut, ref Editor editorMoon) {

        // If settings is not = null
        if (settingsMoon != null) {

            // Include title bar in inspector for these settings
            foldOut = EditorGUILayout.InspectorTitlebar(foldOut, settingsMoon);

            // Check if the Editor GUI has changed
            using (var check = new EditorGUI.ChangeCheckScope()) {

                // if fold out inspector is true
                if (foldOut) {

                    // set editor and reference of editor
                    CreateCachedEditor(settingsMoon, null, ref editorMoon);
                    editorMoon.OnInspectorGUI();

                    // If check changed
                    if (check.changed) {

                        // if on settings update is not = null, invoke on settings updated
                        if (onSettingsUpdatedMoon != null) {
                            onSettingsUpdatedMoon();
                        }

                    }

                }

            }

        }

        
        
    }

    private void OnEnable() {

        // Planet = planet target
        planetMoon = (PlanetMoon)target;

    }

}

