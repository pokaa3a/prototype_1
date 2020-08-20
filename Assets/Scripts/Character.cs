using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  public List<Vector2> pathNodes;
  public GameObject pathLine;
  public GameObject pathEndCircle;

  private float speed = 2.0f;

  // public void MoveTo(Vector2Int targetPosition) {
  //   gameObject.transform.position = GameUtilities.GameToWorldUnit(targetPosition);
  // }

  public void MoveTo(Vector2 targetPosition) {
    gameObject.transform.position = GameUtilities.GameToWorldUnit(targetPosition);
  }

  void Start() {
    pathNodes = new List<Vector2>();
  }

  void Update() {
    if (pathNodes.Count > 1) {
      float distanceToMove = speed * Time.deltaTime;
      float distanceMoved = 0.0f;
      Vector2 posGame = gameObject.transform.position / GameUtilities.unit;
      while (pathNodes.Count > 1 && distanceMoved < distanceToMove) {
        if (Vector2.Distance(posGame, pathNodes[1]) < distanceToMove - distanceMoved) {
          distanceMoved += Vector2.Distance(posGame, pathNodes[1]);
          MoveTo(pathNodes[1]);
          pathNodes.RemoveAt(0);
        } else {
          float t = (distanceToMove - distanceMoved) 
                    / Vector2.Distance(posGame, pathNodes[1]);
          distanceMoved = distanceToMove;
          Vector2 tmp = Vector2.Lerp(posGame, pathNodes[1], t);
          MoveTo(Vector2.Lerp(posGame, pathNodes[1], t));
          pathNodes[0] = posGame;
        }
      }
      Drawing.UpdateLineByList(pathLine, pathNodes);
      if (pathNodes.Count == 1) {
        Destroy(pathLine);
        Destroy(pathEndCircle);
      }
    }    
  }

}
