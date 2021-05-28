////////////////////////////////////////////////////////////
// File:                 <PlanetMoon.cs>
// Author:               <Jack Peedle>
// Date Created:         <20/03/2021>
// Brief:                <File responsible for allocating the settings onto the Moon>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <24/03/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMoon : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskMoon { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskMoon faceRenderMaskMoon;

    // shape and colour settings
    public ShapeSettingsMoon MoonshapeSettings;
    public ColourSettingsMoon MooncolourSettings;

    [HideInInspector]
    public bool shapeSettingsMoonFoldOut;

    [HideInInspector]
    public bool colourSettingsMoonFoldOut;

    // Shape Generator
    ShapeGeneratorMoon shapeGeneratorMoon = new ShapeGeneratorMoon();

    // Colour generator
    ColourGeneratorMoon colourGeneratorMoon = new ColourGeneratorMoon();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersMoon;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceMoon[] terrainFacesMoon;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeMoon() {

        // shape generator with updated shape settings
        shapeGeneratorMoon.UpdateSettingsMoon(MoonshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorMoon.UpdateSettingsMoon(MooncolourSettings);

        // if mesh filters initialized
        if (meshFiltersMoon == null || meshFiltersMoon.Length == 0) {

            //mesh filters = new array 6
            meshFiltersMoon = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesMoon = new TerrainFaceMoon[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersMoon[i] == null) {

                // Mesh gameobject
                GameObject MoonmeshObj = new GameObject("Moonmesh");

                // Set meshobj's parent to this transform
                MoonmeshObj.transform.parent = transform;

                // Add mesh renderer on object
                MoonmeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersMoon[i] = MoonmeshObj.AddComponent<MeshFilter>();
                meshFiltersMoon[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersMoon[i].GetComponent<MeshRenderer>().sharedMaterial = MooncolourSettings.MoonMaterial;

            // Create terrain faces
            terrainFacesMoon[i] = new TerrainFaceMoon(shapeGeneratorMoon, meshFiltersMoon[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskMoon == FaceRenderMaskMoon.All || (int)faceRenderMaskMoon - 1 == i;
            meshFiltersMoon[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateMoon() {


        InitializeMoon();
        GenerateMeshMoon();
        GenerateColoursMoon();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedMoon() {

        if (autoUpdate){

            InitializeMoon();
            GenerateMeshMoon();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedMoon() {

        if (autoUpdate) {

            InitializeMoon();
            GenerateColoursMoon();

        }
        
    }

    // Generate Mesh
    void GenerateMeshMoon() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersMoon[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesMoon[i].ConstructMeshMoon();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorMoon.UpdateElevationMoon(shapeGeneratorMoon.elevationMinMaxMoon);

    }

    // Generate colours
    void GenerateColoursMoon() {

        // update colours
        colourGeneratorMoon.UpdateColoursMoon();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersMoon[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesMoon[i].UpdateUVsMoon(colourGeneratorMoon);

            }

        }

    }

}
