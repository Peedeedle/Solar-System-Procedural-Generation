////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactoryMars {

    public static INoiseFilterMars CreateNoiseFilterMars(NoiseSettingsMars settingsMars) {

        // Filter type corresponding to noise settings
        switch (settingsMars.filterTypeMars) {

            // Simple noise settings case
            case NoiseSettingsMars.FilterTypeMars.Simple:
                return new SimpleNoiseFilterMars(settingsMars.simpleNoiseSettingsMars);

            // Rigid noise settings case
            case NoiseSettingsMars.FilterTypeMars.Rigid:
                return new RigidNoiseFilterMars(settingsMars.rigidNoiseSettingsMars);

        }

        // If it is not any of the cases, return null
        return null;
    }

}