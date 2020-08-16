using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour {

  private static GamePlayManager instance;

  public static GamePlayManager Instance {
    get {
      if (instance == null) {
        instance = new GamePlayManager();
      }
      return instance;
    }
  }

}
