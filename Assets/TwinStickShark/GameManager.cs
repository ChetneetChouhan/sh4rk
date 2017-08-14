﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;

    public int resetTimer;
    public Bounds movementBounds;
    public Bounds cameraBounds;

    void Awake() {
        if (_instance != null)
            throw new System.Exception();

        _instance = this;
    }

    public static void GameOver() {
        Debug.Log("GameOver!");
        Time.timeScale = 0;
        GameOverSplash.Show();
        _instance.Reload();
    }

    protected void Reload() {
        StartCoroutine(ReloadLevel());
    }

    private IEnumerator ReloadLevel() {
        var scene = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(resetTimer);
        Time.timeScale = 1;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public static Vector3 BindToGameArea(Vector3 position) {
        return _instance.movementBounds.Contains(position) ? position : _instance.movementBounds.ClosestPoint(position);
    }

    public static Vector3 BindToCameraGameArea(Vector3 position) {
        return _instance.cameraBounds.Contains(position) ? position : _instance.cameraBounds.ClosestPoint(position);
    }
}
