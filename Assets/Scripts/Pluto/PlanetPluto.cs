////////////////////////////////////////////////////////////
// File:                 <PlanetPluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Pluto>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPluto : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskPluto { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskPluto faceRenderMaskPluto;

    // shape and colour settings
    public ShapeSettingsPluto PlutoshapeSettings;
    public ColourSettingsPluto PlutocolourSettings;

    [HideInInspector]
    public bool shapeSettingsPlutoFoldOut;

    [HideInInspector]
    public bool colourSettingsPlutoFoldOut;

    // Shape Generator
    ShapeGeneratorPluto shapeGeneratorPluto = new ShapeGeneratorPluto();

    // Colour generator
    ColourGeneratorPluto colourGeneratorPluto = new ColourGeneratorPluto();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersPluto;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFacePluto[] terrainFacesPluto;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializePluto() {

        // shape generator with updated shape settings
        shapeGeneratorPluto.UpdateSettingsPluto(PlutoshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorPluto.UpdateSettingsPluto(PlutocolourSettings);

        // if mesh filters initialized
        if (meshFiltersPluto == null || meshFiltersPluto.Length == 0) {

            //mesh filters = new array 6
            meshFiltersPluto = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesPluto = new TerrainFacePluto[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersPluto[i] == null) {

                // Mesh gameobject
                GameObject PlutomeshObj = new GameObject("Plutomesh");

                // Set meshobj's parent to this transform
                PlutomeshObj.transform.parent = transform;

                // Add mesh renderer on object
                PlutomeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersPluto[i] = PlutomeshObj.AddComponent<MeshFilter>();
                meshFiltersPluto[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersPluto[i].GetComponent<MeshRenderer>().sharedMaterial = PlutocolourSettings.PlutoMaterial;

            // Create terrain faces
            terrainFacesPluto[i] = new TerrainFacePluto(shapeGeneratorPluto, meshFiltersPluto[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskPluto == FaceRenderMaskPluto.All || (int)faceRenderMaskPluto - 1 == i;
            meshFiltersPluto[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GeneratePluto() {


        InitializePluto();
        GenerateMeshPluto();
        GenerateColoursPluto();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedPluto() {

        if (autoUpdate){

            InitializePluto();
            GenerateMeshPluto();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedPluto() {

        if (autoUpdate) {

            InitializePluto();
            GenerateColoursPluto();

        }
        
    }

    // Generate Mesh
    void GenerateMeshPluto() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersPluto[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesPluto[i].ConstructMeshPluto();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorPluto.UpdateElevationPluto(shapeGeneratorPluto.elevationMinMaxPluto);

    }

    // Generate colours
    void GenerateColoursPluto() {

        // update colours
        colourGeneratorPluto.UpdateColoursPluto();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersPluto[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesPluto[i].UpdateUVsPluto(colourGeneratorPluto);

            }

        }

    }

}
