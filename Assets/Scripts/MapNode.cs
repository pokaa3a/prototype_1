using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode {
  private int _x;
  private int _y;
  private bool _isWall;

  public MapNode(int x, int y) {
    this._x = x;
    this._y = y;
    this._isWall = false;
  }
  public int x() {
    return _x;
  }
  public int y() {
    return _y;
  }
  public bool IsWall() {
    return _isWall;
  }
  public void SetWall(bool isWall) {
    this._isWall = isWall;
    if (IsWall()) {
      Object wallPrefab = Resources.Load("wall");
      Vector3 pos = new Vector3(GameUtil.ToWorldUnit(_x), GameUtil.ToWorldUnit(_y), -1.0f);
      GameObject.Instantiate(wallPrefab, pos, Quaternion.identity);
    }
  }
}
