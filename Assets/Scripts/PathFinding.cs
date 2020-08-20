using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PathFinding {

  private const int MOVE_STRAIGHT_COST = 10;
  private const int MOVE_DIAGONAL_COST = 14;

  private GameGrid grid;
  private List<PathNode> openList;
  private List<PathNode> closedList;

  public PathFinding(int maxX, int minX, int maxY, int minY) {
    grid = new GameGrid(maxX, minX, maxY, minY, 
                        (GameGrid g, int x, int y, bool isWall) => new PathNode(g, x, y, isWall));
  }

  public List<Vector2> FindPath(int srcX, int srcY, int dstX, int dstY) {
    PathNode startNode = grid.GetGridNode(srcX, srcY);
    PathNode endNode = grid.GetGridNode(dstX, dstY);

    Assert.IsNotNull(startNode);
    Assert.IsNotNull(endNode);

    openList = new List<PathNode>{startNode};
    closedList = new List<PathNode>();
    grid.InitializeNodes();

    startNode.gCost = 0;
    startNode.hCost = CalculateDistanceCost(startNode, endNode);
    startNode.CalculateFCost();

    while (openList.Count > 0) {
      PathNode curNode = GetLowestFCostNode(openList);
      if (curNode == endNode) {
        return CalculatePath(endNode);
      }
      openList.Remove(curNode);
      closedList.Add(curNode);
    
      foreach (PathNode neighborNode in GetNeighborList(curNode)) {
        if (closedList.Contains(neighborNode)) continue;

        int tentativeCost = curNode.gCost + CalculateDistanceCost(curNode, neighborNode);
        if (tentativeCost < neighborNode.gCost) {
          neighborNode.cameFrom = curNode;
          neighborNode.gCost = tentativeCost;
          neighborNode.hCost = CalculateDistanceCost(neighborNode, endNode);
          neighborNode.CalculateFCost();

          if (!openList.Contains(neighborNode)) {
            openList.Add(neighborNode);
          }
        }
      }
    }
    return null;
  }

  private List<Vector2> CalculatePath(PathNode endNode) {
    // List<PathNode> path = new List<PathNode>();
    List<Vector2> path = new List<Vector2>();
    // path.Add(endNode);
    path.Add(new Vector2(endNode.x, endNode.y));

    PathNode curNode = endNode;
    while (curNode.cameFrom != null) {
      // path.Add(curNode.cameFrom);
      path.Add(new Vector2(curNode.x, curNode.y));

      curNode = curNode.cameFrom;
    }
    path.Reverse();
    return path;
  }

  private List<PathNode> GetNeighborList(PathNode curNode) {
    List<PathNode> neighborList = new List<PathNode>();

    for (int x = -1; x <= 1; x++) {
      for (int y = -1; y <= 1; y++) {
        if (x == 0 && y == 0) continue;
        PathNode neighborNode = grid.GetGridNode(curNode.x + x, curNode.y + y);
        if (neighborNode != null) {
          neighborList.Add(neighborNode);
        }
      }
    }
    return neighborList;
  }

  private PathNode GetLowestFCostNode(List<PathNode> nodeList) {
     PathNode lowestFCostNode = nodeList[0];
     foreach (PathNode curNode in nodeList) {
       if (curNode.fCost < lowestFCostNode.fCost) {
         lowestFCostNode = curNode;
       }
     }
     return lowestFCostNode;
  }

  private int CalculateDistanceCost(PathNode node1, PathNode node2) {
    int xDistance = Mathf.Abs(node1.x - node2.x);
    int yDistance = Mathf.Abs(node1.y - node2.y);
    int remaining = Mathf.Abs(xDistance - yDistance);
    return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
  }

}
