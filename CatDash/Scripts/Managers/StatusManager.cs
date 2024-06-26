using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{

    public enum GameStatus
    {
        Start,
        Playing,
        Pause,
        End
    }
    public GameStatus gameStatus = GameStatus.Start;

    public enum Status
    {
    Normal,
    FallDown,
    Dead
    }
    public Status status = Status.Normal;

    public float timerCount = 20f;

    public Quaternion displayDirection;
    public Vector3 displayPosition;

    public Quaternion playerRotation;
    public Vector3 playerPosition;

    public float catColorValue;

    private GameOverUI gameOverUI;

    // 饼
    public int level = 0; // 0: easy, 1: normal, 2: hard

    void Awake()
    {
        gameOverUI = GameObject.FindWithTag(TagName.gameOverUI).GetComponentInChildren<GameOverUI>();
        gameOverUI.transform.parent.gameObject.SetActive(false);
    }

    void Start()
    {
        Save.LoadRankingList();
    }
    
    void Update()
    {
        switch (gameStatus)
        {
            case GameStatus.Start:
                Cursor.visible = true;
                //显示开始UI栏
                //找到Start并激活

                break;
            case GameStatus.Playing:
                Cursor.visible = false;

                if (GetComponent<Timer>().timerCount <= 0)
                    SwitchStatus(Status.Dead);

                break;
            case GameStatus.Pause:
                Cursor.visible = true;
                break;
            case GameStatus.End:
                Cursor.visible = true;
                break;
        }
    }
    public void SwitchGameStatus(GameStatus gameStatus)
    {
        this.gameStatus = gameStatus;
        switch (gameStatus)
        {
            case GameStatus.End:
                GetComponent<Timer>().StopTimer();
                gameOverUI.transform.parent.gameObject.SetActive(true);
                gameOverUI.gameObject.SetActive(true);
                gameOverUI.GameOver();

                break;
        }
    }
    public void SwitchStatus(Status status)
    {
        this.status = status;
        switch (status)
        {
            case Status.FallDown:
                //添加分数
                GetComponent<ScoreBoard>().Score(1);

                //玩家反馈
                GameObject.Find("Player").GetComponent<PlayerControl>().FallDown();

                //开始计时
                GetComponent<Timer>().SetTimer(true, timerCount, 0f);
                GetComponent<Timer>().StartTimer();

                //生成新的目标点
                GameObject.Find("TargetPointGenerator").GetComponent<TargetPointGenerator>().GenerateTargetPoint();

                SwitchStatus(Status.Normal);
                break;
            case Status.Dead:
                GameObject.Find("Player").GetComponent<PlayerControl>().Dead();

                // Debug.Log("Game Over");
                SwitchGameStatus(GameStatus.End);
                break;
        }
    }
    public void GameStart()
    {
        SwitchGameStatus(GameStatus.Playing);
        SwitchStatus(Status.Normal);
        GetComponent<Timer>().SetTimer(true, timerCount, 0f);
        GetComponent<Timer>().StartTimer();
    }

    public void ShowGameTitle(){
        GameObject.FindWithTag(TagName.startUI).GetComponent<StartUI>().OpenStartUI();
        GameObject.FindWithTag(TagName.player).transform.position = new Vector3(0, 0, 0);
    }
}
