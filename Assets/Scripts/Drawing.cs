using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class Drawing {

  private static float circleRadius = 0.5f;
  private static int circleResolution = 40;

  public static GameObject CreateLine() {
    Object linePrefab = Resources.Load("line");
    Assert.IsNotNull(linePrefab);
    return (GameObject)GameObject.Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
  }

  // public static GameObject CreateLine(Vector2 firstPosition, Vector2 secondPosition) {
  //   // Create 
  //   GameObject line = CreateLine();
  //   AddOneNodeToLine(line, firstPosition);
  //   AddOneNodeToLine(line, secondPosition);
  //   return line;
  // }

  public static void AddOneNodeToLine(GameObject line, Vector2 node) {
    LineRenderer renderer = line.GetComponent<LineRenderer>();
    renderer.positionCount++;
    node = GameUtil.ToWorldUnit(node);
    renderer.SetPosition(renderer.positionCount - 1, new Vector3(node.x, node.y, -1.0f));
  }

  public static void UpdateLineByList(GameObject line, List<Vector2> nodes) {
    LineRenderer renderer = line.GetComponent<LineRenderer>();
    renderer.positionCount = nodes.Count;
    // TODO: Would it be more time efficient using SetPositions?
    for (int i = 0; i < renderer.positionCount; i++) {
      renderer.SetPosition(i, new Vector3(GameUtil.ToWorldUnit(nodes[i].x), 
                                          GameUtil.ToWorldUnit(nodes[i].y), -1.0f));
    }
  }

  public static GameObject CreateCircle() {
    GameObject circle = CreateLine();
    LineRenderer renderer = circle.GetComponent<LineRenderer>();
    renderer.useWorldSpace = false;
    renderer.positionCount = circleResolution + 1;
    for (int i = 0; i < circleResolution; i++) {
      float angle = (float)i / (float)circleResolution * 2.0f * Mathf.PI;
      renderer.SetPosition(i, new Vector3(GameUtil.ToWorldUnit(circleRadius) * Mathf.Cos(angle),
                                          GameUtil.ToWorldUnit(circleRadius) * Mathf.Sin(angle),
                                          -1.0f));
    }
    renderer.SetPosition(circleResolution, 
      new Vector3(GameUtil.ToWorldUnit(circleRadius) * Mathf.Cos(0.0f),
                  GameUtil.ToWorldUnit(circleRadius) * Mathf.Sin(0.0f),
                  -1.0f));
    return circle;
  }
}
