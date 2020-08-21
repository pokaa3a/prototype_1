using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class MapHandler {

  private static MapNode[,] gridArray;
  private static PathFinding pathFinding;

  static MapHandler() {
    pathFinding = new PathFinding();
    // Initialize the grid
    int mapW = GameUtil.maxX - GameUtil.minX + 1;
    int mapH = GameUtil.maxY - GameUtil.minY + 1;
    int numNodes = mapW * mapH;
    gridArray = new MapNode[mapW, mapH];
    for (int x = GameUtil.minX; x <= GameUtil.maxX; x++) {
      for (int y = GameUtil.minY; y <= GameUtil.maxY; y++) {
        // TODO: Chekc if it's a wall
        bool isWall = false;
        gridArray[x - GameUtil.minX, y - GameUtil.minY] = new MapNode(x, y, isWall);
      }
    }
  }

  public static List<Vector2> FindPath(Vector2Int startNode, Vector2Int endNode) {
    return FindPath(GetMapNode(startNode), GetMapNode(endNode));
  }

  public static List<Vector2> FindPath(MapNode startNode, MapNode endNode) {
    Assert.IsFalse(startNode.isWall());
    Assert.IsFalse(startNode.isWall());
    List<PathFindingNode> pathNodes = pathFinding.FindPath(startNode, endNode);
    List<Vector2> outputPath = new List<Vector2>();
    // TODO: Smooth the path
    foreach (PathFindingNode node in pathNodes) {
      outputPath.Add(new Vector2(node.x, node.y));
    }
    return outputPath;
  }

  public static MapNode GetMapNode(PathFindingNode node) {
    return GetMapNode(node.x, node.y);
  }

  public static MapNode GetMapNode(Vector2Int pos) {
    return GetMapNode(pos.x, pos.y);
  }

  public static MapNode GetMapNode(int x, int y) {
    if (x >= GameUtil.minX && x <= GameUtil.maxX &&
        y >= GameUtil.minY && y <= GameUtil.maxY) {
      Assert.IsNotNull(gridArray[x - GameUtil.minX, y - GameUtil.minY]);
      return gridArray[x - GameUtil.minX, y - GameUtil.minY];
    }
    return null;
  }
  public static MapNode GetMapNodeFromWorld(Vector3 pos) {
    return GetMapNode(GameUtil.ToGameUnitInt(pos.x), GameUtil.ToGameUnitInt(pos.y));
  }

}
