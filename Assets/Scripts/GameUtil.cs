using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtil {
  public const float unit = 0.4f;
  public const int maxX = 12;
  public const int minX = -12;
  public const int maxY = 20;
  public const int minY = -20;

  /* Convert values from world scale to game scale. */
  public static Vector2 ToGameUnit(Vector2 vec2World) {
    return vec2World / unit;
  }
  public static Vector2 ToGameUnit(Vector3 vec3World) {
    return new Vector2(ToGameUnit(vec3World.x), ToGameUnit(vec3World.y));
  }
  public static float ToGameUnit(float valWorld) {
    return valWorld / unit;
  }
  public static Vector2Int ToGameUnitInt(Vector3 vec3World) {
    return new Vector2Int(ToGameUnitInt(vec3World.x), ToGameUnitInt(vec3World.y));
  }
  public static int ToGameUnitInt(float valWorld) {
    return (int)Mathf.Round(ToGameUnit(valWorld));
  }
  /* Convert values from game scale to world scale. */
  public static Vector2 ToWorldUnit(Vector2 vec2Game) {
    return vec2Game * unit;
  }
  public static Vector2 ToWorldUnit(MapNode node) {
    return ToWorldUnit(new Vector2((float)node.x(), (float)node.y()));
  }
  public static float ToWorldUnit(float valGame) {
    return valGame * unit;
  }
  public static int ToWorldUnitInt(float valGame) {
    return (int)Mathf.Round(ToWorldUnit(valGame));
  }
}
