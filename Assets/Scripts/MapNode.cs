using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode {
  private int _x;
  private int _y;
  private bool _isWall;

  public MapNode(int x, int y, bool isWall) {
    this._x = x;
    this._y = y;
    this._isWall = isWall;
  }
  public int x() {
    return _x;
  }
  public int y() {
    return _y;
  }
  public bool isWall() {
    return _isWall;
  }
}
