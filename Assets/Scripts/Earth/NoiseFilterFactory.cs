////////////////////////////////////////////////////////////
// File:                 <NoiseFilterFactory.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for creating noise filters>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFilterFactory {

    public static INoiseFilter CreateNoiseFilter (NoiseSettings settings) {

        // Filter type corresponding to noise settings
        switch (settings.filterType) {

            // Simple noise settings case
            case NoiseSettings.FilterType.Simple:
                return new SimpleNoiseFilter(settings.simpleNoiseSettings);

            // Rigid noise settings case
            case NoiseSettings.FilterType.Rigid:
                return new RigidNoiseFilter(settings.rigidNoiseSettings);

        }

        // If it is not any of the cases, return null
        return null;
    }

}