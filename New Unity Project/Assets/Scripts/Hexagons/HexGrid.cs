using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/** 
 * Our big fancy grid of hex cells
 * Author: Evan
 */
public class HexGrid : MonoBehaviour {

	public int width = 6;
	public int height = 6;

	public HexCell cellPrefab;

	// Single mesh that draws all hexagons.
	HexMesh hexMesh;

	HexCell[] cells;

	public Text cellLabelPrefab;

	Canvas gridCanvas;

	void Awake() {
		gridCanvas = GetComponentInChildren<Canvas> ();
		hexMesh = GetComponentInChildren<HexMesh> ();

		cells = new HexCell[height * width];

		for (int z = 0, i = 0; z < height; z++) {
			for (int x = 0; x < width; x++) {
				CreateCell(x, z, i++);
			}
		}
	}

	void Start() {
		hexMesh.Triangulate (cells);
	}

	/**
	 * Creates a cell.
	 * Yeah that's some real good documentation
	 */
	void CreateCell (int x, int z, int i) {
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent (transform, false);
		cell.transform.localPosition = position;

		Text label = Instantiate<Text> (cellLabelPrefab);
		label.rectTransform.SetParent (gridCanvas.transform, false);
		label.rectTransform.anchoredPosition =
			new Vector2 (position.x, position.z);
		label.text = x.ToString () + "\n" + z.ToString ();
	}
}
