////////////////////////////////////////////////////////////
// File:                 <GeneratePlanets.cs>
// Author:               <Jack Peedle>
// Date Created:         <24/03/2021>
// Brief:                <File responsible for Generating the planets once buttons have been pressed>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Planets working + updated planets>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanets : MonoBehaviour
{

    public bool CurrentPlanetUranus;
    public bool CurrentPlanetEarth;
    public bool CurrentPlanetMoon;
    public bool CurrentPlanetNeptune;
    public bool CurrentPlanetJupiter;
    public bool CurrentPlanetPluto;
    public bool CurrentPlanetMercury;
    public bool CurrentPlanetSaturn;
    public bool CurrentPlanetMars;
    public bool CurrentPlanetVenus;

    public bool CurrentInfoPlanet;
    public bool CurrentInfoMoon;
    public bool CurrentInfoUranus;
    public bool CurrentInfoNeptune;
    public bool CurrentInfoJupiter;
    public bool CurrentInfoPluto;
    public bool CurrentInfoMercury;
    public bool CurrentInfoSaturn;
    public bool CurrentInfoMars;
    public bool CurrentInfoVenus;

    public GameObject Uranus;
    public GameObject Earth;
    public GameObject Moon;
    public GameObject Neptune;
    public GameObject Jupiter;
    public GameObject Pluto;
    public GameObject Mercury;
    public GameObject Saturn;
    public GameObject Mars;
    public GameObject Venus;

    public GameObject PlanetInfoPanel;
    public GameObject MoonInfoPanel;
    public GameObject UranusInfoPanel;
    public GameObject NeptuneInfoPanel;
    public GameObject JupiterInfoPanel;
    public GameObject PlutoInfoPanel;
    public GameObject MercuryInfoPanel;
    public GameObject SaturnInfoPanel;
    public GameObject MarsInfoPanel;
    public GameObject VenusInfoPanel;

    // Public reference to planet script
    public Planet planetScript;
    public PlanetMoon planetMoonScript;
    public PlanetNeptune planetNeptuneScript;
    public PlanetUranus planetUranusScript;
    public PlanetJupiter planetJupiterScript;
    public PlanetPluto planetPlutoScript;
    public PlanetMercury planetMercuryScript;
    public PlanetSaturn planetSaturnScript;
    public PlanetMars planetMarsScript;
    public PlanetVenus planetVenusScript;

    public void Start() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = true;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;


        CurrentInfoPlanet = false;
        CurrentInfoMoon = false;
        CurrentInfoUranus = false;
        CurrentInfoNeptune = false;
        CurrentInfoJupiter = false;
        CurrentInfoPluto = false;
        CurrentInfoMercury = false;
        CurrentInfoSaturn = false;
        CurrentInfoMars = false;
        CurrentInfoVenus = false;

    }







    // Generate the earth when button is pressed
    public void GenerateTheEarth() {
        

        CurrentPlanetEarth = true;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetScript.GeneratePlanetEarth();

    }

    // Generate the Moon when button is pressed
    public void GenerateTheMoon() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = true;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetMoonScript.GenerateMoon();

    }


    // Generate Neptune when button is pressed
    public void GenerateNeptune() {
        

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = true;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetNeptuneScript.GenerateNeptune();
        
    }

    public void GenerateUranus() {
        
        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = true;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetUranusScript.GenerateUranus();

    }

    public void GenerateJupiter() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = true;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetJupiterScript.GenerateJupiter();

    }

    public void GeneratePluto() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = true;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetPlutoScript.GeneratePluto();

    }

    public void GenerateMercury() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = true;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetMercuryScript.GenerateMercury();

    }

    public void GenerateSaturn() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = true;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = false;

        // Generate planet
        planetSaturnScript.GenerateSaturn();

    }

    public void GenerateMars() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = true;
        CurrentPlanetVenus = false;

        // Generate planet
        planetMarsScript.GenerateMars();

    }

    
    public void GenerateVenus() {

        CurrentPlanetEarth = false;
        CurrentPlanetMoon = false;
        CurrentPlanetNeptune = false;
        CurrentPlanetUranus = false;
        CurrentPlanetJupiter = false;
        CurrentPlanetPluto = false;
        CurrentPlanetMercury = false;
        CurrentPlanetSaturn = false;
        CurrentPlanetMars = false;
        CurrentPlanetVenus = true;

        // Generate planet
        planetVenusScript.GenerateVenus();

    }
    






    public void Update() {
        
        if (CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetJupiter && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Moon.gameObject.SetActive(false);
            Earth.gameObject.SetActive(true);
            Neptune.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = true;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetMoon && !CurrentPlanetEarth && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetJupiter && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Moon.gameObject.SetActive(true);
            Earth.gameObject.SetActive(false);
            Neptune.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = true;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetNeptune && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetUranus && !CurrentPlanetJupiter && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(true);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = true;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
            
        }

        if (CurrentPlanetUranus && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetJupiter && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(true);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = true;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetJupiter && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(true);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = true;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetPluto && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetJupiter && !CurrentPlanetMercury && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(true);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = true;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetMercury && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetPluto && !CurrentPlanetJupiter && !CurrentPlanetSaturn && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(true);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = true;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetSaturn && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetJupiter && !CurrentPlanetMars && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(true);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = true;
            CurrentInfoMars = false;
            CurrentInfoVenus = false;
        }

        if (CurrentPlanetMars && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetJupiter && !CurrentPlanetSaturn && !CurrentPlanetVenus) { 

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(true);
            Venus.gameObject.SetActive(false);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = true;
            CurrentInfoVenus = false;
        }
        
        if (CurrentPlanetVenus && !CurrentPlanetMars && !CurrentPlanetEarth && !CurrentPlanetMoon && !CurrentPlanetNeptune && !CurrentPlanetUranus && !CurrentPlanetPluto && !CurrentPlanetMercury && !CurrentPlanetJupiter && !CurrentPlanetSaturn) {

            Neptune.gameObject.SetActive(false);
            Earth.gameObject.SetActive(false);
            Moon.gameObject.SetActive(false);
            Uranus.gameObject.SetActive(false);
            Jupiter.gameObject.SetActive(false);
            Pluto.gameObject.SetActive(false);
            Mercury.gameObject.SetActive(false);
            Saturn.gameObject.SetActive(false);
            Mars.gameObject.SetActive(false);
            Venus.gameObject.SetActive(true);

            CurrentInfoPlanet = false;
            CurrentInfoMoon = false;
            CurrentInfoUranus = false;
            CurrentInfoNeptune = false;
            CurrentInfoJupiter = false;
            CurrentInfoPluto = false;
            CurrentInfoMercury = false;
            CurrentInfoSaturn = false;
            CurrentInfoMars = false;
            CurrentInfoVenus = true;
        }
        



        if (CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoUranus && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && ! CurrentPlanetMars && !CurrentInfoVenus) {
            PlanetInfoPanel.gameObject.SetActive(true);
            MoonInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);

        }

        if (CurrentInfoMoon && !CurrentInfoPlanet && !CurrentInfoUranus && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(true);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoUranus && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(true);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoNeptune && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoUranus && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(true);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
            
        }

        if (CurrentInfoJupiter && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoUranus && !CurrentInfoNeptune && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(true);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoPluto && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoUranus && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(true);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoMercury && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoUranus && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(true);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoSaturn && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoUranus && !CurrentPlanetMars && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(true);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoMars && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetUranus && !CurrentInfoVenus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(true);
            VenusInfoPanel.gameObject.SetActive(false);
        }

        if (CurrentInfoVenus && !CurrentInfoPlanet && !CurrentInfoMoon && !CurrentInfoNeptune && !CurrentInfoJupiter && !CurrentInfoPluto && !CurrentInfoMercury && !CurrentInfoSaturn && !CurrentPlanetMars && !CurrentInfoUranus) {
            MoonInfoPanel.gameObject.SetActive(false);
            PlanetInfoPanel.gameObject.SetActive(false);
            UranusInfoPanel.gameObject.SetActive(false);
            NeptuneInfoPanel.gameObject.SetActive(false);
            JupiterInfoPanel.gameObject.SetActive(false);
            PlutoInfoPanel.gameObject.SetActive(false);
            MercuryInfoPanel.gameObject.SetActive(false);
            SaturnInfoPanel.gameObject.SetActive(false);
            MarsInfoPanel.gameObject.SetActive(false);
            VenusInfoPanel.gameObject.SetActive(true);
        }




    }
    
}
