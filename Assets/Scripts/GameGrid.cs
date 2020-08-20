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

  public GameGrid(int maxX, int minX, int maxY, int minY, 
                  Func<GameGrid, int, int, PathNode> createNode) {
    this.maxX = maxX;
    this.minX = minX;
    this.maxY = maxY;
    this.minY = minY;
    W = maxX - minX + 1;
    H = maxY - minY + 1;

    gridArray = new PathNode[W, H];
    for (int x = minX; x <= maxX; x++) {
      for (int y = minY; y <= maxY; y++) {
        gridArray[x - minX, y - minY] = createNode(this, x, y);
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
    if (x >= minX && x <= maxX && y >= minY && y <= maxY) {
      return gridArray[x - minX, y - minY];
    } else {
      return null;
    }
  }

}
