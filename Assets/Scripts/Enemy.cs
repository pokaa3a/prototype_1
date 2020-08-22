using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy {
  
  GameObject enemy;
  public MovingPath movingPath;
  private float speed = 1.0f;
  private int bornX;
  private int bornY;
  private int wanderDistance = 5;

  public Enemy(int x, int y) {
    bornX = x;
    bornY = y;
    Object enemyPrefab = Resources.Load("enemy");
    Assert.IsNotNull(enemyPrefab);
    Vector3 pos = new Vector3((float)GameUtil.ToWorldUnit(x), 
                              (float)GameUtil.ToWorldUnit(y),
                              0.0f);
    enemy = (GameObject)GameObject.Instantiate(enemyPrefab, pos, Quaternion.identity);
    movingPath = new MovingPath();
  }

  public MapNode GetPosition() {
    return MapHandler.GetMapNodeFromWorld(enemy.transform.position);
  }

  void MoveTo(Vector2 dstPos) {
    enemy.transform.position = GameUtil.ToWorldUnit(dstPos);
  }

  public void MoveAlongPath(float deltaTime) {
    if (movingPath.HasPath()) {
      float distanceToMove = speed * deltaTime;
      MoveTo(movingPath.MoveAlongPath(distanceToMove));
    }
  }

  public bool IsMoving() {
    return movingPath.HasPath();
  }

  public void SetPath(List<Vector2> path) {
    movingPath.SetPath(path);
  }

  public void FindNextTarget() {
    movingPath.Reset();
    MapNode curNode = GetPosition();
    MapNode nextNode = curNode;
    while (nextNode == null || nextNode == curNode || nextNode.IsWall()) {
      int x = Random.Range(bornX - wanderDistance, bornX + wanderDistance);
      int y = Random.Range(bornY - wanderDistance, bornY + wanderDistance);
      nextNode = MapHandler.GetMapNode(x, y);
    }
    List<Vector2> path = MapHandler.FindPath(curNode, nextNode);
    SetPath(path);
  }
}
