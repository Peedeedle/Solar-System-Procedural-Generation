////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactoryNeptune {

    public static INoiseFilterNeptune CreateNoiseFilterNeptune(NoiseSettingsNeptune settingsNeptune) {

        // Filter type corresponding to noise settings
        switch (settingsNeptune.filterTypeNeptune) {

            // Simple noise settings case
            case NoiseSettingsNeptune.FilterTypeNeptune.Simple:
                return new SimpleNoiseFilterNeptune(settingsNeptune.simpleNoiseSettingsNeptune);

            // Rigid noise settings case
            case NoiseSettingsNeptune.FilterTypeNeptune.Rigid:
                return new RigidNoiseFilterNeptune(settingsNeptune.rigidNoiseSettingsNeptune);

        }

        // If it is not any of the cases, return null
        return null;
    }

}