using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid {

  private int maxX;
  private int minX;
  private int maxY;
  private int minY;
  private int H;
  private int W;
  private PathNode[,] gridArray;

  private HashSet<Vector2Int> walls;

  private void InitializeWalls() {
    UnityEngine.Object wallPrefab = Resources.Load("wall");
    walls = new HashSet<Vector2Int>();
    int x, y;
    // (3, -2) ... (3, 3)
    for (x = 3, y = -2; y <= 3; y++) {
      walls.Add(new Vector2Int(x, y));
      GameObject.Instantiate(wallPrefab, new Vector3(GameUtilities.GameToWorldUnit((float)x),
        GameUtilities.GameToWorldUnit((float)y), -1.0f), Quaternion.identity);
    }
    // (0, 3) ... (0, 8)
    for (x = 0, y = 3; y <= 8; y++) {
      walls.Add(new Vector2Int(x, y));
      GameObject.Instantiate(wallPrefab, new Vector3(GameUtilities.GameToWorldUnit((float)x),
        GameUtilities.GameToWorldUnit((float)y), -1.0f), Quaternion.identity);
    }
    // (-5, -6) ... (-5, -1)
    for (x = -5, y = -6; y <= -1; y++) {
      walls.Add(new Vector2Int(x, y));
      GameObject.Instantiate(wallPrefab, new Vector3(GameUtilities.GameToWorldUnit((float)x),
        GameUtilities.GameToWorldUnit((float)y), -1.0f), Quaternion.identity);
    }
    // (-7, 0) ... (-2, 0)
    for (x = -7, y = 0; x <= -2; x++) {
      walls.Add(new Vector2Int(x, y));
      GameObject.Instantiate(wallPrefab, new Vector3(GameUtilities.GameToWorldUnit((float)x),
        GameUtilities.GameToWorldUnit((float)y), -1.0f), Quaternion.identity);
    }
    // (-2, -5) ... (3, -5)
    for (x = -2, y = -5; x <= 3; x++) {
      walls.Add(new Vector2Int(x, y));
      GameObject.Instantiate(wallPrefab, new Vector3(GameUtilities.GameToWorldUnit((float)x),
        GameUtilities.GameToWorldUnit((float)y), -1.0f), Quaternion.identity);
    }
    // (1, 8) ... (5, 8)
    for (x = 1, y = 8; x <= 5; x++) {
      walls.Add(new Vector2Int(x, y));
      GameObject.Instantiate(wallPrefab, new Vector3(GameUtilities.GameToWorldUnit((float)x),
        GameUtilities.GameToWorldUnit((float)y), -1.0f), Quaternion.identity);
    }
  }

  public GameGrid(int maxX, int minX, int maxY, int minY, 
                  Func<GameGrid, int, int, bool, PathNode> createNode) {
    this.maxX = maxX;
    this.minX = minX;
    this.maxY = maxY;
    this.minY = minY;
    W = maxX - minX + 1;
    H = maxY - minY + 1;

    InitializeWalls();
    gridArray = new PathNode[W, H];
    for (int x = minX; x <= maxX; x++) {
      for (int y = minY; y <= maxY; y++) {
        if (walls.Contains(new Vector2Int(x, y))) {
          gridArray[x - minX, y - minY] = createNode(this, x, y, true);
        } else {
          gridArray[x - minX, y - minY] = createNode(this, x, y, false);
        }
      }
    }
  }

  public void InitializeNodes() {
    for (int x = minX; x <= maxX; x++) {
      for (int y = minY; y <= maxY; y++) {
        gridArray[x - minX, y - minY].gCost = int.MaxValue;
        gridArray[x - minX, y - minY].CalculateFCost();
        gridArray[x - minX, y - minY].cameFrom = null;
      }
    }
  }

  public PathNode GetGridNode(int x, int y) {
    if (x >= minX && x <= maxX && y >= minY && y <= maxY &&
        !walls.Contains(new Vector2Int(x, y))) {
      return gridArray[x - minX, y - minY];
    } else {
      return null;
    }
  }

}
