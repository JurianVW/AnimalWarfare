using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	public int gridWidth, gridHeight;
	public float tileOffset, tileHeight;
	public Tile prefab;
	private Tile[, ] tiles;

	void Start () {
		tiles = new Tile[gridWidth, gridHeight];
		for (int i = 0; i < gridWidth; i++) {
			for (int j = 0; j < gridHeight; j++) {
				Tile tile = Instantiate (prefab, new Vector3 ((i * 10) * (prefab.transform.localScale.x + tileOffset), tileHeight, (j * 10) *(prefab.transform.localScale.z + tileOffset)), Quaternion.identity);
			//	tile.setLocation (i, j);
				tile.transform.SetParent (this.transform);
				tiles[i, j] = tile;
			}
		}
		Vector3 tilePosition = tiles[gridWidth - 1, gridHeight - 1].transform.position;
		this.transform.position = new Vector3(-(tilePosition.x / 2), 0, -(tilePosition.z / 2));
	}
}