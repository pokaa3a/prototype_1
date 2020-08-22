using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  public MovingPath movingPath;
  private float speed = 2.0f;

  void Start() {
    movingPath = new MovingPath();
  }
  void Update() {
    if (movingPath.HasPath()) {
      float distanceToMove = speed * Time.deltaTime;
      MoveTo(movingPath.MoveAlongPath(distanceToMove));
    }
  }
  public void MoveTo(Vector2 targetPosition) {
    gameObject.transform.position = GameUtil.ToWorldUnit(targetPosition);
  }
  public Vector2Int GetPathEndCirclePos() {
    return new Vector2Int(GameUtil.ToGameUnitInt(movingPath.pathEndCircle.transform.position.x),
                          GameUtil.ToGameUnitInt(movingPath.pathEndCircle.transform.position.y));
  }
  public void ResetPath() {
    movingPath.Reset();
  }
  public Vector2 GetPosition() {
    return new Vector2(GameUtil.ToGameUnit(gameObject.transform.position.x), 
                       GameUtil.ToGameUnit(gameObject.transform.position.y));
  }
  public Vector2Int GetPositionInt() {
    return new Vector2Int(GameUtil.ToGameUnitInt(gameObject.transform.position.x),
                          GameUtil.ToGameUnitInt(gameObject.transform.position.y));
  }
  public void SetPath(List<Vector2> path) {
    movingPath.SetPath(path);
  }
  public void SetEndCircle(MapNode node) {
    movingPath.SetEndCircle(node);
  }
  public void MoveEndCircle(MapNode node) {
    movingPath.MoveEndCircle(node);
  }
  public void UpdatePath(MapNode node) {
    if (!IsMoving()) {
      return;
    }
    MapNode circlePos = MapHandler.GetMapNodeFromWorld(movingPath.pathEndCircle.transform.position);
    if (node != circlePos) {
      movingPath.pathNodes.Add(new Vector2((float)node.x(), (float)node.y()));
      MoveEndCircle(node);
    }
  }
  public bool IsMoving () {
    return movingPath.HasPath();
  }
}
