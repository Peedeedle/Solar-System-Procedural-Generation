////////////////////////////////////////////////////////////
// File:                 <PlanetVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Venus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetVenus : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskVenus { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskVenus faceRenderMaskVenus;

    // shape and colour settings
    public ShapeSettingsVenus VenusshapeSettings;
    public ColourSettingsVenus VenuscolourSettings;

    [HideInInspector]
    public bool shapeSettingsVenusFoldOut;

    [HideInInspector]
    public bool colourSettingsVenusFoldOut;

    // Shape Generator
    ShapeGeneratorVenus shapeGeneratorVenus = new ShapeGeneratorVenus();

    // Colour generator
    ColourGeneratorVenus colourGeneratorVenus = new ColourGeneratorVenus();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersVenus;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceVenus[] terrainFacesVenus;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeVenus() {

        // shape generator with updated shape settings
        shapeGeneratorVenus.UpdateSettingsVenus(VenusshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorVenus.UpdateSettingsVenus(VenuscolourSettings);

        // if mesh filters initialized
        if (meshFiltersVenus == null || meshFiltersVenus.Length == 0) {

            //mesh filters = new array 6
            meshFiltersVenus = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesVenus = new TerrainFaceVenus[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersVenus[i] == null) {

                // Mesh gameobject
                GameObject VenusmeshObj = new GameObject("Venusmesh");

                // Set meshobj's parent to this transform
                VenusmeshObj.transform.parent = transform;

                // Add mesh renderer on object
                VenusmeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersVenus[i] = VenusmeshObj.AddComponent<MeshFilter>();
                meshFiltersVenus[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersVenus[i].GetComponent<MeshRenderer>().sharedMaterial = VenuscolourSettings.VenusMaterial;

            // Create terrain faces
            terrainFacesVenus[i] = new TerrainFaceVenus(shapeGeneratorVenus, meshFiltersVenus[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskVenus == FaceRenderMaskVenus.All || (int)faceRenderMaskVenus - 1 == i;
            meshFiltersVenus[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateVenus() {


        InitializeVenus();
        GenerateMeshVenus();
        GenerateColoursVenus();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedVenus() {

        if (autoUpdate){

            InitializeVenus();
            GenerateMeshVenus();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedVenus() {

        if (autoUpdate) {

            InitializeVenus();
            GenerateColoursVenus();

        }
        
    }

    // Generate Mesh
    void GenerateMeshVenus() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersVenus[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesVenus[i].ConstructMeshVenus();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorVenus.UpdateElevationVenus(shapeGeneratorVenus.elevationMinMaxVenus);

    }

    // Generate colours
    void GenerateColoursVenus() {

        // update colours
        colourGeneratorVenus.UpdateColoursVenus();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersVenus[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesVenus[i].UpdateUVsVenus(colourGeneratorVenus);

            }

        }

    }

}
