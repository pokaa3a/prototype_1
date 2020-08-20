using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode {

  public GameGrid grid;
  public int x;
  public int y;
  public PathNode cameFrom;
  public bool isWall;

  public int gCost;   // Cost to move from the starting point
  public int hCost;   // Cost to move to the target point
  public int fCost;

  public PathNode(GameGrid grid, int x, int y, bool isWall) {
    this.grid = grid;
    this.x = x;
    this.y = y;
    this.isWall = isWall;
  }

  public void CalculateFCost() {
    fCost = gCost + hCost;
  }
}
