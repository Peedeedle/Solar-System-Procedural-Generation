////////////////////////////////////////////////////////////
// File:                 <Planet.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/02/2021>
// Brief:                <File responsible for allocating the settings onto the planet>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;

    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMask faceRenderMask;

    // shape and colour settings
    public ShapeSettings shapeSettings;
    public ColourSettings colourSettings;

    [HideInInspector]
    public bool shapeSettingsFoldOut;

    [HideInInspector]
    public bool colourSettingsFoldOut;

    // Shape Generator
    ShapeGenerator shapeGenerator = new ShapeGenerator();

    // Colour generator
    ColourGenerator colourGenerator = new ColourGenerator();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFace[] terrainFaces;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeEarth() {

        // shape generator with updated shape settings
        shapeGenerator.UpdateSettings(shapeSettings);

        // colour generator with updated colour settings
        colourGenerator.UpdateSettings(colourSettings);

        // if mesh filters initialized
        if (meshFilters == null || meshFilters.Length == 0) {

            //mesh filters = new array 6
            meshFilters = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFaces = new TerrainFace[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFilters[i] == null) {

                // Mesh gameobject
                GameObject meshObj = new GameObject("mesh");

                // Set meshobj's parent to this transform
                meshObj.transform.parent = transform;

                // Add mesh renderer on object
                meshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colourSettings.planetMaterial;

            // Create terrain faces
            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GeneratePlanetEarth() {


        InitializeEarth();
        GenerateMeshEarth();
        GenerateColoursEarth();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedEarth() {

        if (autoUpdate){

            InitializeEarth();
            GenerateMeshEarth();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedEarth() {

        if (autoUpdate) {

            InitializeEarth();
            GenerateColoursEarth();

        }
        
    }

    // Generate Mesh
    void GenerateMeshEarth() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFilters[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFaces[i].ConstructMesh();

            }

        }

        // Update colour of planet based on elevation
        colourGenerator.UpdateElevation(shapeGenerator.elevationMinMax);

    }

    // Generate colours
    void GenerateColoursEarth() {

        // update colours
        colourGenerator.UpdateColours();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFilters[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFaces[i].UpdateUVs(colourGenerator);

            }

        }

    }





   

}
