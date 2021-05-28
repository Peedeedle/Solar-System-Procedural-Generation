////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactorySaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactorySaturn {

    public static INoiseFilterSaturn CreateNoiseFilterSaturn(NoiseSettingsSaturn settingsSaturn) {

        // Filter type corresponding to noise settings
        switch (settingsSaturn.filterTypeSaturn) {

            // Simple noise settings case
            case NoiseSettingsSaturn.FilterTypeSaturn.Simple:
                return new SimpleNoiseFilterSaturn(settingsSaturn.simpleNoiseSettingsSaturn);

            // Rigid noise settings case
            case NoiseSettingsSaturn.FilterTypeSaturn.Rigid:
                return new RigidNoiseFilterSaturn(settingsSaturn.rigidNoiseSettingsSaturn);

        }

        // If it is not any of the cases, return null
        return null;
    }

}