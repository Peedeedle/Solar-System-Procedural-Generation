////////////////////////////////////////////////////////////
// File:                 <PlanetSaturn.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the settings onto Saturn>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <02/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSaturn : MonoBehaviour
{
    // Range for the resolution
    [Range(2, 256)]
    public int resolution = 10;
    

    // bool for autoUpdate
    public bool autoUpdate = true;

    // Face render mask for each face to individually or all render
    public enum FaceRenderMaskSaturn { All, Top, Bottom, Left, Right, Front, Back}

    // Public face render mask
    public FaceRenderMaskSaturn faceRenderMaskSaturn;

    // shape and colour settings
    public ShapeSettingsSaturn SaturnshapeSettings;
    public ColourSettingsSaturn SaturncolourSettings;

    [HideInInspector]
    public bool shapeSettingsSaturnFoldOut;

    [HideInInspector]
    public bool colourSettingsSaturnFoldOut;

    // Shape Generator
    ShapeGeneratorSaturn shapeGeneratorSaturn = new ShapeGeneratorSaturn();

    // Colour generator
    ColourGeneratorSaturn colourGeneratorSaturn = new ColourGeneratorSaturn();

    // Mesh filter array
    [SerializeField, HideInInspector]
    MeshFilter[] meshFiltersSaturn;

    // Terrain Face Array
    [SerializeField, HideInInspector]
    TerrainFaceSaturn[] terrainFacesSaturn;

    // Keep the planets position at 0, 0, 0
    public void Update() {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
    }

    // Initialize function
    void InitializeSaturn() {

        // shape generator with updated shape settings
        shapeGeneratorSaturn.UpdateSettingsSaturn(SaturnshapeSettings);

        // colour generator with updated colour settings
        colourGeneratorSaturn.UpdateSettingsSaturn(SaturncolourSettings);

        // if mesh filters initialized
        if (meshFiltersSaturn == null || meshFiltersSaturn.Length == 0) {

            //mesh filters = new array 6
            meshFiltersSaturn = new MeshFilter[6];
        }

        // array of terrain faces = 6
        terrainFacesSaturn = new TerrainFaceSaturn[6];

        // All Vector 3 Directions
        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++) {

            // if mesh filters == null create new mesh object
            if (meshFiltersSaturn[i] == null) {

                // Mesh gameobject
                GameObject SaturnmeshObj = new GameObject("Saturnmesh");

                // Set meshobj's parent to this transform
                SaturnmeshObj.transform.parent = transform;

                // Add mesh renderer on object
                SaturnmeshObj.AddComponent<MeshRenderer>();

                // mesh obj add mesh filter in mesh filters array
                meshFiltersSaturn[i] = SaturnmeshObj.AddComponent<MeshFilter>();
                meshFiltersSaturn[i].sharedMesh = new Mesh();

            }

            // Assign material to mesh
            meshFiltersSaturn[i].GetComponent<MeshRenderer>().sharedMaterial = SaturncolourSettings.SaturnMaterial;

            // Create terrain faces
            terrainFacesSaturn[i] = new TerrainFaceSaturn(shapeGeneratorSaturn, meshFiltersSaturn[i].sharedMesh, resolution, directions[i]);

            // If render face is true, render all faces
            bool renderFace = faceRenderMaskSaturn == FaceRenderMaskSaturn.All || (int)faceRenderMaskSaturn - 1 == i;
            meshFiltersSaturn[i].gameObject.SetActive(renderFace);
        }

    }

    // Generate planet (Mesh, colours)
    public void GenerateSaturn() {


        InitializeSaturn();
        GenerateMeshSaturn();
        GenerateColoursSaturn();

    }

    // When shape settings update & autoupdate is true, initialize and generate mesh
    public void OnShapeSettingsUpdatedSaturn() {

        if (autoUpdate){

            InitializeSaturn();
            GenerateMeshSaturn();

        }
        
    }

    // When colour settings update & autoupdate is true, initialize and update colours
    public void OnColourSettingsUpdatedSaturn() {

        if (autoUpdate) {

            InitializeSaturn();
            GenerateColoursSaturn();

        }
        
    }

    // Generate Mesh
    void GenerateMeshSaturn() {

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersSaturn[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesSaturn[i].ConstructMeshSaturn();

            }

        }

        // Update colour of planet based on elevation
        colourGeneratorSaturn.UpdateElevationSaturn(shapeGeneratorSaturn.elevationMinMaxSaturn);

    }

    // Generate colours
    void GenerateColoursSaturn() {

        // update colours
        colourGeneratorSaturn.UpdateColoursSaturn();

        // For each face
        for (int i = 0; i < 6; i++) {

            // If mesh filters is active
            if (meshFiltersSaturn[i].gameObject.activeSelf) {

                // Contruct terrain faces, generate mesh
                terrainFacesSaturn[i].UpdateUVsSaturn(colourGeneratorSaturn);

            }

        }

    }

}
