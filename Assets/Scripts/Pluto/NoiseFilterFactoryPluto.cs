////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactoryPluto
{

    public static INoiseFilterPluto CreateNoiseFilterPluto(NoiseSettingsPluto settingsPluto) {

        // Filter type corresponding to noise settings
        switch (settingsPluto.filterTypePluto) {

            // Simple noise settings case
            case NoiseSettingsPluto.FilterTypePluto.Simple:
                return new SimpleNoiseFilterPluto(settingsPluto.simpleNoiseSettingsPluto);

            // Rigid noise settings case
            case NoiseSettingsPluto.FilterTypePluto.Rigid:
                return new RigidNoiseFilterPluto(settingsPluto.rigidNoiseSettingsPluto);

        }

        // If it is not any of the cases, return null
        return null;
    }

}