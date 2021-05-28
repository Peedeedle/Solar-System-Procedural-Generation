////////////////////////////////////////////////////////////
// File:                 <TerrainFacePluto.cs>
// Author:               <Jack Peedle>
// Date Created:         <30/03/2021>
// Brief:                <File responsible for allocating the terrain faces and executing generators in the game>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <01/04/2021>
// Last Edit Brief:      <Working>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFacePluto
{

    // Shape Generator
    ShapeGeneratorPluto shapeGeneratorPluto;

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
    public TerrainFacePluto(ShapeGeneratorPluto shapeGeneratorPluto, Mesh mesh, int resolution, Vector3 localUp) {

        // Shape generator
        this.shapeGeneratorPluto = shapeGeneratorPluto;

        // Set each value to itself
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

    }

    public void ConstructMeshPluto() {

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
                Vector3 pointOnUnitCubePluto = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;

                // vector 3 for point on unit sphere
                Vector3 pointOnUnitSpherePluto = pointOnUnitCubePluto.normalized;

                // Unscaled elevation = shape generator to calculate unscaled elevation with point on unit sphere
                float unscaledElevationPluto = shapeGeneratorPluto.CalculateUnscaledElevationPluto(pointOnUnitSpherePluto);

                // verrticies with index of i = point on unit sphere * shape generator with unscaled elevation
                vertices[i] = pointOnUnitSpherePluto * shapeGeneratorPluto.GetScaledElevationPluto(unscaledElevationPluto);

                // uv's on the y axis = unscaled elevation
                uv[i].y = unscaledElevationPluto;

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
    public void UpdateUVsPluto(ColourGeneratorPluto colourGeneratorPluto) {

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
                Vector3 pointOnUnitCubePluto = localUp + (percent.x - 0.5f) * 2 * axisA + (percent.y - 0.5f) * 2 * axisB;

                // vector 3 for point on unit sphere
                Vector3 pointOnUnitSpherePluto = pointOnUnitCubePluto.normalized;

                // uv with index of i on the x axis = colour generator on unit sphere 
                uv[i].x = colourGeneratorPluto.BiomePercentFromPointPluto(pointOnUnitSpherePluto);

            }
        }

        // Mesh uv is = uv
        mesh.uv = uv;

    }

}
