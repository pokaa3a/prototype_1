using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour {
  
  List<Enemy> enemies;

  void Start() {
    enemies = new List<Enemy>();
    List<Vector2Int> enemyPts = CSVReader.LoadEnemies();
    foreach (Vector2Int vec in enemyPts) {
      enemies.Add(new Enemy(vec.x, vec.y));
    }
  }

  void Update() {
    MoveAllEnemies(Time.deltaTime);
  }

  void MoveAllEnemies(float deltaTime) {
    foreach (Enemy enemy in enemies) {
      if (enemy.IsMoving()) {
        enemy.MoveAlongPath(deltaTime);
      } else {
        enemy.FindNextTarget();
      }
    }
  }
}
