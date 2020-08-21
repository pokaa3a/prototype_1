using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MovingPath {
  public List<Vector2> pathNodes;
  public GameObject pathLine;
  public GameObject pathEndCircle;

  public MovingPath() {
    pathNodes = new List<Vector2>();
  }

  public void MoveAlongPath(Character character, float distanceToMove) {
    float distanceMoved = 0.0f;
    while (pathNodes.Count > 1 && distanceMoved < distanceToMove) {
      // |------|     (d = distance between nodes)
      // |--------->  Case: A (d < distanceToMove - distanceMoved)
      // |--->        Case: B (d > distanceToMove - distanceMoved)
      if (Vector2.Distance(pathNodes[0], pathNodes[1]) < distanceToMove - distanceMoved) {
        // Case A
        distanceMoved += Vector2.Distance(pathNodes[0], pathNodes[1]);
        character.MoveTo(pathNodes[1]);
        pathNodes.RemoveAt(0);
      } else {
        // Case B
        float t = (distanceToMove - distanceMoved)
                    / Vector2.Distance(pathNodes[0], pathNodes[1]);
        // Debug.Log("Node 0: " + pathNodes[0].x + ", " + pathNodes[0].y);
        // Debug.Log("Node 1: " + pathNodes[1].x + ", " + pathNodes[1].y);
        // Debug.Log("t = " + t);
        Vector2 endPoint = Vector2.Lerp(pathNodes[0], pathNodes[1], t);
        distanceMoved = distanceToMove;
        character.MoveTo(endPoint);
        pathNodes[0] = endPoint;
      }
    }
    if (pathNodes.Count < 2) {
      Reset();
    }
    Drawing.UpdateLineByList(pathLine, pathNodes);
  }

  public void SetPath(List<Vector2> path) {
    pathNodes = path;
    pathLine = Drawing.CreateLine();
    Drawing.UpdateLineByList(pathLine, pathNodes);
  }

  public void SetEndCircle(MapNode node) {
    pathEndCircle = Drawing.CreateCircle();
    MoveEndCircle(node);
  }

  public void MoveEndCircle(MapNode node) {
    pathEndCircle.transform.position = GameUtil.ToWorldUnit(node);
  }

  public void Reset() {
    pathNodes.Clear();
    if (pathLine != null) {
      Assert.IsNotNull(pathEndCircle);
      Object.Destroy(pathLine);
      Object.Destroy(pathEndCircle);
    }
  }
}
