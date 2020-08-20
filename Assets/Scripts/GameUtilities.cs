using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class GameUtilities {
  public static float unit = 0.4f;
  public static int maxX = 12;
  public static int minX = -12;
  public static int maxY = 20;
  public static int minY = -20;

  public static Vector2 WorldToGameUnit(Vector2 posWorld) {
    Vector2 posGame = new Vector2(Mathf.Round(posWorld.x / unit),
                                  Mathf.Round(posWorld.y / unit));
    return posGame;
  }

  public static float WorldToGameUnit(float valWorld) {
    return Mathf.Round(valWorld / unit);
  }

  public static Vector2 GameToWorldUnit(Vector2 posGame) {
    return new Vector2(posGame.x * unit, posGame.y * unit);
  }

  public static float GameToWorldUnit(float valGame) {
    return valGame * unit;
  }
}
