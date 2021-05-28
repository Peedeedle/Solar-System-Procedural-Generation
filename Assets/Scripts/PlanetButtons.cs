////////////////////////////////////////////////////////////
// File:                 <PlanetButtons.cs>
// Author:               <Jack Peedle>
// Date Created:         <24/03/2021>
// Brief:                <File responsible for Executing other scripts when UI buttons are pressed>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Buttons Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetButtons : MonoBehaviour
{

    public GeneratePlanets generatePlanetsScript;

    public void EarthButtonPressed() {
        generatePlanetsScript.GenerateTheEarth();
    }

    public void MoonButtonPressed() {
        generatePlanetsScript.GenerateTheMoon();
    }

    public void NeptuneButtonPressed() {
        generatePlanetsScript.GenerateNeptune();
    }

    public void UranusButtonPressed() {
        generatePlanetsScript.GenerateUranus();
    }

    public void JupiterButtonPressed() {
        generatePlanetsScript.GenerateJupiter();
    }

    public void PlutoButtonPressed() {
        generatePlanetsScript.GeneratePluto();
    }

    public void MercuryButtonPressed() {
        generatePlanetsScript.GenerateMercury();
    }

    public void SaturnButtonPressed() {
        generatePlanetsScript.GenerateSaturn();
    }

    public void MarsButtonPressed() {
        generatePlanetsScript.GenerateMars();
    }
    
    public void VenusButtonPressed() {
        generatePlanetsScript.GenerateVenus();
    }
    
}
