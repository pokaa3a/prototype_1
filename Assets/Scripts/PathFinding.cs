using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PathFinding {

  private const int MOVE_STRAIGHT_COST = 10;
  private const int MOVE_DIAGONAL_COST = 14;

  private List<PathFindingNode> openList;
  private List<PathFindingNode> closedList;

  public PathFinding() {

  }

  public List<PathFindingNode> FindPath(MapNode startMapNode, MapNode endMapNode) {
    PathFindingGrid pathFindingGrid = new PathFindingGrid();
    PathFindingNode startNode = pathFindingGrid.GetGridNode(startMapNode);
    PathFindingNode endNode = pathFindingGrid.GetGridNode(endMapNode);

    openList = new List<PathFindingNode>{startNode};
    closedList = new List<PathFindingNode>();

    startNode.gCost = 0;
    startNode.hCost = CalculateDistanceCost(startNode, endNode);
    startNode.CalculateFCost();

    while (openList.Count > 0) {
      PathFindingNode curNode = GetLowestFCostNode(openList);
      if (curNode == endNode) {
        return CalculatePath(endNode);
      }
      openList.Remove(curNode);
      closedList.Add(curNode);
    
      foreach (PathFindingNode neighborNode in GetNeighborList(pathFindingGrid, curNode)) {
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

  private List<PathFindingNode> CalculatePath(PathFindingNode endNode) {
    List<PathFindingNode> path = new List<PathFindingNode>();
    path.Add(endNode);

    PathFindingNode curNode = endNode;
    while (curNode.cameFrom != null) {
      path.Add(curNode.cameFrom);
      curNode = curNode.cameFrom;
    }
    path.Reverse();
    return path;
  }

  private List<PathFindingNode> GetNeighborList(PathFindingGrid grid, PathFindingNode curNode) {
    List<PathFindingNode> neighborList = new List<PathFindingNode>();
    // TODO: Take wall corner case into account, i.e. an object cannot pass
    // from (0,0) to (1,1) if (0,1) and (1,0) are walls
    for (int x = -1; x <= 1; x++) {
      for (int y = -1; y <= 1; y++) {
        if (x == 0 && y == 0) continue;
        PathFindingNode neighborNode = grid.GetGridNode(curNode.x + x, curNode.y + y);
        if (neighborNode != null && !neighborNode.IsWall()) {
          neighborList.Add(neighborNode);
        }
      }
    }
    return neighborList;
  }

  private PathFindingNode GetLowestFCostNode(List<PathFindingNode> nodeList) {
     PathFindingNode lowestFCostNode = nodeList[0];
     foreach (PathFindingNode curNode in nodeList) {
       if (curNode.fCost < lowestFCostNode.fCost) {
         lowestFCostNode = curNode;
       }
     }
     return lowestFCostNode;
  }

  private int CalculateDistanceCost(PathFindingNode node1, PathFindingNode node2) {
    int xDistance = Mathf.Abs(node1.x - node2.x);
    int yDistance = Mathf.Abs(node1.y - node2.y);
    int remaining = Mathf.Abs(xDistance - yDistance);
    return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
  }

}
