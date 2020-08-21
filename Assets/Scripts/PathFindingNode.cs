using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingNode {
  public int x;
  public int y;
  public PathFindingNode cameFrom;

  public int gCost;   // Cost to move from the starting point
  public int hCost;   // Cost to move to the target point
  public int fCost;

  public PathFindingNode(int x, int y) {
    this.x = x;
    this.y = y;
    this.gCost = int.MaxValue;
    this.hCost = 0;
    this.cameFrom = null;
    CalculateFCost();
  }

  public PathFindingNode(MapNode mapNode) {
    this.x = mapNode.x();
    this.y = mapNode.y();
    this.gCost = int.MaxValue;
    this.hCost = 0;
    this.cameFrom = null;
    CalculateFCost();
  }

  public void CalculateFCost() {
    fCost = gCost + hCost;
  }
}
