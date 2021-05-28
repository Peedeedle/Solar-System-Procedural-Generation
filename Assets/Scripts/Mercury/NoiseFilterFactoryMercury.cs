////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactoryMercury.cs>
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

public class NoiseFilterFactoryMercury {

    public static INoiseFilterMercury CreateNoiseFilterMercury(NoiseSettingsMercury settingsMercury) {

        // Filter type corresponding to noise settings
        switch (settingsMercury.filterTypeMercury) {

            // Simple noise settings case
            case NoiseSettingsMercury.FilterTypeMercury.Simple:
                return new SimpleNoiseFilterMercury(settingsMercury.simpleNoiseSettingsMercury);

            // Rigid noise settings case
            case NoiseSettingsMercury.FilterTypeMercury.Rigid:
                return new RigidNoiseFilterMercury(settingsMercury.rigidNoiseSettingsMercury);

        }

        // If it is not any of the cases, return null
        return null;
    }

}