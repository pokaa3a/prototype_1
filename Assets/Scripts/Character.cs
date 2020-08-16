using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

  public List<Vector2> pathNodes;
  public GameObject pathLine;
  public GameObject pathEndCircle;

  private float speed = 1f;

  public void MoveTo(Vector2 targetPosition) {
    gameObject.transform.position = targetPosition;
  }

  void Start() {
    pathNodes = new List<Vector2>();
  }

  void Update() {
    if (pathNodes.Count > 1) {
      float distanceToMove = speed * Time.deltaTime;
      float distanceMoved = 0.0f;
      while (pathNodes.Count > 1 && distanceMoved < distanceToMove) {
        if (Vector2.Distance(gameObject.transform.position, pathNodes[1])
            < distanceToMove - distanceMoved) {
          distanceMoved += Vector2.Distance(gameObject.transform.position, pathNodes[1]);
          MoveTo(pathNodes[1]);
          pathNodes.RemoveAt(0);
        } else {
          float t = (distanceToMove - distanceMoved) 
                    / Vector2.Distance(gameObject.transform.position, pathNodes[1]);
          distanceMoved = distanceToMove;
          MoveTo(Vector2.Lerp(gameObject.transform.position, pathNodes[1], t));
          pathNodes[0] = gameObject.transform.position;
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
