////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactoryMoon {

    public static INoiseFilterMoon CreateNoiseFilterMoon(NoiseSettingsMoon settingsMoon) {

        // Filter type corresponding to noise settings
        switch (settingsMoon.filterTypeMoon) {

            // Simple noise settings case
            case NoiseSettingsMoon.FilterTypeMoon.Simple:
                return new SimpleNoiseFilterMoon(settingsMoon.simpleNoiseSettingsMoon);

            // Rigid noise settings case
            case NoiseSettingsMoon.FilterTypeMoon.Rigid:
                return new RigidNoiseFilterMoon(settingsMoon.rigidNoiseSettingsMoon);

        }

        // If it is not any of the cases, return null
        return null;
    }

}