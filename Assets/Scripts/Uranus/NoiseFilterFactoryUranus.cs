////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactoryUranus {

    public static INoiseFilterUranus CreateNoiseFilterUranus(NoiseSettingsUranus settingsUranus) {

        // Filter type corresponding to noise settings
        switch (settingsUranus.filterTypeUranus) {

            // Simple noise settings case
            case NoiseSettingsUranus.FilterTypeUranus.Simple:
                return new SimpleNoiseFilterUranus(settingsUranus.simpleNoiseSettingsUranus);

            // Rigid noise settings case
            case NoiseSettingsUranus.FilterTypeUranus.Rigid:
                return new RigidNoiseFilterUranus(settingsUranus.rigidNoiseSettingsUranus);

        }

        // If it is not any of the cases, return null
        return null;
    }

}