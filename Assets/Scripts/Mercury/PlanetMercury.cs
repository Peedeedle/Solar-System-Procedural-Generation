////////////////////////////////////////////////////////////
// File:                 <PlanetMercury.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Mercury>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMercury : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskMercury { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskMercury faceRenderMaskMercury;

    // shape and colour settings
    public ShapeSettingsMercury MercuryshapeSettings;
    public ColourSettingsMercury MercurycolourSettings;

    [HideInInspector]
    public bool shapeSettingsMercuryFoldOut;

    [HideInInspector]
    public bool colourSettingsMercuryFoldOut;

    // Shape Generator
    ShapeGeneratorMercury shapeGeneratorMercury = new ShapeGeneratorMercury();

    // Colour generator
    ColourGeneratorMercury colourGeneratorMercury = new ColourGeneratorMercury();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersMercury;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceMercury[] terrainFacesMercury;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeMercury() {

        // shape generator with updated shape settings
        shapeGeneratorMercury.UpdateSettingsMercury(MercuryshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorMercury.UpdateSettingsMercury(MercurycolourSettings);

        // if mesh filters initialized
        if (meshFiltersMercury == null || meshFiltersMercury.Length == 0) {

            //mesh filters = new array 6
            meshFiltersMercury = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesMercury = new TerrainFaceMercury[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersMercury[i] == null) {

                // Mesh gameobject
                GameObject MercurymeshObj = new GameObject("Mercurymesh");

                // Set meshobj's parent to this transform
                MercurymeshObj.transform.parent = transform;

                // Add mesh renderer on object
                MercurymeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersMercury[i] = MercurymeshObj.AddComponent<MeshFilter>();
                meshFiltersMercury[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersMercury[i].GetComponent<MeshRenderer>().sharedMaterial = MercurycolourSettings.MercuryMaterial;

            // Create terrain faces
            terrainFacesMercury[i] = new TerrainFaceMercury(shapeGeneratorMercury, meshFiltersMercury[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskMercury == FaceRenderMaskMercury.All || (int)faceRenderMaskMercury - 1 == i;
            meshFiltersMercury[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateMercury() {


        InitializeMercury();
        GenerateMeshMercury();
        GenerateColoursMercury();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedMercury() {

        if (autoUpdate){

            InitializeMercury();
            GenerateMeshMercury();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedMercury() {

        if (autoUpdate) {

            InitializeMercury();
            GenerateColoursMercury();

        }
        
    }

    // Generate Mesh
    void GenerateMeshMercury() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersMercury[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesMercury[i].ConstructMeshMercury();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorMercury.UpdateElevationMercury(shapeGeneratorMercury.elevationMinMaxMercury);

    }

    // Generate colours
    void GenerateColoursMercury() {

        // update colours
        colourGeneratorMercury.UpdateColoursMercury();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersMercury[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesMercury[i].UpdateUVsMercury(colourGeneratorMercury);

            }

        }

    }

}
