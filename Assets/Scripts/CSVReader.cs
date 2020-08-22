using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class CSVReader {

  private static string SPLIT_RE = @",";
  private static string LINE_SPLIT_RE = @"\n";

  public static List<Vector2Int> LoadMap() {
    List<Vector2Int> walls = new List<Vector2Int>();
    string file = "Maps/map0";

    TextAsset data = Resources.Load(file) as TextAsset;
    Assert.IsNotNull(data);
    var lines = Regex.Split(data.text, LINE_SPLIT_RE);

    for (int i = 0; i < lines.Length; i++) {
      var values = Regex.Split(lines[i], SPLIT_RE);
      if (values.Length == 0 || values[0] == "") continue;
      walls.Add(new Vector2Int(Convert.ToInt32(values[0]), Convert.ToInt32(values[1])));
    } 
    return walls;
  }
  public static List<Vector2Int> LoadEnemies() {
    List<Vector2Int> enemies = new List<Vector2Int>();
    string file = "Maps/enemies0";

    TextAsset data = Resources.Load(file) as TextAsset;
    Assert.IsNotNull(data);
    var lines = Regex.Split(data.text, LINE_SPLIT_RE);

    for (int i = 0; i < lines.Length; i++) {
      var values = Regex.Split(lines[i], SPLIT_RE);
      if (values.Length == 0 || values[0] == "") continue;
      enemies.Add(new Vector2Int(Convert.ToInt32(values[0]), Convert.ToInt32(values[1])));
    }
    return enemies;
  }
}
