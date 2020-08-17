using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Level : MonoBehaviour {

  private bool showGrid = true;
  private float maxH = 20.0f;
  private float minH = -20.0f;
  private float maxW = 12.0f;
  private float minW = -12.0f;

  void Start() {
    if (showGrid) {
      displayGrid();
    }
  }
  void displayGrid() {
    Object linePrefab = Resources.Load("gridLine");
    Assert.IsNotNull(linePrefab);
      
    // Horizontal lines
    for (float h = minH - 0.5f; h <= maxH + 0.5f; h += 1.0f) {
      GameObject line = (GameObject)GameObject.Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
      Drawing.AddOneNodeToLine(line, new Vector2(minW, h));
      Drawing.AddOneNodeToLine(line, new Vector2(maxW, h));
    }
    // Vertical lines
    for (float w = minW - 0.5f; w <= maxW + 0.5f; w += 1.0f) {
      GameObject line = (GameObject)GameObject.Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
      Drawing.AddOneNodeToLine(line, new Vector2(w, minH));
      Drawing.AddOneNodeToLine(line, new Vector2(w, maxH));
    }
  }
}
