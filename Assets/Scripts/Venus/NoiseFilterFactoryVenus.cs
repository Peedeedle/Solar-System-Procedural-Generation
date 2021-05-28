////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryVenus.cs>
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

public class NoiseFilterFactoryVenus {

    public static INoiseFilterVenus CreateNoiseFilterVenus(NoiseSettingsVenus settingsVenus) {

        // Filter type corresponding to noise settings
        switch (settingsVenus.filterTypeVenus) {

            // Simple noise settings case
            case NoiseSettingsVenus.FilterTypeVenus.Simple:
                return new SimpleNoiseFilterVenus(settingsVenus.simpleNoiseSettingsVenus);

            // Rigid noise settings case
            case NoiseSettingsVenus.FilterTypeVenus.Rigid:
                return new RigidNoiseFilterVenus(settingsVenus.rigidNoiseSettingsVenus);

        }

        // If it is not any of the cases, return null
        return null;
    }

}