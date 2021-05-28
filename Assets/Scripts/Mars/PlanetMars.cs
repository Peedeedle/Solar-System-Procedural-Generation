////////////////////////////////////////////////////////////
// File:                 <PlanetMars.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Mars>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMars : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskMars { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskMars faceRenderMaskMars;

    // shape and colour settings
    public ShapeSettingsMars MarsshapeSettings;
    public ColourSettingsMars MarscolourSettings;

    [HideInInspector]
    public bool shapeSettingsMarsFoldOut;

    [HideInInspector]
    public bool colourSettingsMarsFoldOut;

    // Shape Generator
    ShapeGeneratorMars shapeGeneratorMars = new ShapeGeneratorMars();

    // Colour generator
    ColourGeneratorMars colourGeneratorMars = new ColourGeneratorMars();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersMars;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceMars[] terrainFacesMars;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeMars() {

        // shape generator with updated shape settings
        shapeGeneratorMars.UpdateSettingsMars(MarsshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorMars.UpdateSettingsMars(MarscolourSettings);

        // if mesh filters initialized
        if (meshFiltersMars == null || meshFiltersMars.Length == 0) {

            //mesh filters = new array 6
            meshFiltersMars = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesMars = new TerrainFaceMars[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersMars[i] == null) {

                // Mesh gameobject
                GameObject MarsmeshObj = new GameObject("Marsmesh");

                // Set meshobj's parent to this transform
                MarsmeshObj.transform.parent = transform;

                // Add mesh renderer on object
                MarsmeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersMars[i] = MarsmeshObj.AddComponent<MeshFilter>();
                meshFiltersMars[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersMars[i].GetComponent<MeshRenderer>().sharedMaterial = MarscolourSettings.MarsMaterial;

            // Create terrain faces
            terrainFacesMars[i] = new TerrainFaceMars(shapeGeneratorMars, meshFiltersMars[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskMars == FaceRenderMaskMars.All || (int)faceRenderMaskMars - 1 == i;
            meshFiltersMars[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateMars() {


        InitializeMars();
        GenerateMeshMars();
        GenerateColoursMars();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedMars() {

        if (autoUpdate){

            InitializeMars();
            GenerateMeshMars();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedMars() {

        if (autoUpdate) {

            InitializeMars();
            GenerateColoursMars();

        }
        
    }

    // Generate Mesh
    void GenerateMeshMars() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersMars[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesMars[i].ConstructMeshMars();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorMars.UpdateElevationMars(shapeGeneratorMars.elevationMinMaxMars);

    }

    // Generate colours
    void GenerateColoursMars() {

        // update colours
        colourGeneratorMars.UpdateColoursMars();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersMars[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesMars[i].UpdateUVsMars(colourGeneratorMars);

            }

        }

    }

}
