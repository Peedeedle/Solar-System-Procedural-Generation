////////////////////////////////////////////////////////////
// File:                 <PlanetNeptune.cs>
// Author:               <Jack Peedle>
// Date Created:         <27/03/2021>
// Brief:                <File responsible for allocating the settings onto Neptune>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <27/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetNeptune : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskNeptune { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskNeptune faceRenderMaskNeptune;

    // shape and colour settings
    public ShapeSettingsNeptune NeptuneshapeSettings;
    public ColourSettingsNeptune NeptunecolourSettings;

    [HideInInspector]
    public bool shapeSettingsNeptuneFoldOut;

    [HideInInspector]
    public bool colourSettingsNeptuneFoldOut;

    // Shape Generator
    ShapeGeneratorNeptune shapeGeneratorNeptune = new ShapeGeneratorNeptune();

    // Colour generator
    ColourGeneratorNeptune colourGeneratorNeptune = new ColourGeneratorNeptune();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersNeptune;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceNeptune[] terrainFacesNeptune;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeNeptune() {

        // shape generator with updated shape settings
        shapeGeneratorNeptune.UpdateSettingsNeptune(NeptuneshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorNeptune.UpdateSettingsNeptune(NeptunecolourSettings);

        // if mesh filters initialized
        if (meshFiltersNeptune == null || meshFiltersNeptune.Length == 0) {

            //mesh filters = new array 6
            meshFiltersNeptune = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesNeptune = new TerrainFaceNeptune[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersNeptune[i] == null) {

                // Mesh gameobject
                GameObject NeptunemeshObj = new GameObject("Neptunemesh");

                // Set meshobj's parent to this transform
                NeptunemeshObj.transform.parent = transform;

                // Add mesh renderer on object
                NeptunemeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersNeptune[i] = NeptunemeshObj.AddComponent<MeshFilter>();
                meshFiltersNeptune[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersNeptune[i].GetComponent<MeshRenderer>().sharedMaterial = NeptunecolourSettings.NeptuneMaterial;

            // Create terrain faces
            terrainFacesNeptune[i] = new TerrainFaceNeptune(shapeGeneratorNeptune, meshFiltersNeptune[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskNeptune == FaceRenderMaskNeptune.All || (int)faceRenderMaskNeptune - 1 == i;
            meshFiltersNeptune[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateNeptune() {

        Debug.Log("Generated Neptune");

        InitializeNeptune();
        GenerateMeshNeptune();
        GenerateColoursNeptune();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedNeptune() {

        if (autoUpdate){

            InitializeNeptune();
            GenerateMeshNeptune();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedNeptune() {

        if (autoUpdate) {

            InitializeNeptune();
            GenerateColoursNeptune();

        }
        
    }

    // Generate Mesh
    void GenerateMeshNeptune() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersNeptune[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesNeptune[i].ConstructMeshNeptune();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorNeptune.UpdateElevationNeptune(shapeGeneratorNeptune.elevationMinMaxNeptune);

    }

    // Generate colours
    void GenerateColoursNeptune() {

        // update colours
        colourGeneratorNeptune.UpdateColoursNeptune();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersNeptune[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesNeptune[i].UpdateUVsNeptune(colourGeneratorNeptune);

            }

        }

    }

}
