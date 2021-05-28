﻿////////////////////////////////////////////////////////////
// File:                 <TerrainFaceVenus.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the terrain faces and executing generators in the game>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <05/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFaceVenus {

    // Shape Generator
    ShapeGeneratorVenus shapeGeneratorVenus;

    // Mesh
    Mesh mesh;

    // Resolution
    int resolution;

    // Local up
    Vector3 localUp;

    // Axis A and B
    Vector3 axisA;
    Vector3 axisB;

    // Generate terrain face (These variables)
    public TerrainFaceVenus(ShapeGeneratorVenus shapeGeneratorVenus, Mesh mesh, int resolution, Vector3 localUp) {

        // Shape generator
        this.shapeGeneratorVenus = shapeGeneratorVenus;

        // Set each value to itself
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

    }

    public void ConstructMeshVenus() {

        // Total number of vertices
        Vector3[] vertices = new Vector3[resolution * resolution];

        // How many triangles are in the mesh
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];

        // trinagle index
        int triIndex = 0;

        // if mesh.uv.length is = verticies length, if not set to new array that has the same length as the verticies array
        Vector2[] uv = (mesh.uv.Length == vertices.Length) ? mesh.uv : new Vector2[vertices.Length];

        // for y is less than resolution
        for (int y = 0; y < resolution; y++) {

            // for x is less than resolution
            for (int x = 0; x < resolution; x++) {

                // Resolution of sphere
                int i = x + y * resolution;

                // How close to complete are the two above for loops
                Vector2 percent = new Vector2(x, y) / (resolution - 1);

                // find point on cube by using how far you are from each axis
                Vector3 pointOnUnitCubeVenus = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;

                // vector 3 for point on unit sphere
                Vector3 pointOnUnitSphereVenus = pointOnUnitCubeVenus.normalized;

                // Unscaled elevation = shape generator to calculate unscaled elevation with point on unit sphere
                float unscaledElevationVenus = shapeGeneratorVenus.CalculateUnscaledElevationVenus(pointOnUnitSphereVenus);

                // verrticies with index of i = point on unit sphere * shape generator with unscaled elevation
                vertices[i] = pointOnUnitSphereVenus * shapeGeneratorVenus.GetScaledElevationVenus(unscaledElevationVenus);

                // uv's on the y axis = unscaled elevation
                uv[i].y = unscaledElevationVenus;

                if (x != resolution - 1 && y != resolution - 1) {

                    // First triangle (Vertices)
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;

                    // Second triangle (Vertices)
                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    // 6 Vertices = 1 square (2 Triangles)
                    triIndex += 6;

                }

            }

        }

        // Clear mesh data
        mesh.Clear();

        // Assign the meshes triangles and vertices
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Reassign uv mesh
        mesh.uv = uv;

    }

    // Update UV's
    public void UpdateUVsVenus(ColourGeneratorVenus colourGeneratorVenus) {

        // UV vector 2 = mesh.uv
        Vector2[] uv = mesh.uv;

        // for y is less than resolution
        for (int y = 0; y < resolution; y++) {

            // for x is less than resolution
            for (int x = 0; x < resolution; x++) {

                // Resolution of sphere
                int i = x + y * resolution;

                // How close to complete are the two above for loops
                Vector2 percent = new Vector2(x, y) / (resolution - 1);

                // find point on cube by using how far you are from each axis
                Vector3 pointOnUnitCubeVenus = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;

                // vector 3 for point on unit sphere
                Vector3 pointOnUnitSphereVenus = pointOnUnitCubeVenus.normalized;

                // uv with index of i on the x axis = colour generator on unit sphere 
                uv[i].x = colourGeneratorVenus.BiomePercentFromPointVenus(pointOnUnitSphereVenus);

            }
        }

        // Mesh uv is = uv
        mesh.uv = uv;

    }

}
