﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    #region Params
    [SerializeField]
    private Button _button;
    private bool _isPaused = false;

    GridManager helpers;

    #endregion
    #region EventArgs
    public class OnSwitchEventArgs
    {
        public Tile tileToMove;
        public Tile targetedTile;
        public Cell cellToMove;
        public Cell targetedCell;
    }

    public class OnClickEventArgs
    {
        public Vector3 mousePos;
    }
    #endregion
    #region Event Listeners
    public event EventHandler<EventArgs> onAppInitializeListener;

    public event EventHandler<EventArgs> onGameStartListener;

    public event EventHandler<OnClickEventArgs> onClickListener;

    public event EventHandler<EventArgs> onGamePauseListener;

    public event EventHandler<EventArgs> onGameResumeListener;

    public event EventHandler<EventArgs> onGameEndListener;

    public event EventHandler<OnSwitchEventArgs> onSwitchListener;
    #endregion
    #region Methodes
    private void Awake()
    {
        //FindObjectOfType<SoundManager>().onPlaySoundListener += OnAppInitializeEmitter;
        OnAppInitialize();
    }
    private void Start()
    {
        helpers = GetComponent<GridManager>();
        #region souscriptions en début de partie
        FindObjectOfType<TimerManager>().onGameEndTimerListener += OnGameEndEmitter;
        FindObjectOfType<ScoreManager>().onScoreUpdateListener += OnGameStartEmitter;
        
        //onGameStartListener += OnGameStartEmitter;
        #endregion
        OnGameStart();
        StartCoroutine(Playing());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Je suis en pause");
            TogglePause();
        }
    }
    public void TogglePause()
    {
        _isPaused = !_isPaused;

    }
    public void Switch(Cell cellToMove, float switchDuration)
    {

    }
    #endregion
    #region Co-routines
    public IEnumerator Playing()
    {
        //Touch touch = Input.GetTouch(0);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Je clique");

                OnClick(new OnClickEventArgs()
                {
                    mousePos = Input.mousePosition

                });
            }
            yield return null;
        }
    }

    
    #endregion
    #region Event Emitters
    public void OnAppInitializeEmitter(object sender, EventArgs e)
    {
        // ajouter la logique interne
        print("I'm inside the event OnAppInitializeEmitter" + e);

    }
    public void OnGameStartEmitter(object sender, EventArgs e)
    {
        // ajouter la logique interne
        print("I'm inside the event OnGameStartEmitter" + e);
    }
    public void OnGamePauseEmitter(object sender, EventArgs e)
    {
        // ajouter la logique interne
        print("I'm inside the event OnGamePauseEmitter" + e);
    }
    public void OnGameEndEmitter(object sender, EventArgs e)
    {
        // ajouter la logique interne
        print("I'm inside the event OnGameEndEmitter" + e);
        this.enabled = false;
    }
    /*public void OnClickEmitter(object sender, OnClickEventArgs e)
    {
        print("I'm inside the event OnClickEmitter" + e);
    }*/
    
    #endregion

    #region Event Invokers
    public void OnAppInitialize()
    {
        onAppInitializeListener?.Invoke(this, new EventArgs());
    }
    public void OnGameStart()
    {
        onGameStartListener?.Invoke(this, new EventArgs());
    }
    public void OnGamePause()
    {
        onGamePauseListener?.Invoke(this, new EventArgs());
    }
    public void OnGameResume()
    {
        onGameResumeListener?.Invoke(this, new EventArgs());
    }
    public void OnGameEnd()
    {
        onGameEndListener?.Invoke(this, new EventArgs());
    }
    public void OnClick(OnClickEventArgs e)
    {
        onClickListener?.Invoke(this, e);
    }


    #endregion

}
