////////////////////////////////////////////////////////////
// File:                 <PlanetUranus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Uranus>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <30/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetUranus : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskUranus { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskUranus faceRenderMaskUranus;

    // shape and colour settings
    public ShapeSettingsUranus UranusshapeSettings;
    public ColourSettingsUranus UranuscolourSettings;

    [HideInInspector]
    public bool shapeSettingsUranusFoldOut;

    [HideInInspector]
    public bool colourSettingsUranusFoldOut;

    // Shape Generator
    ShapeGeneratorUranus shapeGeneratorUranus = new ShapeGeneratorUranus();

    // Colour generator
    ColourGeneratorUranus colourGeneratorUranus = new ColourGeneratorUranus();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersUranus;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceUranus[] terrainFacesUranus;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeUranus() {

        // shape generator with updated shape settings
        shapeGeneratorUranus.UpdateSettingsUranus(UranusshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorUranus.UpdateSettingsUranus(UranuscolourSettings);

        // if mesh filters initialized
        if (meshFiltersUranus == null || meshFiltersUranus.Length == 0) {

            //mesh filters = new array 6
            meshFiltersUranus = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesUranus = new TerrainFaceUranus[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersUranus[i] == null) {

                // Mesh gameobject
                GameObject UranusmeshObj = new GameObject("Uranusmesh");

                // Set meshobj's parent to this transform
                UranusmeshObj.transform.parent = transform;

                // Add mesh renderer on object
                UranusmeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersUranus[i] = UranusmeshObj.AddComponent<MeshFilter>();
                meshFiltersUranus[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersUranus[i].GetComponent<MeshRenderer>().sharedMaterial = UranuscolourSettings.UranusMaterial;

            // Create terrain faces
            terrainFacesUranus[i] = new TerrainFaceUranus(shapeGeneratorUranus, meshFiltersUranus[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskUranus == FaceRenderMaskUranus.All || (int)faceRenderMaskUranus - 1 == i;
            meshFiltersUranus[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateUranus() {


        InitializeUranus();
        GenerateMeshUranus();
        GenerateColoursUranus();

        Debug.Log("Generated and all Uranus");
    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedUranus() {

        if (autoUpdate){

            InitializeUranus();
            GenerateMeshUranus();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedUranus() {

        if (autoUpdate) {

            InitializeUranus();
            GenerateColoursUranus();

        }
        
    }

    // Generate Mesh
    void GenerateMeshUranus() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersUranus[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesUranus[i].ConstructMeshUranus();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorUranus.UpdateElevationUranus(shapeGeneratorUranus.elevationMinMaxUranus);

    }

    // Generate colours
    void GenerateColoursUranus() {

        // update colours
        colourGeneratorUranus.UpdateColoursUranus();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersUranus[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesUranus[i].UpdateUVsUranus(colourGeneratorUranus);

            }

        }

    }

}
