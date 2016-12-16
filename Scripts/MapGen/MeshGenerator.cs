using UnityEngine;
using System.Collections;

public static class MeshGenerator{

	public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier , AnimationCurve heightCurve , int levelOfDetail) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        
        int meshSimplificationIncrement = (levelOfDetail == 0) ? 1: levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;
        int vertexIndex = 0;

        MeshData meshdata = new MeshData(verticesPerLine, verticesPerLine);

        for (int y = 0; y < height; y+= meshSimplificationIncrement) {
            for(int x = 0; x < width; x+= meshSimplificationIncrement) {

                meshdata.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y]) * heightMultiplier,topLeftZ - y);
                meshdata.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if(x < width - 1 && y < height-1) {
                    meshdata.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshdata.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshdata;
    }
}

public class MeshData{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWith, int meshHeight) {
        vertices = new Vector3[meshWith * meshHeight];
        uvs = new Vector2[meshWith * meshHeight];
        triangles = new int[(meshWith-1) * (meshHeight-1) * 6];
    }

    public void AddTriangle(int a , int b , int c) {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;

        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;

    }

}