////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryJupiter.cs>
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

public class NoiseFilterFactoryJupiter
{

    public static INoiseFilterJupiter CreateNoiseFilterJupiter(NoiseSettingsJupiter settingsJupiter) {

        // Filter type corresponding to noise settings
        switch (settingsJupiter.filterTypeJupiter) {

            // Simple noise settings case
            case NoiseSettingsJupiter.FilterTypeJupiter.Simple:
                return new SimpleNoiseFilterJupiter(settingsJupiter.simpleNoiseSettingsJupiter);

            // Rigid noise settings case
            case NoiseSettingsJupiter.FilterTypeJupiter.Rigid:
                return new RigidNoiseFilterJupiter(settingsJupiter.rigidNoiseSettingsJupiter);

        }

        // If it is not any of the cases, return null
        return null;
    }

}