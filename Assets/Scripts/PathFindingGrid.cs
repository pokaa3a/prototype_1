using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingGrid {

  private PathFindingNode[,] grid;

  public PathFindingGrid() {
    int mapW = GameUtil.maxX - GameUtil.minX + 1;
    int mapH = GameUtil.maxY - GameUtil.minY + 1;
    grid = new PathFindingNode[mapW, mapH];
  }

  public PathFindingNode GetGridNode(MapNode node) {
    return GetGridNode(node.x(), node.y());
  }

  public PathFindingNode GetGridNode(int x, int y) {
    if (x >= GameUtil.minX && x <= GameUtil.maxX &&
        y >= GameUtil.minY && y <= GameUtil.maxY) {
      if (grid[x - GameUtil.minX, y - GameUtil.minY] == null) {
        grid[x - GameUtil.minX, y - GameUtil.minY] = new PathFindingNode(x, y);
      }
      return grid[x - GameUtil.minX, y - GameUtil.minY];
    }
    return null;
  }
}
