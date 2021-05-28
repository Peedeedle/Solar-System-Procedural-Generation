////////////////////////////////////////////////////////////
// File:                 <PlanetJupiter.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Jupiter>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetJupiter : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskJupiter { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskJupiter faceRenderMaskJupiter;

    // shape and colour settings
    public ShapeSettingsJupiter JupitershapeSettings;
    public ColourSettingsJupiter JupitercolourSettings;

    [HideInInspector]
    public bool shapeSettingsJupiterFoldOut;

    [HideInInspector]
    public bool colourSettingsJupiterFoldOut;

    // Shape Generator
    ShapeGeneratorJupiter shapeGeneratorJupiter = new ShapeGeneratorJupiter();

    // Colour generator
    ColourGeneratorJupiter colourGeneratorJupiter = new ColourGeneratorJupiter();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersJupiter;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceJupiter[] terrainFacesJupiter;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeJupiter() {

        // shape generator with updated shape settings
        shapeGeneratorJupiter.UpdateSettingsJupiter(JupitershapeSettings);

        // colour generator with updated colour settings
        colourGeneratorJupiter.UpdateSettingsJupiter(JupitercolourSettings);

        // if mesh filters initialized
        if (meshFiltersJupiter == null || meshFiltersJupiter.Length == 0) {

            //mesh filters = new array 6
            meshFiltersJupiter = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesJupiter = new TerrainFaceJupiter[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersJupiter[i] == null) {

                // Mesh gameobject
                GameObject JupitermeshObj = new GameObject("Jupitermesh");

                // Set meshobj's parent to this transform
                JupitermeshObj.transform.parent = transform;

                // Add mesh renderer on object
                JupitermeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersJupiter[i] = JupitermeshObj.AddComponent<MeshFilter>();
                meshFiltersJupiter[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersJupiter[i].GetComponent<MeshRenderer>().sharedMaterial = JupitercolourSettings.JupiterMaterial;

            // Create terrain faces
            terrainFacesJupiter[i] = new TerrainFaceJupiter(shapeGeneratorJupiter, meshFiltersJupiter[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskJupiter == FaceRenderMaskJupiter.All || (int)faceRenderMaskJupiter - 1 == i;
            meshFiltersJupiter[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateJupiter() {


        InitializeJupiter();
        GenerateMeshJupiter();
        GenerateColoursJupiter();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedJupiter() {

        if (autoUpdate){

            InitializeJupiter();
            GenerateMeshJupiter();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedJupiter() {

        if (autoUpdate) {

            InitializeJupiter();
            GenerateColoursJupiter();

        }
        
    }

    // Generate Mesh
    void GenerateMeshJupiter() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersJupiter[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesJupiter[i].ConstructMeshJupiter();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorJupiter.UpdateElevationJupiter(shapeGeneratorJupiter.elevationMinMaxJupiter);

    }

    // Generate colours
    void GenerateColoursJupiter() {

        // update colours
        colourGeneratorJupiter.UpdateColoursJupiter();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersJupiter[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesJupiter[i].UpdateUVsJupiter(colourGeneratorJupiter);

            }

        }

    }

}
