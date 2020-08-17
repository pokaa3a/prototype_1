using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public static class GameUtilities {
  public static float unit = 0.4f;

  public static Vector2 WorldToGameUnit(Vector2 posWorld) {
    Vector2 posGame = new Vector2(Mathf.Round(posWorld.x / unit),
                                  Mathf.Round(posWorld.y / unit));
    return posGame;
  }

  public static float WorldToGameUnit(float valWorld) {
    return Mathf.Round(valWorld / unit);
  }

  // public static Vector2 GameToWorldUnit(Vector2Int posGame) {
  //   Vector2 posWorld = new Vector2((float)posGame.x * unit, (float)posGame.y * unit);
  //   return posWorld;
  // }

  public static Vector2 GameToWorldUnit(Vector2 posGame) {
    return new Vector2(posGame.x * unit, posGame.y * unit);
  }

  // public static float GameToWorldUnit(int valGame) {
  //   return (float)valGame * unit;
  // }

  public static float GameToWorldUnit(float valGame) {
    return valGame * unit;
  }
}
