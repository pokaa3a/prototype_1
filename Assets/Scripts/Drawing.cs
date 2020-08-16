using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class Drawing {

  public static float circleRadius = 0.15f;
  public static int circleResolution = 40;

  public static GameObject CreateLine(Vector2 firstPosition, Vector2 secondPosition) {
    // Create 
    Object linePrefab = Resources.Load("line");
    Assert.IsNotNull(linePrefab);
    GameObject line = (GameObject)GameObject.Instantiate(linePrefab, Vector3.zero, Quaternion.identity);

    AddOneNodeToLine(line, firstPosition);
    AddOneNodeToLine(line, secondPosition);
    return line;
  }

  public static void AddOneNodeToLine(GameObject line, Vector2 node) {
    LineRenderer renderer = line.GetComponent<LineRenderer>();
    renderer.positionCount++;
    renderer.SetPosition(renderer.positionCount - 1, new Vector3(node.x, node.y, -1.0f));
  }

  public static void UpdateLineByList(GameObject line, List<Vector2> nodes) {
    LineRenderer renderer = line.GetComponent<LineRenderer>();
    renderer.positionCount = nodes.Count;
    // TODO: Would it be more time efficient using SetPositions?
    for (int i = 0; i < renderer.positionCount; i++) {
      renderer.SetPosition(i, new Vector3(nodes[i].x, nodes[i].y, -1.0f));
    }
  }

  public static GameObject CreateCircle(Vector2 center) {
    Object linePrefab = Resources.Load("line");
    Assert.IsNotNull(linePrefab);
    GameObject circle = (GameObject)GameObject.Instantiate(linePrefab, new Vector3(center.x, center.y, -1.0f), 
                                    Quaternion.identity);
    LineRenderer renderer = circle.GetComponent<LineRenderer>();
    renderer.useWorldSpace = false;
    renderer.positionCount = circleResolution + 1;
    for (int i = 0; i < circleResolution; i++) {
      float angle = (float)i / (float)circleResolution * 2.0f * Mathf.PI;
      renderer.SetPosition(i, new Vector3(circleRadius * Mathf.Cos(angle),
                                          circleRadius * Mathf.Sin(angle),
                                          -1.0f));
    }
    renderer.SetPosition(circleResolution, new Vector3(circleRadius * Mathf.Cos(0.0f),
                                                       circleRadius * Mathf.Sin(0.0f),
                                                       -1.0f));
    return circle;
  }
}
