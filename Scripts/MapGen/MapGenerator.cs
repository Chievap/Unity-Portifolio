using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap , Mesh};
	public DrawMode drawMode;

    public const int mapChunksize = 241;
    [Range(0,6)]
    public int levelOfDetail;

	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;
    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;

	public TerrainType[] regions;

	public void Start(){
		GenerateMap();
	}

	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapChunksize, mapChunksize, seed, noiseScale, octaves, persistance, lacunarity, offset);

		Color[] colourMap = new Color[mapChunksize * mapChunksize];
		for (int y = 0; y < mapChunksize; y++) {
			for (int x = 0; x < mapChunksize; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapChunksize + x] = regions [i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay> ();
		if (drawMode == DrawMode.NoiseMap) {
			display.DrawTexture (TextureGenerator.TextureFromHeightMap(noiseMap));
		} else if (drawMode == DrawMode.ColourMap) {
			display.DrawTexture (TextureGenerator.TextureFromColourMap(colourMap, mapChunksize, mapChunksize));
		} else if (drawMode == DrawMode.Mesh) {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap , meshHeightMultiplier , meshHeightCurve , levelOfDetail), TextureGenerator.TextureFromColourMap(colourMap, mapChunksize, mapChunksize));
        }
	}

	void OnValidatee() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}